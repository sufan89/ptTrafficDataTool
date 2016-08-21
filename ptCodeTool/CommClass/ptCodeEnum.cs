using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptCodeTool
{
    class ptCodeEnum
    {
    }
    /// <summary>
    /// 编码类型
    /// </summary>
    public enum CodeType
    {
        SzRoadCode = 1,
        FsRoadCode = 2,
        FsFacilityCode = 3
    }
    /// <summary>
    /// 模型类型
    /// </summary>
    public enum ModelType
    {
        RoadLevel = 1, //道路等级类型
        RoadFacility = 2 //道路设施类型
    }
    /// <summary>
    /// 道路图层类型
    /// </summary>
    public enum RoadeType
    {
        facility=1,//道路设施
        roadformation=2,//道路面
        roadline=3,//道路中心线
        district=4 //行政区
    }
}
