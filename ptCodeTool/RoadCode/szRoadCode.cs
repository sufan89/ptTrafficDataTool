using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ptRoadCodeConfig;
using System.Data;
using ESRI.ArcGIS.Geodatabase;

namespace ptCodeTool
{
    /// <summary>
    /// 深圳道路编码
    /// </summary>
    class szRoadCode :IRoadCode
    {
        public szRoadCode(RefreshLogEventHandle RefreshLog)
        {
            if (RefreshLog != null)
            {
                m_RefreshLog = RefreshLog;
            }
        }
        private RefreshLogEventHandle m_RefreshLog;

        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer)
        {
            if (dicCodeLayer.Count <= 0)
            {
                m_RefreshLog(string.Format("未加载任何道路图层配置"));
            }
            try
            {
                //获取配置信息
                ptDataPool pDataPool = new ptDataPool();
                DataTable ModelDt = pDataPool.ModelDt;
                foreach (string layername in dicCodeLayer.Keys)
                {
                    m_RefreshLog(string.Empty);

                    m_RefreshLog(string.Format("开始对图层【{0}】进行编码", layername));
                    ptCodeFeautreLayer ptCodeRoadLayer = dicCodeLayer[layername];
                    IFeatureLayer pRoadLayer = ptCodeRoadLayer.CodeLayer;
                    //根据配置信息来获取图层要素进行编码
                    //根据道路等级进行编码
                    DataView dv = ModelDt.DefaultView;
                    dv.RowFilter = string.Format("{0}='{1}'", ptColumnName.ModelType, Enum.GetName(typeof(ModelType),ModelType.RoadLevel));
                    DataTable pRoadLevel = dv.ToTable(true, new string[] { ptColumnName.ModelCode, ptColumnName.StartCode });
                    //数据中道路等级字段，通过配置获取
                    string DataRoadLevel = ptCodeRoadLayer.LayerConfigRow[ptColumnName.LevelField].ToString();
                    if (string.IsNullOrEmpty(DataRoadLevel))
                    {
                        m_RefreshLog(string.Format("未配置图层【{0}】的道路分级字段", layername));
                        continue;
                    }

                    for (int i = 0; i < pRoadLevel.Rows.Count; i++)
                    {
                        string RoadLevelId = pRoadLevel.Rows[i][ptColumnName.ModelCode].ToString();
                        DataRow[] pRoadRows = ModelDt.Select(string.Format("{0}='{1}' and {2}='{3}'", ptColumnName.ModelCode, RoadLevelId, 
                            ptColumnName.ModelType, Enum.GetName(typeof(ModelType), ModelType.RoadLevel)));
                        if (pRoadRows.Length == 0) continue;
                        //构造条件选择所有的道路
                        string StrWhere = "";
                        //日志显示信息
                        string LogStr = "";
                        foreach (DataRow pRow in pRoadRows)
                        {
                            StrWhere = StrWhere + string.Format("{0}='{1}' or ", DataRoadLevel, pRow[ptColumnName.ModelName]);
                            LogStr = LogStr + string.Format("{0},", pRow[ptColumnName.ModelName]);
                        }
                        //去掉最后的OR
                        StrWhere = StrWhere.Substring(0, StrWhere.LastIndexOf("or"));
                        LogStr = LogStr.Substring(0, LogStr.LastIndexOf(","));
                        //写日志
                        m_RefreshLog(string.Format("开始对图层【{0}】中类型为【{1}】的数据进行编码",layername,LogStr));
                        //获取所有符合条件的要素
                        //获取起点在该行政区内的所有道路线
                        IList<IFeature> AllRoadLine = ptGeoFeatureBase.GetFeatures(pRoadLayer.FeatureClass, StrWhere);
                        m_RefreshLog(string.Format("完成对图层【{0}】中类型为【{1}】的数据获取", layername, LogStr));
                        //根据排序规则，多该行政区内的所有道路线进行排序
                        SortFeature SortRoad = new SortFeature(AllRoadLine);
                        SortRoad.Sort();
                        m_RefreshLog(string.Format("完成对图层【{0}】中类型为【{1}】的数据排序", layername, LogStr));
                        IList<RoadFeature> SortedRoadLine = SortRoad.m_SortedRoad;
                        //根据编码规则进行编码
                        RoadLineCode(SortedRoadLine, pRoadLevel.Rows[i], ptCodeRoadLayer.CodeField);
                        m_RefreshLog(string.Format("完成对图层【{0}】中类型为【{1}】的数据编码", layername, LogStr));

                        m_RefreshLog(string.Empty);
                    }
                    m_RefreshLog(string.Format("图层【{0}】道路编码完成编码", layername));
                }
            }
            catch (Exception ex)
            {
                m_RefreshLog(string.Format("编码失败: {0}", ex.Message));
                return;
            }
        }
        /// <summary>
        /// 进行道路数据编码(深圳道路编码)
        /// </summary>
        /// <param name="SortedRoadLine"></param>
        private void RoadLineCode(IList<RoadFeature> SortedRoadLine, DataRow pModelRow,string StrCodeField)
        {
            try
            {
                if (SortedRoadLine == null || SortedRoadLine.Count == 0) return;
                if (pModelRow == null) return;
                //获取编码起始值
                int maxNum = 0;
                string StrStartCode = pModelRow[ptColumnName.StartCode].ToString();
                if (string.IsNullOrEmpty(StrStartCode)) maxNum = 0;
                else int.TryParse(StrStartCode, out maxNum);
                foreach (RoadFeature RoadLine in SortedRoadLine)
                {
                    string StrSequence = maxNum.ToString();
                    IFeature pRoadFeature = RoadLine.m_RoadFeature;
                    int RoadCodeIndex = pRoadFeature.Fields.FindField(StrCodeField);
                    //道路编码
                    string RoadCode = pModelRow[ptColumnName.ModelCode].ToString() + StrSequence;
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
                m_RefreshLog(string.Format("编码失败: {0}", ex.Message));
                return;
            }
        }
    }
}
