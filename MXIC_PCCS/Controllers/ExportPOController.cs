using MXIC_PCCS.DataUnity.BusinessUnity;
using MXIC_PCCS.DataUnity.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace MXIC_PCCS.Controllers
{
    [Authorize(Roles = "true,SuperAdmin")]
    //兩個以上權限使用,分開
    public class ExportPOController : Controller
    {
        IExportPO _ExportPO = new ExportPO();

        // GET: Test
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult DownloadQuotation(string PONumber, string Month)
        {
             string responseStr; 
             StringBuilder SB = new StringBuilder();

            if (string.IsNullOrWhiteSpace(PONumber) || string.IsNullOrWhiteSpace(Month))
            {
                responseStr = "欄位未填!";
                SB.Clear();
                SB.AppendFormat("<script>alert('{0}');window.location.href='../ExportPO/Index';</script>", responseStr);
            }
            else
            {
                try
                {
                    System.IO.File.Copy(ConfigurationManager.AppSettings["ExampleDirectory"], ConfigurationManager.AppSettings["DowloadDirectory"], true);
                    responseStr = ExportPO(PONumber, Month);
                    if (responseStr == "寫入成功")
                    {
                        string filepath = Server.MapPath("~/Content/PDF/計價單.pdf");
                        string filename = Path.GetFileName(filepath);
                        Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        return File(iStream, "application/pdf", filename);
                    }
                    else
                    {
                        SB.Clear();
                        SB.AppendFormat("<script>alert('{0}');window.location.href='../ExportPO/Index';</script>", responseStr);
                    }
                }
                catch (Exception ex)
                {
                    responseStr = "匯出失敗!";
                    SB.Clear();
                    SB.AppendFormat("<script>alert('{0}');window.location.href='../ExportPO/Index';</script>", responseStr);
                }
            }           
            return Content(SB.ToString());
        }

        public string ExportPO(string PONumber, string Month)
        {
            var responseStr = _ExportPO.CalcuationPO(PONumber, Convert.ToDateTime(Month));
            return responseStr;
        }

        public ActionResult ExportSchedule()
        {
            return View();
        }
    }
}