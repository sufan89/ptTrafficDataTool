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

        public void Coding(Dictionary<string, ptCodeFeautreLayer> dicCodeLayer)
        {
            throw new NotImplementedException();
        }
    }
}
