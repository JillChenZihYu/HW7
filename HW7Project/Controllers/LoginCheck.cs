using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HW7Project.Controllers
{
    public class LoginCheck:ActionFilterAttribute
    {
        void LoginState(HttpContext context) 
        {
            /*Session作為判斷是否為管理員的狀態管理，可全域使用，生命週期為瀏覽器關掉為止，=user為管理員狀態，=null為非管理員狀態*/
            if (context.Session["user"] == null)  //如果非管理員狀態的話，不能讓使用者進入HomeManager的頁面
            {
                context.Response.Redirect("/Home/Index"); //回到前台Home的Index頁面
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        
        {
            HttpContext context = HttpContext.Current;
            LoginState(context);
        }

    }
}