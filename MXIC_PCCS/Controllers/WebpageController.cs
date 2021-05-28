using MXIC_PCCS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MXIC_PCCS.Controllers
{
    [Authorize]
    public class WebpageController : Controller
    {
        public PCCSContext _db = new PCCSContext();

        // GET: Webpage
        public ActionResult Index()
        {
            var list = _db.MXIC_InputGenerates;

            return View(list);
        }

        public string PageGenerate(string tablename, string COLUMN_NAME)
        {
            var Admin = HttpContext.User.IsInRole("true");

            var list = _db.MXIC_InputGenerates.Where(x => x.TableName == tablename).OrderBy(x => x.Sequence);

            //if (Admin != true) 
            //{
            //     list = list.Where(x =>x.Admin==false).OrderBy(x => x.Sequence);
            //}

            if (!string.IsNullOrWhiteSpace(COLUMN_NAME))
            {
                list = list.Where(x => x.COLUMN_NAME == COLUMN_NAME).OrderBy(x => x.Sequence);

            }

            string Str = JsonConvert.SerializeObject(list, Formatting.Indented);

            return (Str);
        }

        public string Nav()
        {
            var list = _db.NavDatas.Select(x => new { x.Name, x.Url, x.commonly_used });

            string Str = JsonConvert.SerializeObject(list, Formatting.Indented);

            return (Str);
        }

        public string VendorManagementSelect()
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

            var list = _db.MXIC_VendorManagements.Select(x => new { x.VendorName }).Distinct().OrderBy(x => x.VendorName).ToList();

            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.VendorName,
                    value = item.VendorName
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }


        public string QuotationSelect()
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

            var list = _db.MXIC_Quotations.Select(x => new { x.VendorName }).Distinct().OrderBy(x => x.VendorName).ToList();

            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.VendorName,
                    value = item.VendorName
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }

        public string DepartmentManagementSelect()
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

            var list = _db.MXIC_UserManagements.Where(x => x.UserDisable == true).Select(x => new { x.DepName }).Distinct().OrderBy(x => x.DepName).ToList();

            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.DepName,
                    value = item.DepName
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }

        public string SwipeInfoSelect(string TableName)
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();


            var list = _db.SelectLists.Where(x=>x.TableName == TableName).Select(x => new { x.name }).Distinct().OrderBy(x => x.name).ToList();
            //var list = _db.SwipeInfoSelects.Select(x => new { x.name }).Distinct().OrderBy(x => x.name).ToList();
            //var list = _db.MXIC_LisenceManagements.Select(x => new { x.LicName }).Distinct().OrderBy(x => x.LicName).ToList();//證照名稱SelectList

            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.name,
                    value = item.name
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }

        public string SwipeDoubleCheckTypeSelect(string TableName)
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

            var list = _db.View_Swipe_Doubles.Select(x => new { x.CheckType }).Distinct().OrderBy(x => x.CheckType).ToList();
            //var list = _db.SwipeInfoSelects.Select(x => new { x.name }).Distinct().OrderBy(x => x.name).ToList();


            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.CheckType,
                    value = item.CheckType
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }

        public string SwipPoNoSelect(string TableName) //20210514
        {
            List<SelectViewModel> SelectListx = new List<SelectViewModel>();

            var list = _db.MXIC_View_Swipes.Where(x => x.PoNo == TableName).Select(x => new { x.PoNo }).Distinct().OrderBy(x => x.PoNo).ToList();

            foreach (var item in list)
            {
                var SelectItem = new SelectViewModel()
                {
                    name = item.PoNo,
                    value = item.PoNo
                };
                SelectListx.Add(SelectItem);
            }

            string Str = JsonConvert.SerializeObject(SelectListx, Formatting.Indented);

            return (Str);
        }

        public string marqueeUrl()
        {
            DateTime Today = DateTime.Now.AddDays(100);

            var Maturity = _db.MXIC_LisenceManagements.Where(x => x.EndDate < Today).Select(x => new { x.EmpName, x.LicName, x.EndDate });

            string Str = JsonConvert.SerializeObject(Maturity, Formatting.Indented);

            Str = Str.Replace("T00:00:00", "");

            return (Str);
        }

        public string Admin(string UserID)
        {
            var Admin = _db.MXIC_UserManagements.Where(x => x.UserListID.ToString() == UserID).Select(x => x.Admin).FirstOrDefault().ToString();

            return (Admin);
        }

        public ActionResult _PartialSetting()
        {
            return View();
        }
        [HttpPost]
        public string EditPassword(string UserID ,string Password)
        {
            string Str = "欄位未填";
            if (!string.IsNullOrWhiteSpace(Password))
            {
                try { 
                UserManagement EditPassword = _db.MXIC_UserManagements.Where(x => x.UserListID.ToString() == UserID).FirstOrDefault();
                string hash = GetSHA1.GetSHA1Hash(Password);
                EditPassword.PassWord = hash;
                _db.SaveChanges();
                Str = "修改成功";
                }
                catch(Exception e)
                {

                    Str = e.ToString();

                }
            }


            return (Str);

        }
    }
}