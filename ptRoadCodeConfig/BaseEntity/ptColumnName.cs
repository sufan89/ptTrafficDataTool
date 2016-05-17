using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptRoadCodeConfig
{
   public static class ptColumnName
   {
       #region T_RegionConfig
       public const string RegionCode = "RegionCode";
       public const string RegionName = "RegionName";
       public const string RoadRegion = "RoadRegion";
       public const string RegionLevel = "RegionLevel";
       #endregion
       public const string ModelName = "ModelName";
       public const string ModelCode = "ModelCode";
       public const string ModelType = "ModelType";

       public const string Shape_RoadLevel = "道路分级";
       /// <summary>
       /// 行政区图层行政区代码字段名称
       /// </summary>
       public const string Shape_RegionCode = "StreetId";
       /// <summary>
       /// 道路线图层道路编码字段名称
       /// </summary>
       public const string Shape_RoadCode = "道路编号";
   }
}
