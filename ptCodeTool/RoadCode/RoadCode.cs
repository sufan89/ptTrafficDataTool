using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using ptRoadCodeConfig;

namespace ptCodeTool
{
    class RoadCode:IRoadCode
    {
        /// <summary>
        /// 佛山道路编码
        /// </summary>
        /// <param name="pRoadLayer"></param>
        /// <param name="pRegionLayer"></param>
        public void Coding(IFeatureLayer pRoadLayer,IFeatureLayer pRegionLayer)
        {
            if (pRoadLayer == null || pRegionLayer==null) return;
            IFeatureClass pRoadFc = pRoadLayer.FeatureClass;
            IFeatureClass pRegionFc = pRegionLayer.FeatureClass;
            //获取所有行政区面
            IList<IFeature> AllRegionFeatures = ptGeoFeatureBase.GetFeatures(pRegionFc);

            if (AllRegionFeatures.Count == 0) return;
            ptDataPool pDataPool=new ptDataPool();
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
                        string StrWhere = string.Format("{0}='{1}'", ptColumnName.Shape_RoadLevel,pRow[ptColumnName.ModelName].ToString());
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
        /// <summary>
        /// 进行道路数据编码
        /// </summary>
        /// <param name="SortedRoadLine"></param>
        private void RoadLineCode(IList<RoadFeature> SortedRoadLine,IFeature pRegionFeature,DataRow pModelRow)
        {
            if (SortedRoadLine == null && SortedRoadLine.Count == 0) return;
            if (pRegionFeature == null) return;
            if (pModelRow == null) return;
            int maxNum = 0;
           
            //获取行政区编码
            int RegionFieldIndex = pRegionFeature.Fields.FindField(ptColumnName.Shape_RegionCode);
            if (RegionFieldIndex < 0) return;
            string RoadRegionCode = GetReigonCode(pRegionFeature.get_Value(RegionFieldIndex).ToString());
            foreach (RoadFeature RoadLine in SortedRoadLine)
            {
                string StrSequence = "000" + maxNum.ToString();
                StrSequence = StrSequence.Substring(StrSequence.Length - 3);
                IFeature pRoadFeature = RoadLine.m_RoadFeature;
                int RoadCodeIndex=pRoadFeature.Fields.FindField(ptColumnName.Shape_RoadCode);
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
        /// <summary>
        /// 进行道路数据编码(深圳道路编码)
        /// </summary>
        /// <param name="SortedRoadLine"></param>
        private void RoadLineCode(IList<RoadFeature> SortedRoadLine, DataRow pModelRow)
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
                    int RoadCodeIndex = pRoadFeature.Fields.FindField(ptColumnName.Shape_RoadCode_SZ);
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
 
            }
        }
        private string GetReigonCode(string StrRegionField)
        {
            string StrRegionCode = "";
            if (StrRegionField.Length != 9) return StrRegionCode; 

            ptDataPool pDataPool = new ptDataPool();
            Dictionary<string,string> pTable = pDataPool.DicRegionCode;

            if (pTable.ContainsKey(StrRegionField))
            {
                string shiCode = StrRegionField.Substring(0, 6);
                if (pTable.ContainsKey(shiCode))
                {
                    StrRegionCode = pTable[shiCode] + pTable[StrRegionField];
                }
            }
            return StrRegionCode;
        }
        /// <summary>
        /// 深圳市道路编码
        /// </summary>
        /// <param name="pRoadLayer"></param>
        public void Coding(IFeatureLayer pRoadLayer)
        {
            try
            {
                if (pRoadLayer == null) return;
                //获取配置信息
                ptDataPool pDataPool = new ptDataPool();
                DataTable ModelDt = pDataPool.ModelDt;
                //根据配置信息来获取图层要素进行编码
                DataView dv = ModelDt.DefaultView;
                dv.RowFilter = string.Format("{0}='{1}'", ptColumnName.ModelType, "道路等级");
                DataTable pRoadLevel = dv.ToTable(true, new string[] { ptColumnName.ModelCode, ptColumnName.StartCode });
                for (int i = 0; i < pRoadLevel.Rows.Count; i++)
                {
                    string RoadLevelId = pRoadLevel.Rows[i][ptColumnName.ModelCode].ToString();
                    DataRow[] pRoadRows = ModelDt.Select(string.Format("{0}='{1}'", ptColumnName.ModelCode, RoadLevelId));
                    if (pRoadRows.Length == 0) continue;
                    //构造条件选择所有的道路
                    string StrWhere = "";
                    foreach (DataRow pRow in pRoadRows)
                    {
                        StrWhere = StrWhere + string.Format("{0}='{1}' or ", ptColumnName.Shape_RoadLevel_SZ, pRow[ptColumnName.ModelName]);
                    }
                    //去掉最后的OR
                    StrWhere = StrWhere.Substring(0, StrWhere.LastIndexOf("or"));
                    //获取所有符合条件的要素
                    //获取起点在该行政区内的所有道路线
                    IList<IFeature> AllRoadLine = ptGeoFeatureBase.GetFeatures(pRoadLayer.FeatureClass, StrWhere);
                    //根据排序规则，多该行政区内的所有道路线进行排序
                    SortFeature SortRoad = new SortFeature(AllRoadLine);
                    SortRoad.Sort();
                    IList<RoadFeature> SortedRoadLine = SortRoad.m_SortedRoad;
                    //根据编码规则进行编码
                    RoadLineCode(SortedRoadLine, pRoadLevel.Rows[i]);
                }
            }
            catch (Exception ex)
            {
 
            }

        }
    }
}
