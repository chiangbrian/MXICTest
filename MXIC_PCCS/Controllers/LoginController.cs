using MXIC_PCCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MXIC_PCCS.Controllers
{
    public class LoginController : Controller
    {
        public PCCSContext _db = new PCCSContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            string Hash = GetSHA1.GetSHA1Hash(Password);

            UserManagement UserData = _db.MXIC_UserManagements.Where(x => x.UserID.ToLower() == Account.ToLower() && x.PassWord == Hash).FirstOrDefault();

            if (UserData == null)
            {   
                // 找不到這一筆記錄（帳號與密碼有錯，沒有這個會員）
                //return HttpNotFound();
                ViewData["ErrorMessage"] = "帳號或密碼有錯";
                return View();
            }
            else
            {   //*************************************************************(start)
                // https://dotblogs.com.tw/mickey/2017/01/01/154812 
                // https://dotblogs.com.tw/mis2000lab/2014/08/01/authentication-mode-forms_web-config
                // https://blog.miniasp.com/post/2008/06/11/How-to-define-Roles-but-not-implementing-Role-Provider-in-ASPNET.aspx
                // http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
                DateTime DTnow = DateTime.Now;

                // 以下需要搭配 System.Web.Security 命名空間。                
                var authTicket = new FormsAuthenticationTicket(    // 登入成功，取得門票 (票證)。請自行填寫以下資訊。
                    version: 1,                                    //版本號（Ver.）
                    name: UserData.UserListID.ToString(),          // ***自行放入資料（如：使用者帳號、真實名稱）

                    issueDate: DTnow,                              // 登入成功後，核發此票證的本機日期和時間（資料格式 DateTime）
                    expiration: DTnow.AddDays(1),                  //  "一天"內都有效（票證到期的本機日期和時間。）
                    isPersistent: true,                            // 記住我？ true or false（畫面上通常會用 CheckBox表示）

                    userData: UserData.Admin,                      // ***自行放入資料（如：會員權限、等級、群組） 
                                                                   // 與票證一起存放的使用者特定資料。
                                                                   // 需搭配 Global.asax設定檔 - Application_AuthenticateRequest事件。

                    cookiePath: FormsAuthentication.FormsCookiePath
                    );

                // *** 把上面的 ticket資訊 "加密"  ****** 
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket))
                {                        // 重點！！避免 Cookie遭受攻擊、盜用或不當存取。請查詢關鍵字「」。
                    HttpOnly = true      // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取

                    //HttpOnly = true,  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取
                    //Secure = true;    // 需要搭配https（SSL）才行。
                };
                if (authTicket.IsPersistent)
                {
                    authCookie.Expires = authTicket.Expiration;    // Cookie過期日（票證到期的本機日期和時間。）
                }
                Response.Cookies.Add(authCookie);                  // 完成 Cookie，寫入使用者的瀏覽器與設備中
                //*************************************************************(end)

                return RedirectToAction("Index", "Home");

                // 完成這個範例以後，您可以參考這篇文章 - OWIN Forms authentication（作法很類似）
                // https://blogs.msdn.microsoft.com/webdev/2013/07/03/understanding-owin-forms-authentication-in-mvc-5/
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();  // 登出
            Session.Abandon();              // 不光是清除 Session裡面的資料，把 Session也取消了！

            //// 參考資料 http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
            //// 建立一個同名的 Cookie 來覆蓋原本的 Cookie
            //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")   
            //{
            //    Expires = DateTime.Now.AddYears(-1)   // 設定過期日（已過期一年）
            //};
            //Response.Cookies.Add(cookie1);

            //// 建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            //HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "")   
            //{
            //    Expires = DateTime.Now.AddYears(-1)   // 設定過期日（已過期一年）
            //};
            //Response.Cookies.Add(cookie2);

            return RedirectToAction("Login");
            // 回到 登入畫面（Login動作）
        }

        public ActionResult Permissiodenied()
        {
            return View();
        }
    }
}