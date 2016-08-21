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
            ptCodeFeautreLayer  ptRoadPolygonLayer= GetLayerByRoadType(dicCodeLayer, RoadeType.roadformation);
            
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
        private void DoCoding(IFeatureLayer pRoadLayer, IFeatureLayer pRegionLayer)
        {
            if (pRoadLayer == null || pRegionLayer == null) return;
            IFeatureClass pRoadFc = pRoadLayer.FeatureClass;
            IFeatureClass pRegionFc = pRegionLayer.FeatureClass;
            //获取所有行政区面
            IList<IFeature> AllRegionFeatures = ptGeoFeatureBase.GetFeatures(pRegionFc);

            if (AllRegionFeatures.Count == 0) return;
            ptDataPool pDataPool = new ptDataPool();
            DataTable ModelDt = pDataPool.ModelDt;
            for (int regionIndex = 0; regionIndex < AllRegionFeatures.Count; regionIndex++)
            {
                if (ModelDt != null && ModelDt.Rows.Count > 0)
                {
                    DataView dv = ModelDt.DefaultView;
                    dv.RowFilter = string.Format("{0}='{1}'", ptColumnName.ModelType, "道路等级");
                    DataTable pRoadModelDt = dv.ToTable();
                    foreach (DataRow pRow in pRoadModelDt.Rows)
                    {
                        string StrWhere = string.Format("{0}='{1}'", ptColumnName.Shape_RoadLevel, pRow[ptColumnName.ModelName].ToString());
                        IFeature pRegionFeature = AllRegionFeatures[regionIndex];
                        //获取起点在该行政区内的所有道路线
                        IList<IFeature> AllRoadLine = ptGeoFeatureBase.GetFeatures(pRoadFc, pRegionFeature, StrWhere);
                        //根据排序规则，多该行政区内的所有道路线进行排序
                        SortFeature SortRoad = new SortFeature(AllRoadLine);
                        SortRoad.Sort();
                        IList<RoadFeature> SortedRoadLine = SortRoad.m_SortedRoad;
                        //根据编码规则进行编码
                        RoadLineCode(SortedRoadLine, pRegionFeature, pRow);
                    }
                }
            }

        }
    }
}
