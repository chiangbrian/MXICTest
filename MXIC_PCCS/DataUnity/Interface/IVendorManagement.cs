using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MXIC_PCCS.DataUnity.Interface
{
    interface IVendorManagement
    {
        /// <summary>
        /// 廠商人員
        /// </summary>
        /// <param name="VenID"></param>
        /// <param name="PoNo"></param>
        /// <param name="VendorName"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <param name="DeleteVenID"></param>
        /// <param name="EditVenID"></param>
        /// <returns></returns>
        string VendorList(string PoNo, string VendorName, string EmpID, string EmpName,string Shifts);
        /// <summary>
        /// 新增廠商人員
        /// </summary>
        /// <param name="VenID"></param>
        /// <param name="PoNo"></param>
        /// <param name="VendorName"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <param name="DeleteVenID"></param>
        /// <param name="EditVenID"></param>
        /// <returns></returns>
        string AddVendor(string PoNo, string VendorName, string EmpID, string EmpName, string Shifts);
        /// <summary>
        /// 刪除廠商人員
        /// </summary>
        /// <param name="VenID"></param>
        /// <param name="PoNo"></param>
        /// <param name="VendorName"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <param name="DeleteVenID"></param>
        /// <param name="EditVenID"></param>
        /// <returns></returns>
        string DeleteVendor(string DeleteID);
        /// <summary>
        /// 修改廠商人員
        /// </summary>
        /// <param name="VenID"></param>
        /// <param name="PoNo"></param>
        /// <param name="VendorName"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <param name="DeleteVenID"></param>
        /// <param name="EditVenID"></param>
        /// <returns></returns>
        string EditVendor(string EditID, string PoNo, string VendorName, string EmpID, string EmpName, string Shifts);
        /// <summary>
        /// 修改廠商人員細節
        /// </summary>
        /// <param name="EditID"></param>
        /// <returns></returns>
        string EditVendorDetail(string EditID);

        string Shifts();
    }
}

