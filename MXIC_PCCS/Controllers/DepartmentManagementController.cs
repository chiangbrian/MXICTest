using MXIC_PCCS.DataUnity.BusinessUnity;
using MXIC_PCCS.DataUnity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class DepartmentManagementController : Controller
    {
        IDepartmentManagement _IDepartmentManagement = new DepartmentManagement();
        // GET: DepartmentManagement
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public string SearchDepToVen(string DepNo, string DepName)
        {
            string str = _IDepartmentManagement.SearchDepToVen(DepNo, DepName);

            return str;
        }

        public string DeleteDepToVen(string DeleteID)
        {
            string str = _IDepartmentManagement.DeleteDepToVen(DeleteID);

            return str;
        }

        public string Venderdata()
        {
            string str = _IDepartmentManagement.Venderdata();

            return str;
        }

        //[Authorize(Roles = "true,SuperAdmin")]
        public string AddDepToVen(string DepName, string VendorName)
        {
            string str = _IDepartmentManagement.AddDepToVen( DepName,  VendorName);

            return str;
        }
    }
}