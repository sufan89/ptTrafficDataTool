using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;

namespace ptCodeTool
{
    class ptCodeFeautreLayer
    {
        private IFeatureLayer m_CodeLayer;
        /// <summary>
        /// 数据图层
        /// </summary>
        public IFeatureLayer CodeLayer
        {
            get {
                return m_CodeLayer;
            }
            set { m_CodeLayer = value; }
        }

        private string m_LayerName;
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName
        {
            get { return m_LayerName; }
            set{m_LayerName=value;}
        }

        private string m_RoadType;
        /// <summary>
        /// 图层类型
        /// </summary>
        public string RoadType
        {
            get { return m_RoadType; }
            set { m_RoadType = value; }
        }

        private string m_CodeField;
        /// <summary>
        /// 编码字段
        /// </summary>
        public string CodeField
        {
            get { return m_CodeField; }
            set { m_CodeField = value; }
        }
    }
}
