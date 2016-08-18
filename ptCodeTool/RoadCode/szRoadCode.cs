using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace ptCodeTool
{
    /// <summary>
    /// 深圳道路编码
    /// </summary>
    class szRoadCode :IRoadCode
    {

        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer)
        {
           
        }

        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer, RefreshLogEventHandle RefreshLog)
        {
            if (dicCodeLayer.Count <= 0)
            {
                RefreshLog(string.Format("未加载任何道路图层配置"));
            }
            RefreshLog(string.Format("11111"));
        }
    }
}
