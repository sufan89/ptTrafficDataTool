using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ptCodeTool
{
    class ptRoadCodeFactory
    {
        public ptRoadCodeFactory()
        {

        }
        public IRoadCode GetRoadCodeClass(CodeType pCodeType)
        {
            IRoadCode pRoadCode = null;
            switch (pCodeType)
            {
                case CodeType.SzRoadCode:
                    pRoadCode = new szRoadCode();
                    break;
                case CodeType.FsRoadCode:
                    pRoadCode = new FsRoadCode();
                    break;
                case CodeType.FsFacilityCode:
                    pRoadCode = new FsFacilityCode();
                    break;
            }
            return pRoadCode;
        }
    }
}
