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
                var check_ID = database.Account.Where(s => s.ID == _user.ID).FirstOrDefault();
                var check_Name = database.Account.Where(s => s.userName == _user.userName).FirstOrDefault();
                var check_Pass = database.Account.Where(s => s.userPassword == _user.userPassword).FirstOrDefault();
                if (check_ID == null || check_Name == null || check_Pass == null)
                {
                    if (check_ID == null)
                        ViewBag.ErrorID = "ID khong hop le";
                    if (check_Name == null)
                        ViewBag.ErrorName = "Ten dang nhap khong hop le";
                    if (check_Pass == null)
                        ViewBag.ErrorPass = "ID khong hop le";
                    return View("Login");
                }
                else
                {
                    Session["Name"] = _user.userName;
                    return RedirectToAction("TrangChu", "Home");
                }
            }
            catch
            {
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            // Clear the session and abandon it
            Session.Clear();
            Session.Abandon();

            // Redirect to the login page
            return RedirectToAction("Login", "Login"); // Ensure "Login" points to your login action
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