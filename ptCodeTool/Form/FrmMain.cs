using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

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
        /// <summary>
        /// 读取图层配置信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// 开始编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoCoding_Click(object sender, EventArgs e)
        {
            if (m_MainMap == null)
            {

            }
            switch (m_RoadCodeType)
            {
                case CodeType.SzRoadCode:
                    break;
                case CodeType.FsRoadCode:
                    break;
                case CodeType.FsFacilityCode:
                    break;
            }
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLog_Click(object sender, EventArgs e)
        {

        }
        private void RefreshLog(string Strmessage)
        {

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
