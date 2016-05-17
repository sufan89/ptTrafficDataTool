using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ptCodeTool
{
    public static class ptGeoFeatureBase
    {
        /// <summary>
        /// 获取图层所有要素
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <returns></returns>
        public static IList<IFeature> GetFeatures(IFeatureClass pFeatureClass)
        {
            IList<IFeature> newList = new List<IFeature>();
            try
            {
                IFeatureCursor pCursor = pFeatureClass.Search(null,false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    newList.Add(pFeature);
                    pFeature = pCursor.NextFeature();
                }
                return newList;
            }
            catch (Exception ex)
            {
                return newList;    
            }
        }
        /// <summary>
        /// 获取所有指定行政区内的要素
        /// </summary>
        /// <param name="pRoadFeatureClass"></param>
        /// <param name="pRegionFeature"></param>
        /// <returns></returns>
        public static IList<IFeature> GetFeatures(IFeatureClass pRoadFeatureClass, IFeature pRegionFeature,string StrWhere="")
        {
            IList<IFeature> newList = new List<IFeature>();
            try
            {
                if (pRoadFeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline) return newList;
                ISpatialFilter pFiler = new SpatialFilterClass();
                pFiler.Geometry = pRegionFeature.ShapeCopy;
                pFiler.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                if (!string.IsNullOrEmpty(StrWhere))
                {
                    pFiler.WhereClause = StrWhere;
                }
                IFeatureCursor pCursor = pRoadFeatureClass.Search(pFiler, false);
                IFeature pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    IPolyline pLine = pFeature.ShapeCopy as IPolyline;
                    //获取起点
                    IPoint pStartPoint = GetStartPoint(pLine);
                    //判断起点是否在行政区内
                    IRelationalOperator pRela = pRegionFeature.ShapeCopy as IRelationalOperator;
                    if (pRela.Contains(pStartPoint))
                    {
                        newList.Add(pFeature);
                    }
                    pFeature = pCursor.NextFeature();
                }
                return newList;
            }
            catch (Exception ex)
            {
                return newList;
            }
        }
        /// <summary>
        /// 获取线起点
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        public static IPoint GetStartPoint(IPolyline pLine)
        {
            IPoint pStartPoint = null;
            try
            {
                if (pLine == null) return pStartPoint;
                ISegmentCollection pLines = pLine as ISegmentCollection;
                if (pLines.SegmentCount == 0) return pStartPoint;
                ILine pStarLine = pLines.get_Segment(0) as ILine;
                if (pStarLine == null) return pStartPoint;
                pStartPoint = pStarLine.FromPoint;
                return pStartPoint;
            }
            catch (Exception ex)
            {
                return pStartPoint;
            }
        }
    }
}
