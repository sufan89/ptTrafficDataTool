using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace ptCodeTool
{
    public partial class FrmRoadCode : Form
    {
        public FrmRoadCode(IMap pMap)
        {
            InitializeComponent();
            m_MainMap = pMap;
        }

        IMap m_MainMap = null;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (m_MainMap != null)
            {
                //初始化下拉框
                for (int i = 0; i < m_MainMap.LayerCount; i++)
                {
                    ILayer pLayer = m_MainMap.get_Layer(i);
                    if (pLayer is IFeatureLayer&&(pLayer as IFeatureLayer).FeatureClass.ShapeType==esriGeometryType.esriGeometryPolyline)
                    {
                        cbRoad.Items.Add(pLayer.Name);
                    }
                    else if (pLayer is IFeatureLayer && (pLayer as IFeatureLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        cbRegion.Items.Add(pLayer.Name);
                    }
                }
                if(cbRoad.Items.Count>0)
                    cbRoad.SelectedIndex = 0;
                if (cbRegion.Items.Count > 0)
                    cbRegion.SelectedIndex = 0;
            }
        }

        private void rdoSelectRoadLine_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSelectRoadLine.Checked)
            {
                cbRoad.Items.Clear();
                if (m_MainMap != null)
                {
                    //初始化下拉框
                    for (int i = 0; i < m_MainMap.LayerCount; i++)
                    {
                        ILayer pLayer = m_MainMap.get_Layer(i);
                        if (pLayer is IFeatureLayer && (pLayer as IFeatureLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            cbRoad.Items.Add(pLayer.Name);
                        }
                    }
                }
            }
        }

        private void rdoSelectRoadPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSelectRoadPolygon.Checked)
            {
                cbRoad.Items.Clear();
                if (m_MainMap != null)
                {
                    //初始化下拉框
                    for (int i = 0; i < m_MainMap.LayerCount; i++)
                    {
                        ILayer pLayer = m_MainMap.get_Layer(i);
                        if (pLayer is IFeatureLayer && (pLayer as IFeatureLayer).FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            cbRoad.Items.Add(pLayer.Name);
                        }
                    }
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //开始进行编码
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbRoad.SelectedIndex < 0)
            {
                MessageBox.Show("请选择编码道路");
                return;
            }
            //if (cbRegion.SelectedIndex < 0)
            //{
            //    MessageBox.Show("请选择行政区图层");
            //}
            if (m_MainMap == null && m_MainMap.LayerCount == 0)
            {
                MessageBox.Show("请先加载编码图层");
                return;
            }
            IFeatureLayer pRoadLayer=null;
            IFeatureLayer pRegionLayer=null;
            string strRoadLayerName = cbRoad.SelectedItem.ToString();
            //string strRegionLayerName =cbRegion.SelectedItem.ToString();
            for (int i = 0; i < m_MainMap.LayerCount; i++)
            {
                if (m_MainMap.get_Layer(i).Name == strRoadLayerName)
                {
                    pRoadLayer = m_MainMap.get_Layer(i) as IFeatureLayer;
                }
                //if (m_MainMap.get_Layer(i).Name == strRegionLayerName)
                //{
                //    pRegionLayer = m_MainMap.get_Layer(i) as IFeatureLayer;
                //}
            }
            IRoadCode DoRoadCode = new RoadCode();
            DoRoadCode.Coding(pRoadLayer);
            MessageBox.Show("编码完成!");
            
        }
    }
}
