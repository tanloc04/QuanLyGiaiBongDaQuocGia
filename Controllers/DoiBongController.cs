using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class DoiBongController : Controller
    {
        QLGIAIBONGDAEntities database = new QLGIAIBONGDAEntities();
        public ActionResult DoiBong()
        {
            return View(database.DOIBONG.ToList());
        }
        public ActionResult DoiBongHome()
        {
            return View(database.DOIBONG.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DOIBONG Mact)
        {
            database.DOIBONG.Add(Mact);
            database.SaveChanges();
            return RedirectToAction("DoiBong");
        }
        // GET Edit
        public ActionResult Edit(string madoi)
        {
            var doiBong = database.DOIBONG.FirstOrDefault(s => s.MaDoi == madoi);

            if (doiBong == null)
            {
                ViewBag.ErrorMessage = "Team not found.";
                return View("Error");
            }

            return View(doiBong);
        }

        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DOIBONG doiBong)
        {
            if (ModelState.IsValid)
            {
                var doiBongInDb = database.DOIBONG.FirstOrDefault(s => s.MaDoi == doiBong.MaDoi);

                if (doiBongInDb == null)
                {
                    ViewBag.ErrorMessage = "Team not found. Unable to update.";
                    return View("Error");
                }

                doiBongInDb.TenDoi = doiBong.TenDoi;
                database.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu validation thất bại, trả lại view với lỗi.
            return View(doiBong);
        }

    }
}