using MXIC_PCCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface ILisenceManagement
    {
        string SearchLisence(string PoNo, string EmpName, string LicName);

        string AddLisence(string PoNo, string EmpName, string LicName, DateTime EndDate);

        string EditLisence(string EditID, string PoNo, string EmpName, string LicName, DateTime EndDate);

        string EditLisenceDetail(string EditID);

        string DeleteLisence(string DeleteID);

        string ImportLisence(string PoNo, List<LisenceProperty> Property_ListModel);

        string ClearTable(string PoNo);
    }
}
