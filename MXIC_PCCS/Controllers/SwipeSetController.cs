using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using MXIC_PCCS.DataUnity.BusinessUnity;
using MXIC_PCCS.DataUnity.Interface;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class SwipeSetController : Controller
    {
        ISwipeSet _ISwipeSet = new SwipeSet();

        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public string UserList(string PoNo, DateTime? Date)
        {
            string str = _ISwipeSet.UserList(PoNo, Date);

            return str;
        }

        public string EditTimeDetail(string EditID)
        {
            string str = _ISwipeSet.EditTimeDetail(EditID);

            return str;
        }

        public string EditTime(string EditID, string SwipeStartTime, string SwipeEndTime)
        {
            string str = _ISwipeSet.EditTime(EditID, SwipeStartTime, SwipeEndTime);

            return str;
        }
    }
}