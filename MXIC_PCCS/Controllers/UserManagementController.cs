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
    public class UserManagementController : Controller
    {
        IUserManagement _IUserManagement = new UserManagement();
        // GET: UserManagement
        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.Name;
            ViewBag.ID = id;
            return View();
        }
        //查詢使用者資料
        public string UserList(string DepNo, string DepName, string UserID, string UserName)
        {               
            //呼叫IUserManagement介面中的 UserList方法
            string str = _IUserManagement.UserList( DepNo , DepName,  UserID , UserName);

            return str ;
        }
        //人員管理刪除使用者
        public string DeleteUser(string DeleteID)
        {
            //呼叫IUserManagement介面中的 DeleteUser方法
            string str = _IUserManagement.DeleteUser(DeleteID);

            return str;
        }
        //新增使用者
        public string AddUser(string DepNo, string DepName, string UserID, string UserName, string Admin, string PassWord)
        {
            //呼叫IUserManagement介面中的 AddUser方法
            string str = _IUserManagement.AddUser( DepNo,  DepName,  UserID,  UserName,  Admin,  PassWord);

            return str;
        }
        //編輯時顯示使用者的詳細資料
        public string EditUserDetail(string EditID)
        {
            //呼叫IUserManagement介面中的 EditUserDetail方法
            string str = _IUserManagement.EditUserDetail(EditID);

            return str;
        }
        //編輯使用者
        public string EditUser(string EditID, string DepNo, string DepName, string UserID, string UserName, string Admin, string PassWord)
        {
            //呼叫IUserManagement介面中的 EditUser方法
            string str = _IUserManagement.EditUser(EditID, DepNo, DepName, UserID, UserName, Admin, PassWord);

            return str;
        }
    }
}