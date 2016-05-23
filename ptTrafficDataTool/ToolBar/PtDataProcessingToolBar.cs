using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace ptTrafficDataTool
{
    /// <summary>
    /// Summary description for PtDataProcessingToolBar.
    /// </summary>
    [Guid("61eda296-c939-4d99-95e3-f13e52c70dcb")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ptTrafficDataTool.ToolBar.PtDataProcessingToolBar")]
    public sealed class PtDataProcessingToolBar : BaseToolbar
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public PtDataProcessingToolBar()
        {
            //这里可以考虑使用配置文件进行动态加载
            ptDbBase.ptPathManag.toolStartPath = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            AddItem("{5471b940-251d-4952-8911-a426f646e598}");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "数据处理工具";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "PtDataProcessingToolBar";
            }
        }
    }
}