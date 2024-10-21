using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class DoiBongController : Controller
    {
        doanEntities2 database = new doanEntities2();
        public ActionResult DoiBong()
        {
            return View(database.DoiBong.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DoiBong Mact)
        {
            database.DoiBong.Add(Mact);
            database.SaveChanges();
            return RedirectToAction("DoiBong");
        }
    }
}