using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using System.IO;
using ptRoadCodeConfig;
using ESRI.ArcGIS.Geodatabase;

namespace ptCodeTool
{
    public partial class FrmMain : Form
    {
        public FrmMain(IMap pMap)
        {
            InitializeComponent();
            m_MainMap = pMap;
        }
        private IMap m_MainMap;
        private CodeType m_RoadCodeType = CodeType.SzRoadCode;
        private DataTable m_DtLayerConfig=null;
        private DataTable m_DtCodeRule=null;
        private ptCodeTool.RefreshLogEventHandle RoadCodeLog;
        /// <summary>
        /// 开始编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoCoding_Click(object sender, EventArgs e)
        {
            //清空日志信息
            txtLog.Text = string.Empty;

            if (m_MainMap == null)
            {
                RefreshLog(string.Format("无法获取地图文档"));
                return;
            }
            else if (m_MainMap.LayerCount <= 0)
            {
                RefreshLog(string.Format("当前地图未加载任何图层"));
                return;
            }
            //读取配置信息
            ReadDbConfig();
            //根据编码类型获取相应的配置信息，并进行编码
            if (m_DtCodeRule == null || m_DtLayerConfig == null) return;
            string RowFilter = "";
            switch (m_RoadCodeType)
            {
                case CodeType.SzRoadCode:
                    RowFilter = string.Format("{0}='{1}'", ptColumnName.CodeType, Enum.GetName(typeof(CodeType),CodeType.SzRoadCode));
                    break;
                case CodeType.FsRoadCode:
                    RowFilter = string.Format("{0}='{1}'", ptColumnName.CodeType, Enum.GetName(typeof(CodeType), CodeType.FsRoadCode));
                    break;
                case CodeType.FsFacilityCode:
                    RowFilter = string.Format("{0}='{1}'", ptColumnName.CodeType, Enum.GetName(typeof(CodeType), CodeType.FsRoadCode));
                    break;
            }
            DataRow[] CodeLayers = m_DtCodeRule.Select(RowFilter);
            if (CodeLayers.Length <= 0)
            {
                RefreshLog(string.Format("当前未配置编码图层"));
            }
            Dictionary<string, ptCodeFeautreLayer> pCodeLayers = new Dictionary<string, ptCodeFeautreLayer>();
            //查找编码图层
            for (int i = 0; i < CodeLayers.Length; i++)
            {
                string StrLayerName = CodeLayers[i][ptColumnName.CodeLayer].ToString();
                IFeatureLayer pTempLayer = GetLayerByName(StrLayerName);
                if (pTempLayer != null)
                {
                    DataRow[] pTempRows = m_DtLayerConfig.Select(string.Format("{0}='{1}'",ptColumnName.LayerName, StrLayerName));
                    if (pTempRows.Length <= 0)
                    {
                        RefreshLog(string.Format("未能加载图层:【{0}】相关配置信息", StrLayerName));
                        return;
                    }
                    ptCodeFeautreLayer pptLayer = new ptCodeFeautreLayer();
                    pptLayer.CodeLayer = pTempLayer;
                    pptLayer.LayerName = StrLayerName;
                    pptLayer.RoadType = pTempRows[0][ptColumnName.RoadType].ToString();
                    pptLayer.CodeField = pTempRows[0][ptColumnName.CodeField].ToString();
                    pCodeLayers.Add(StrLayerName, pptLayer);
                }
                else
                {
                    RefreshLog(string.Format("当前地图未加载图层:{0}", StrLayerName));
                    return;
                }
            }

            ptRoadCodeFactory RoadCodeFac = new ptRoadCodeFactory();
            IRoadCode pRoadCode = RoadCodeFac.GetRoadCodeClass(m_RoadCodeType);
            if (pRoadCode != null)
            {
             
                if (RoadCodeLog == null)
                {
                    RoadCodeLog = RefreshLog;
                    pRoadCode.Coding(pCodeLayers, RoadCodeLog);
                }
            }

        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveLogDia = new SaveFileDialog();
            saveLogDia.Filter = "*.txt|*.txt";
            saveLogDia.FileName = "编码日志.txt";
            if (saveLogDia.ShowDialog() != DialogResult.OK) return;
            string saveFileName = saveLogDia.FileName;
            if (System.IO.File.Exists(saveFileName))
            {
                if (MessageBox.Show("当前文件已存在，是否替换?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    try
                    {
                        System.IO.File.Delete(saveFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除日志文件失败:"+ex.Message,"提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }
                else { return; }
            }
            //将日志文件保存到指定的目录
            try
            {
                StreamWriter SW;
                SW = File.CreateText(saveFileName);
                SW.WriteLine(txtLog.Text);
                SW.Close();
                MessageBox.Show("成功保存日志", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除日志文件失败:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshLog(string Strmessage)
        {
            if (string.IsNullOrEmpty(txtLog.Text))
            {
                txtLog.Text =string.Format("{0}: ",DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss"))+ Strmessage;
            }
            else
            {
                txtLog.Text = txtLog.Text + System.Environment.NewLine + string.Format("{0}: ",DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss")) + Strmessage;
            }
            this.txtLog.Refresh();
        }
        /// <summary>
        /// 读取配置信息
        /// </summary>
        private void ReadDbConfig()
        {
            //读取配置信息
            RefreshLog(string.Format("读取图层配置信息"));
            ptDataPool pDataPool = new ptDataPool();
            m_DtLayerConfig = pDataPool.LayerConfigDt;
            if (m_DtLayerConfig == null || m_DtLayerConfig.Rows.Count <= 0)
            {
                RefreshLog(string.Format("获取图层配置失败"));
            }
            m_DtCodeRule = pDataPool.CodeRuleDt;
            if (m_DtCodeRule == null || m_DtCodeRule.Rows.Count <= 0)
            {
                RefreshLog(string.Format("获取编码规则失败"));
            }
        }
        /// <summary>
        /// 根据图层名称查找图层
        /// </summary>
        /// <param name="pLayerName"></param>
        /// <returns></returns>
        private IFeatureLayer GetLayerByName(string pLayerName)
        {
            IFeatureLayer pFeatureLayer = null;
            for (int i = 0; i < m_MainMap.LayerCount; i++)
            {
                IFeatureLayer pLayer = m_MainMap.get_Layer(i) as IFeatureLayer;
                if (pLayer !=null)
                {
                    IDataset pDs=pLayer.FeatureClass as IDataset;
                    if (pDs != null && pDs.Name == pLayerName)
                    {
                        pFeatureLayer = pLayer;
                        break;
                    }
                }
            }
            return pFeatureLayer;
        }

        #region 设置编码类型
        private void rbSzRoadCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSzRoadCode.Checked)
            {
                m_RoadCodeType = CodeType.SzRoadCode;
            }
        }
        private void rbFsRoadCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFsRoadCode.Checked)
            {
                m_RoadCodeType = CodeType.FsRoadCode;
            }
        }
        private void rbFsFacilityCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFsFacilityCode.Checked)
            {
                m_RoadCodeType = CodeType.FsFacilityCode;
            }
        }
        #endregion
    }
    /// <summary>
    /// 编码类型
    /// </summary>
    public enum CodeType
    {
          SzRoadCode=1,
          FsRoadCode=2,
          FsFacilityCode=3
    }
}
