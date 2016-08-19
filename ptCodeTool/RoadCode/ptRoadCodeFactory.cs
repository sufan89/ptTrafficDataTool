using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptCodeTool
{
    class ptRoadCodeFactory
    {
        public ptRoadCodeFactory(RefreshLogEventHandle RefreshLog)
        {
            if (RefreshLog != null)
            {
                m_RefreshLog = RefreshLog;
            }
        }
        private RefreshLogEventHandle m_RefreshLog;

        public IRoadCode GetRoadCodeClass(CodeType pCodeType)
        {
            IRoadCode pRoadCode = null;
            switch (pCodeType)
            {
                case CodeType.SzRoadCode:
                    pRoadCode = new szRoadCode(m_RefreshLog);
                    break;
                case CodeType.FsRoadCode:
                    pRoadCode = new FsRoadCode(m_RefreshLog);
                    break;
                case CodeType.FsFacilityCode:
                    pRoadCode = new FsFacilityCode(m_RefreshLog);
                    break;
            }
            return pRoadCode;
        }
    }
}
