using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptCodeTool
{
    class FsFacilityCode:IRoadCode
    {
        public FsFacilityCode(RefreshLogEventHandle RefreshLog)
        {
            if (RefreshLog != null)
            {
                m_RefreshLog = RefreshLog;
            }
        }
        private RefreshLogEventHandle m_RefreshLog;
        /// <summary>
        /// 佛山设施编码
        /// </summary>
        /// <param name="dicCodeLayer"></param>
        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer)
        {
            
        }
    }
}
