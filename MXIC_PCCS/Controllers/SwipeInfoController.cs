using MXIC_PCCS.DataUnity.BusinessUnity;
using MXIC_PCCS.DataUnity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class SwipeInfoController : Controller
    {
        ISwipeInfo _ISwipeInfo = new SwipeInfo();

        // GET: SwipeInfo
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Alarm()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public string CheckinList(string VendorName, string EmpID, string EmpName, DateTime? StartTime, DateTime? EndTime, string AttendTypeSelect)
        {
            string str = _ISwipeInfo.CheckinList(VendorName, EmpID, EmpName, StartTime, EndTime, AttendTypeSelect);

            return str;
        }

        public string AlarmList(string PoNo, string VendorName, string EmpName, DateTime? StartTime, DateTime? EndTime, string CheckType)
        {
            string str = _ISwipeInfo.AlarmList(PoNo, VendorName, EmpName, StartTime, EndTime, CheckType);

            return str;
        }

        public string SwipeInfoDetail(string EditID)
        {
            string str = _ISwipeInfo.SwipeInfoDetail(EditID);

            return str;
        }
        public string EditSwipe(string EditID, string AttendTypeSelect, string Hour)
        {
            string str = _ISwipeInfo.EditSwipe(EditID, AttendTypeSelect, Hour);

            return str;
        }

        public string InsertSwipe(string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, float Hour, string WorkShift)
        {
            string str = _ISwipeInfo.InsertSwipe(AttendTypeSelect, CheckType, EmpName, SwipeTime, Hour, WorkShift);

            return str;
        }  
        //20210514

        //public string InsertSwipe(string PoNoSelect, string AttendTypeSelect, string CheckType, string EmpName, DateTime SwipeTime, float Hour, string WorkShift)
        //{
        //    string str = _ISwipeInfo.InsertSwipe(PoNoSelect,AttendTypeSelect, CheckType, EmpName, SwipeTime, Hour, WorkShift);

        //    return str;
        //}

        public string MxicTest()
        {
            string str = _ISwipeInfo.transform();

            return str;
        }

        public ActionResult transform(string StartTime, string EndTime, string PoNo)
        {
            string responseStr = "日期選擇不完整!";

            StringBuilder SB = new StringBuilder();

            //如果開始日不等於空&結束日不等空
            if (!string.IsNullOrWhiteSpace(StartTime) && !string.IsNullOrWhiteSpace(EndTime))
            {
                DateTime start = Convert.ToDateTime(StartTime);

                DateTime end = Convert.ToDateTime(EndTime);

                //判斷日期區間是否正確
                var result = DateTime.Compare(start, end);

                if (result != 1)
                {
                    _ISwipeInfo.transform2(StartTime, EndTime, PoNo);

                    return RedirectToAction("Index", "SwipeInfo");
                }
                else
                {
                    responseStr = "日期選擇異常!";

                    SB.Clear();
                    SB.AppendFormat("<script>alert('{0}');window.location.href='../ScheduleSetting/Index';</script>", responseStr);

                    return Content(SB.ToString());
                }
            }
            //如果開始日或結束日等於空
            else
            {
                if (!string.IsNullOrWhiteSpace(PoNo) && _ISwipeInfo.CheckPO(PoNo))
                {
                    _ISwipeInfo.transform2("", "", PoNo);
                    return RedirectToAction("Index", "SwipeInfo");
                }

                responseStr = "輸入PO不正確!";

                SB.Clear();
                SB.AppendFormat("<script>alert('{0}');window.location.href='../ScheduleSetting/Index';</script>", responseStr);

                return Content(SB.ToString());
            }
        }
    }
}