using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace ptCodeTool
{
    public delegate void RefreshLogEventHandle(string message);

    interface IRoadCode
    {
        //void Coding(IFeatureLayer pRoadLayer,IFeatureLayer pRegionLayer);
        //void Coding(IFeatureLayer pRoadLayer);
        void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer,RefreshLogEventHandle RefreshLog);
    }
}
