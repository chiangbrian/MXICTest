using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MXIC_PCCS.DataUnity.Interface;
using MXIC_PCCS.DataUnity.BusinessUnity;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class ScheduleSettingController : Controller
    {
        IScheduleSetting _IScheduleSetting = new ScheduleSetting();

        // GET: ScheduleSetting
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public string ScheduleList(DateTime? StartTime, DateTime? EndTime, string PoNo)
        {
            string str = _IScheduleSetting.ScheduleList(StartTime, EndTime, PoNo);
            return str;
        }

        [HttpPost]
        public ActionResult UploadSchedule(HttpPostedFileBase file)
        {
            if (file!=null)
            {
                string Result = _IScheduleSetting.ImportSchedul(file);
                TempData["message"] = Result;
                return RedirectToAction("Index", "ScheduleSetting");
            }
            else
            {
                StringBuilder SB = new StringBuilder();
                string responseStr = "未選擇檔案!";
                SB.Clear();
                SB.AppendFormat("<script>alert('{0}');window.location.href='../ScheduleSetting/Index';</script>", responseStr);
                return Content(SB.ToString());
            }
        }

        public ActionResult DownloadScheduleExample()
        {
            StringBuilder SB = new StringBuilder();
            try
            {
                string filepath = Server.MapPath("~/Content/出勤班表範本.xlsx");
                string filename = Path.GetFileName(filepath);
                Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(iStream, "application/xlsx", filename);
            }
            catch (Exception ex)
            {
                SB.Clear();
                SB.AppendFormat("<script>alert('下載失敗!');window.location.href='../ScheduleSetting/Index';</script>");
                return Content(SB.ToString());
            }
        }

        public ActionResult ExportSchedule(string PoNo, string Date)
        {
            if(!string.IsNullOrEmpty(PoNo) && !string.IsNullOrEmpty(Date))
            {
                //"2020", "09", "4500090268"
                MemoryStream OutputStream = _IScheduleSetting.ExportSchedul(Date.Split('-')[0], Date.Split('-')[1], PoNo);
                return File(OutputStream, "application/pdf", "班表產出.pdf");
                //return File(OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "班表產出.xlsx");
            }
            else
            {
                TempData["message"] = "請填入PoNo及年月";
                return RedirectToAction("Index", "ExportPO");
            }
        }

        public string DelSchedule(string ScheduleDate,string SchedulePoNo)
        {
            string str = _IScheduleSetting.DelSchedule(ScheduleDate, SchedulePoNo);
            return str;
        }
    }
}