using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ptRoadCodeConfig
{
   public class ptDataPool
    {
       private IConfig pConfig = new MdbConfig();
       /// <summary>
       /// 区域信息
       /// </summary>
       private DataTable m_RegionDt = null;
       private Dictionary<string, string> m_DicRegionCode;
       public Dictionary<string, string> DicRegionCode
       {
           get {
               if (m_DicRegionCode != null)
               {
                   m_DicRegionCode.Clear();
               }
               else
               {
                   m_DicRegionCode = new Dictionary<string, string>();
               }
               m_RegionDt = pConfig.GetTableByName(ptTableName.T_RegionConfig);
               if (m_RegionDt != null)
               {
                   for (int i = 0; i < m_RegionDt.Rows.Count; i++)
                   {
                       m_DicRegionCode.Add(m_RegionDt.Rows[i][ptColumnName.RegionCode].ToString(), m_RegionDt.Rows[i][ptColumnName.RoadRegion].ToString());
                   }
               }
               return m_DicRegionCode;
           }
       }
       /// <summary>
       /// 模型表
       /// </summary>
       private DataTable m_ModelDt = null;
       public DataTable ModelDt
       {
           get {
               m_ModelDt = pConfig.GetTableByName(ptTableName.T_Model);
               return m_ModelDt;
           }
       }
       /// <summary>
       /// 图层配置表
       /// </summary>
       private DataTable m_LayerConfigDt = null;
       public DataTable LayerConfigDt
       {
           get {
               m_LayerConfigDt = pConfig.GetTableByName(ptTableName.T_LayerConfig);
               return m_LayerConfigDt;
           }
       }
       /// <summary>
       /// 编码规则表
       /// </summary>
       private DataTable m_CodeRuleDt = null;
       public DataTable CodeRuleDt
       {
           get {
               m_CodeRuleDt = pConfig.GetTableByName(ptTableName.T_CodeRule);
               return m_CodeRuleDt;
           }
       }
    }
}
