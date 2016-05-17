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

       private DataTable m_ModelDt = null;
       public DataTable ModelDt
       {
           get {
               m_ModelDt = pConfig.GetTableByName(ptTableName.T_Model);
               return m_ModelDt;
           }
       }
    }
}
