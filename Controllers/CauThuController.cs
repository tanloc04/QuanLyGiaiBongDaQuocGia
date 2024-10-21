using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class CauThuController : Controller
    {
        doanEntities2 database = new doanEntities2();
        public ActionResult DanhSach(string searchValue)
        {
            var cauthu = from a in database.CauThu
                           select a;

            if (!String.IsNullOrEmpty(searchValue))
            {
                cauthu = cauthu.Where(s => s.TenCT.Contains(searchValue));
            }
            return View(database.CauThu.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CauThu cauthu, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string uploadDir = "~/UploadedImages/";
                    string uploadPath = Server.MapPath(uploadDir);

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string fileName = Path.GetFileName(upload.FileName);
                    string path = Path.Combine(uploadPath, fileName);
                    upload.SaveAs(path);
                    cauthu.Images = uploadDir + fileName;
                }

                if (ModelState.IsValid)
                {
                    database.CauThu.Add(cauthu);
                    database.SaveChanges();
                    return RedirectToAction("DanhSach");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
            }

            return View(cauthu);
        }
        public ActionResult Cauthu()
        {
            return View(database.CauThu.ToList());
        }

        public ActionResult Edit(string mact)
        {
            if (string.IsNullOrEmpty(mact))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CauThu cauthu = database.CauThu.FirstOrDefault(s => s.MaCT == mact);

            if (cauthu == null)
            {
                return HttpNotFound();
            }

            return View(cauthu);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CauThu cauthu,string mact, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        string uploadDir = "~/UploadedImages/";
                        string uploadPath = Server.MapPath(uploadDir);

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string fileName = Path.GetFileName(upload.FileName);
                        string path = Path.Combine(uploadPath, fileName);
                        upload.SaveAs(path);
                        cauthu.Images = uploadDir + fileName;
                    }

                    database.Entry(cauthu).State = EntityState.Modified;
                    database.SaveChanges();
                    return RedirectToAction("DanhSach");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
            }

            return View(cauthu);
        }
        public ActionResult Delete(string mact)
        {
            var cauthu = database.CauThu.FirstOrDefault(s => s.MaCT == mact);

            if (cauthu == null)
            {
                return HttpNotFound();
            }

            return View(cauthu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string mact)
        {
            var cauthu = database.CauThu.FirstOrDefault(s => s.MaCT == mact);

            if (cauthu != null)
            {
                database.CauThu.Remove(cauthu);
                database.SaveChanges();
            }

            return RedirectToAction("DanhSach");
        }

    }
}