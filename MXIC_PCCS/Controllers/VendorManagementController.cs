using MXIC_PCCS.DataUnity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MXIC_PCCS.DataUnity.BusinessUnity;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class VendorManagementController : Controller
    {
        IVendorManagement _IVendorManagement = new VendorManagement();
        
        // GET: VendorManagement
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public string VendorList(string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            var str = _IVendorManagement.VendorList(PoNo, VendorName, EmpID, EmpName,Shifts);

            return str;
        }

        public string AddVendor(string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            string str = _IVendorManagement.AddVendor(PoNo, VendorName, EmpID, EmpName, Shifts);

            return str;
        }

        public string DeleteVendor(string DeleteID)
        {
            string str = _IVendorManagement.DeleteVendor(DeleteID);

            return str;
        }

        public string EditVendor(string EditID, string PoNo, string VendorName, string EmpID, string EmpName, string Shifts)
        {
            string str = _IVendorManagement.EditVendor(EditID, PoNo, VendorName, EmpID, EmpName, Shifts);

            return str;
        }

        public string EditVendorDetail(string EditID)
        {
            string str = _IVendorManagement.EditVendorDetail(EditID);

            return (str);
        }

        /// <summary>
        /// 班別List
        /// </summary>
        /// <returns></returns>
        public string Shifts()
        {
            string str = _IVendorManagement.Shifts();

            return (str);
        }
    }
}