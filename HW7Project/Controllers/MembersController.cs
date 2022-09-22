using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW7Project.Models;
using PagedList;
using System.Configuration;

namespace HW7Project.Controllers
{
   [LoginCheck]  //把LoginCheck的Controller內寫的登入規則放在這個Controller裡，放在這裡所有Action都適用
    public class MembersController : Controller
    {
        private HW7ProjectContext db = new HW7ProjectContext();

        // GET: Members
        public ActionResult Index(int page=1)
        {
            var members = db.Members.ToList();

            int pagesize = 15;



            var pagedList = members.ToPagedList(page, pagesize);
            
            return View(pagedList);
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return PartialView(members); //設為部分檢視，這樣從小視窗檢視的時候不會有頭標尾標
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,MemberName,MemberPhotoFile,MemberBirdthday,CreatedDate,Account,Password")] Members members, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    string extensionName = System.IO.Path.GetExtension(photo.FileName);
                    if (extensionName == ".jpg" || extensionName == ".png")
                    {
                        photo.SaveAs(Server.MapPath("~/MemberPhotos/" + members.Account + extensionName));

                        members.MemberPhotoFile = members.Account + extensionName;
                    }
                }
            }

            //var account = db.Members.Where(m => m.Account == members.Account).FirstOrDefault();
            ////比對新註冊的帳號與資料庫內的帳號是否相同，若不相同則為null值
            //if (account != null) //比對結果非null值 = 帳號註冊過
            //{
            //    ViewBag.Error = "此帳號已註冊過";
            //    return View();
            //}

            if (ModelState.IsValid)
            {
                db.Members.Add(members);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(); //原本預設是return View(members)，但因為有寫自訂驗證在Member裡，網頁上資料卻是寫在VMMember，在網頁輸入資料時會報錯，所以把members拿掉
        }

        [Route(@"M/{name}")]
        public ActionResult EditByName(string name)
        {

            Members members = db.Members.Where(m => m.MemberName == name).FirstOrDefault();
            if (members == null)
            {
                return HttpNotFound();
            }
            return View("Edit", members);
        }



        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,MemberName,MemberBirdthday")] Members members)
        {
            string sql = "update members set MemberName=@MemberName, MemberBirdthday=@MemberBirdthday where MemberID=@MemberID";
            

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HW7ProjectConnection"].ConnectionString); //創建連線物件
            SqlCommand cmd = new SqlCommand(sql, conn);


            cmd.Parameters.AddWithValue("@MemberName",members.MemberName);
            cmd.Parameters.AddWithValue("@MemberBirdthday", members.MemberBirdthday);
            cmd.Parameters.AddWithValue("@MemberID", members.MemberID);

            conn.Open(); //開啟連線
            try
            {
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch(Exception ex) 
            { 
                ViewBag.Error= ex.Message;
            }
            conn.Close();

            return View(members);

            //if (ModelState.IsValid)
            //{
            //    db.Entry(members).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
