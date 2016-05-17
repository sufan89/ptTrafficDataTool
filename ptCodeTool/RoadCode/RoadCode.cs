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
        public void RoadLineCode(IList<RoadFeature> SortedRoadLine,IFeature pRegionFeature,DataRow pModelRow)
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
    }
}
