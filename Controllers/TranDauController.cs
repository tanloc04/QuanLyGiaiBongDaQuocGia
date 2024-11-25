using doan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class TranDauController : Controller
    {
        QLGIAIBONGDAEntities database = new QLGIAIBONGDAEntities();
        public ActionResult TranDau(string searchValue)
        {
            var tranDau = from a in database.TRANDAU
                          select a;

            if (!String.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.Trim(); // Loại bỏ khoảng trắng
                tranDau = tranDau.Where(s => s.SanTD != null && s.MaVD.ToLower().Contains(searchValue.ToLower()));
            }

            return View(database.TRANDAU.ToList());
        }
        public ActionResult Tran()
        {
            return View(database.TRANDAU.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }
   
        [HttpPost]
        public ActionResult Create(TRANDAU tranDau)
        {
            if (!string.IsNullOrEmpty(tranDau.Videos) &&
        !Uri.IsWellFormedUriString(tranDau.Videos, UriKind.Absolute))
            {
                ModelState.AddModelError("Videos", "URL không hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                database.TRANDAU.Add(tranDau);
                database.SaveChanges();
                return RedirectToAction("TranDau");
            }


            return View("TranDau");
        }

    }
}