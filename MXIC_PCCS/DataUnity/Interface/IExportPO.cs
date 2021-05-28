using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IExportPO
    {
        //呼叫FUNCTION
       string CalcuationPO(string PoNo, DateTime Date);
    }
}
