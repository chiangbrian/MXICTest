using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MXIC_PCCS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //**************************************************************************
        //*** 搭配 LoginController。登入與權限控管。
        //      需搭配  System.Web.Security 與 System.Security.Principal命名空間。
        //
        //      Global.asax文件裡面的事件 -- https://dotblogs.com.tw/mis2000lab/2008/04/28/3526
        //**************************************************************************
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {   // 在安全模組建立起當前用戶的有效的身份時，AuthenticateRequest事件被觸發。在這個時候，用戶的憑據將會被驗證。
            // 另一個很類似，不要搞錯。 Application_AuthorizeRequest：當安全模組確認一個用戶可以訪問資源之後，該事件被觸發。

            if (HttpContext.Current.User == null) return;   // 不符合規則的都踢出去！
            if (HttpContext.Current.User.Identity.IsAuthenticated == false) return;
            if (Request.IsAuthenticated == false) return;

            // 先取得登入者的身份識別 -- FormsIdentity。  需搭配 System.Web.Security命名空間
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
            // 再取出使用者的票證 (authTicket) -- FormsAuthenticationTicket
            FormsAuthenticationTicket authTicket = id.Ticket;

            // 取出票證 (authTicket)中的 "角色" 並轉成字串陣列 string[]。
            //// 如果登入者是「多重的」角色，例如 1,3,5。這樣的安排可以寫下面的程式來擷取。
            string[] arrRoles = authTicket.UserData.Split(',');

            // 指派 "角色" 到目前這個 HttpContext 的 User物件
            // ......當初建立票證 (authTicket)時，UserData屬性裡面放了什麼資訊（如：會員權限、等級、群組） 
            // ......然後把資料放入 HttpContext 的 User物件裡面
            HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, arrRoles);
            // GenericPrincipal 需搭配 System.Security.Principal命名空間


            //      https://dotblogs.com.tw/mickey/2017/01/01/154812
            //      https://blog.miniasp.com/post/2008/06/11/How-to-define-Roles-but-not-implementing-Role-Provider-in-ASPNET.aspx
            //      http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
        }
    }
}
