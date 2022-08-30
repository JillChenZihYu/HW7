using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW7Project.Models;

namespace HW7Project.Controllers
{
    public class HomeManagerController : Controller
    {
        HW7ProjectContext db =new HW7ProjectContext();


        // GET: HomeManager
        [LoginCheck]  //把LoginCheck的Controller內寫的登入規則放在Index的Action裡，放在這裡只有Index適用
        public ActionResult Index()
        {
                     
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            //select * from Employee where account=@account and password=@password
            //accout is not PK but unique

            var user = db.Employees.Where(m => m.Account == vMLogin.Account && m.Password == vMLogin.Password).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼有誤!!";
                return View(vMLogin);
            }

            
            else
            {
                Session["user"] = user;/*Session作為判斷是否為管理員的狀態，可全域使用，生命週期為瀏覽器關掉為止，user為管理員狀態*/
                return RedirectToAction("Index");/*回到相同Controller的"Index"頁面*/
            }

            
        }

        [LoginCheck]  //把LoginCheck的Controller內寫的登入規則放在Logout的Action裡，放在這裡只有Logout適用
        public ActionResult Logout()
        {
            Session["user"] = null; /*null為非會員狀態*/
            return RedirectToAction("Index","Home"); /*回到"Home"Controller的"Index"頁面*/
        }
    }
}