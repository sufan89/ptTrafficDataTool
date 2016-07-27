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
       #region T_Model
       public const string ModelName = "ModelName";
       public const string ModelCode = "ModelCode";
       public const string ModelType = "ModelType";
       /// <summary>
       /// 起始编码
       /// </summary>
       public const string StartCode = "StartCode";
       /// <summary>
       /// 最后编码值
       /// </summary>
       public const string LastCode = "LastCode";
       /// <summary>
       /// 最大编码值
       /// </summary>
       public const string MaxCode = "MaxCode";
       #endregion
       public const string Shape_RoadLevel = "道路分级";
       /// <summary>
       /// 行政区图层行政区代码字段名称
       /// </summary>
       public const string Shape_RegionCode = "StreetId";
       /// <summary>
       /// 道路线图层道路编码字段名称
       /// </summary>
       public const string Shape_RoadCode = "道路编号";
       /// <summary>
       /// 道路线图层道路编码字段名称
       /// </summary>
       public const string Shape_RoadCode_SZ = "RoadCode";
       /// <summary>
       /// 深圳路网道路等级字段名称
       /// </summary>
       public const string Shape_RoadLevel_SZ = "道路等级";
   }
}
