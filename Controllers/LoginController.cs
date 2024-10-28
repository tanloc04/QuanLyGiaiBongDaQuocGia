using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class LoginController : Controller
    {
        doanEntities2 database = new doanEntities2();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult autheinLogin(Account _user)
        {
            try
            {
              
                var user = database.Account.FirstOrDefault(s => s.userName == _user.userName && s.userPassword == _user.userPassword);

               
                if (user == null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không hợp lệ.";
                    return View("Login");
                }

                
                Session["Name"] = user.userName;
                Session["UserRole"] = user.userRole;

             
                if (user.userRole == "CauThu")
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    return RedirectToAction("Admin", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi, vui lòng thử lại.";
                return View("Login");
            }
        }
        public ActionResult Dangky()
        {
            return View();
        }
        public ActionResult autheinDangky(Account _user)
        {
            try
            {
                database.Account.Add(_user);
                database.SaveChanges();
                return RedirectToAction("Login");
            }

            catch
            {
                return View("Dangky");
            }

        }
        public ActionResult QuanLy(string searchValue)
        {
            var accounts = from a in database.Account
                           select a;

            if (!String.IsNullOrEmpty(searchValue))
            {
                accounts = accounts.Where(s => s.userName.Contains(searchValue));
            }
            return View(accounts.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account user)
        {
            database.Account.Add(user);
            database.SaveChanges();
            return RedirectToAction("QuanLy");
        }
        public ActionResult Details(int  id)
        {
            return View(database.Account.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int  id)
        {
            return View(database.Account.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Account user)
        {
            database.Entry(user).State=System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QuanLy");
        }
        public ActionResult Delete(int id)
        {
            var account = database.Account.FirstOrDefault(s => s.ID == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var account = new Account { ID = id };
            database.Account.Attach(account);
            database.Account.Remove(account);
            database.SaveChanges();
            return RedirectToAction("QuanLy");
        }
    }
}