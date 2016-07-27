using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
//要素排序
namespace ptCodeTool
{
    class SortFeature
   {
       public SortFeature(IList<IFeature> pSortFeatures)
       {
           SortFeatures = pSortFeatures;
       }
       /// <summary>
       /// 要排序的要素
       /// </summary>
       private IList<IFeature> SortFeatures;
       /// <summary>
       /// 排序后的结果
       /// </summary>
       private IList<RoadFeature> SortedRoad;
       public IList<RoadFeature> m_SortedRoad
       {
           get { return SortedRoad; }
       }
       /// <summary>
       /// 进行要素排序
       /// </summary>
       public void Sort()
       {
           if (SortFeatures == null ||SortFeatures.Count == 0) return;
           IList<RoadFeature> AllStartPoint = getStartPoint(SortFeatures);
           int i, j; //交换标志 
           RoadFeature temp;
           bool exchange;
           for (i = 0; i < AllStartPoint.Count; i++) //最多做R.Length-1趟排序 
           {
               exchange = false; //本趟排序开始前，交换标志应为假
               for (j = AllStartPoint.Count - 2; j >= i; j--)
               {
                   try
                   {
                       if (AllStartPoint[j + 1].m_StartPoint.X < AllStartPoint[j].m_StartPoint.X && AllStartPoint[j + 1].m_StartPoint.Y < AllStartPoint[j].m_StartPoint.Y)　//交换条件
                       {
                           temp = AllStartPoint[j + 1];
                           AllStartPoint[j + 1] = AllStartPoint[j];
                           AllStartPoint[j] = temp;
                           exchange = true; //发生了交换，故将交换标志置为真 
                       }
                   }
                   catch (Exception ex)
                   {
                       
                   }
               }
               if (!exchange) //本趟排序未发生交换，提前终止算法 
               {
                   break;
               }
           }
           SortedRoad = AllStartPoint;

       }
       /// <summary>
       /// 获取所有要素的起点集合
       /// </summary>
       /// <param name="pFeatures"></param>
       /// <returns></returns>
       private IList<RoadFeature> getStartPoint(IList<IFeature> pFeatures)
       {
           IList<RoadFeature> newList = new List<RoadFeature>();
           if (pFeatures == null && pFeatures.Count == 0) return newList;
           for (int i = 0; i < pFeatures.Count; i++)
           {
               IPoint pStartPoint = ptGeoFeatureBase.GetStartPoint(pFeatures[i].ShapeCopy as IPolyline);
               RoadFeature pRoadLine = new RoadFeature(pStartPoint, pFeatures[i]);
               if(!pFeatures[i].Shape.IsEmpty)
               newList.Add(pRoadLine);
           }
          return newList;
       }
    }

    class RoadFeature
   {
       public RoadFeature(IPoint pStartPoint, IFeature pRoadFeature)
       {
           StartPoint = pStartPoint;
           if (pRoadFeature.ShapeCopy.GeometryType == esriGeometryType.esriGeometryPolyline)
           {
               pRoadLine = pRoadFeature;
           }
           else
           {
               pRoadFeature = null;
           }
       }
       //起点
       private IPoint StartPoint;
       public IPoint m_StartPoint
       {
           get { return StartPoint; }
       }
       //道路线
       private IFeature pRoadLine;
       public IFeature m_RoadFeature
       {
           get { return pRoadLine; }

       }

   }
}
