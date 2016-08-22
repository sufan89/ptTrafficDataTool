using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ptRoadCodeConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ptCodeTool
{
    class FsRoadCode :IRoadCode
    {
        public FsRoadCode(RefreshLogEventHandle RefreshLog)
        {
            if (RefreshLog != null)
            {
                m_RefreshLog = RefreshLog;
            }
        }

        private RefreshLogEventHandle m_RefreshLog;
        /// <summary>
        /// 行政区信息
        /// </summary>
        private Dictionary<string, string> m_AllRegionInfo = new Dictionary<string, string>();
        /// <summary>
        /// 佛山道路编码
        /// </summary>
        /// <param name="dicCodeLayer"></param>
        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer)
        {
            if (dicCodeLayer.Count <= 0)
            {
                m_RefreshLog(string.Format("未配置任何编码图层"));
                return;
            }
            //获取区域图层
            ptCodeFeautreLayer ptRegionLayer = GetLayerByRoadType(dicCodeLayer,RoadeType.district);
            if (ptRegionLayer == null)
            {
                m_RefreshLog(string.Format("当前未配置区域图层"));
                return;
            }
            //获取道路中心线图层
            ptCodeFeautreLayer ptRoadLineLayer= GetLayerByRoadType(dicCodeLayer, RoadeType.roadline);
            if (ptRoadLineLayer == null)
            {
                m_RefreshLog(string.Format("当前未配置道路中心线图层"));
                return;
            }
            //判断是否有道路面图层，如果有，则需要对道路面进行编码
            //暂不判断
            ptCodeFeautreLayer  ptRoadPolygonLayer= GetLayerByRoadType(dicCodeLayer, RoadeType.roadformation);
            m_RefreshLog(string.Empty);
            //进行道路编码
            m_RefreshLog(string.Format("开始对图层：【{0}】进行编码", ptRoadLineLayer.LayerName));
            DoCoding(ptRoadLineLayer, ptRegionLayer);
            m_RefreshLog(string.Format("图层：【{0}】编码完成", ptRoadLineLayer.LayerName));
            m_RefreshLog(string.Empty);
            //道路面进行编码
            if (ptRoadPolygonLayer != null)
            {
                
            }
        }
        /// <summary>
        /// 获取行政区图层
        /// </summary>
        /// <param name="dicCodeLayer"></param>
        /// <returns></returns>
        private ptCodeFeautreLayer GetLayerByRoadType(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer,RoadeType sType)
        {
            ptCodeFeautreLayer pRegionLayer = null;
            foreach (string key in dicCodeLayer.Keys)
            {
                pRegionLayer = dicCodeLayer[key];
                DataRow pLayerRow = pRegionLayer.LayerConfigRow;
                string StrLayerType = pLayerRow[ptColumnName.RoadType].ToString();
                if (StrLayerType == Enum.GetName(typeof(RoadeType), sType))
                {
                    return pRegionLayer;
                }
            }
            return pRegionLayer;
        }
        /// <summary>
        /// 佛山道路编码
        /// </summary>
        /// <param name="pRoadLayer"></param>
        /// <param name="pRegionLayer"></param>
        private void DoCoding(ptCodeFeautreLayer pRoadLayer, ptCodeFeautreLayer pRegionLayer)
        {
            if (pRoadLayer == null || pRegionLayer == null) return;
            IFeatureClass pRoadFc = pRoadLayer.CodeLayer.FeatureClass;
            IFeatureClass pRegionFc = pRegionLayer.CodeLayer.FeatureClass;
            try
            {
                m_RefreshLog(string.Format("获取行政区信息"));
                //获取所有行政区面
                IList<IFeature> AllRegionFeatures = ptGeoFeatureBase.GetFeatures(pRegionFc);
                if (AllRegionFeatures.Count == 0)
                {
                    m_RefreshLog(string.Format("行政区信息获取失败"));
                    return;
                }
                ptDataPool pDataPool = new ptDataPool();
                DataTable ModelDt = pDataPool.ModelDt;
                for (int regionIndex = 0; regionIndex < AllRegionFeatures.Count; regionIndex++)
                {
                    if (ModelDt != null && ModelDt.Rows.Count > 0)
                    {
                        DataView dv = ModelDt.DefaultView;
                        //获取道路分级配置信息
                        dv.RowFilter = string.Format("{0}='{1}'", ptColumnName.ModelType, Enum.GetName(typeof(ModelType), ModelType.RoadLevel));
                        DataTable pRoadModelDt = dv.ToTable();
                        foreach (DataRow pRow in pRoadModelDt.Rows)
                        {
                            string StrWhere = string.Format("{0}='{1}'", pRoadLayer.LayerConfigRow[ptColumnName.LevelField], pRow[ptColumnName.ModelName].ToString());

                            IFeature pRegionFeature = AllRegionFeatures[regionIndex];
                            //获取起点在该行政区内的所有道路线
                            IList<IFeature> AllRoadLine = ptGeoFeatureBase.GetFeatures(pRoadFc, pRegionFeature, StrWhere);
                            string tempRegionCode = pRegionFeature.get_Value(pRegionFeature.Fields.FindField(pRegionLayer.CodeField)).ToString();
                            m_RefreshLog(string.Format("获取区域【{0}】中类型为【{1}】的要素：【{2}】", tempRegionCode,
                                pRow[ptColumnName.ModelName], AllRoadLine.Count));
                            //根据排序规则，多该行政区内的所有道路线进行排序
                            SortFeature SortRoad = new SortFeature(AllRoadLine);
                            SortRoad.Sort();
                            IList<RoadFeature> SortedRoadLine = SortRoad.m_SortedRoad;
                            m_RefreshLog(string.Format("完成对区域【{0}】中类型为【{1}】的要素排序", tempRegionCode, pRow[ptColumnName.ModelName]));
                            //根据编码规则进行编码
                            RoadLineCode(SortedRoadLine, pRegionFeature, pRow);
                            m_RefreshLog(string.Format("完成对区域【{0}】中类型为【{1}】的要素编码", tempRegionCode, pRow[ptColumnName.ModelName]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_RefreshLog(string.Format("编码失败:{0}",ex.Message));
            }
        }
        /// <summary>
        /// 进行道路数据编码
        /// </summary>
        /// <param name="SortedRoadLine"></param>
        private void RoadLineCode(IList<RoadFeature> SortedRoadLine, IFeature pRegionFeature, DataRow pModelRow)
        {
            if (SortedRoadLine == null && SortedRoadLine.Count == 0) return;
            if (pRegionFeature == null) return;
            if (pModelRow == null) return;
            int maxNum = 0;
            try
            {
                //获取行政区编码
                int RegionFieldIndex = pRegionFeature.Fields.FindField(ptColumnName.Shape_RegionCode);
                if (RegionFieldIndex < 0) return;
                string RoadRegionCode = GetReigonCode(pRegionFeature.get_Value(RegionFieldIndex).ToString());
                foreach (RoadFeature RoadLine in SortedRoadLine)
                {
                    string StrSequence = "000" + maxNum.ToString();
                    StrSequence = StrSequence.Substring(StrSequence.Length - 3);
                    IFeature pRoadFeature = RoadLine.m_RoadFeature;
                    int RoadCodeIndex = pRoadFeature.Fields.FindField(ptColumnName.Shape_RoadCode);
                    //道路编码
                    string RoadCode = pModelRow[ptColumnName.ModelCode].ToString() + StrSequence + RoadRegionCode;
                    if (RoadCodeIndex > 0)
                    {
                        pRoadFeature.set_Value(RoadCodeIndex, RoadCode);
                        pRoadFeature.Store();
                    }
                    maxNum++;
                }
            }
            catch (Exception ex)
            {
                m_RefreshLog(string.Format("道路编码失败：{0}",ex.Message));
            }
        }
        /// <summary>
        /// 获取行政区代码
        /// </summary>
        /// <param name="StrRegionField"></param>
        /// <returns></returns>
        private string GetReigonCode(string StrRegionField)
        {
            string StrRegionCode = "";
            if (StrRegionField.Length != 9) return StrRegionCode;
            if (m_AllRegionInfo.Count <= 0)
            {
                ptDataPool pDataPool = new ptDataPool();
                m_AllRegionInfo = pDataPool.DicRegionCode;
            }
            if (m_AllRegionInfo.ContainsKey(StrRegionField))
            {
                string shiCode = StrRegionField.Substring(0, 6);
                if (m_AllRegionInfo.ContainsKey(shiCode))
                {
                    StrRegionCode = m_AllRegionInfo[shiCode] + m_AllRegionInfo[StrRegionField];
                }
            }
            return StrRegionCode;
        }

    }
}
