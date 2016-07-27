using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace ptCodeTool
{
    interface IRoadCode
    {
        void Coding(IFeatureLayer pRoadLayer,IFeatureLayer pRegionLayer);
        void Coding(IFeatureLayer pRoadLayer);
    }
}
