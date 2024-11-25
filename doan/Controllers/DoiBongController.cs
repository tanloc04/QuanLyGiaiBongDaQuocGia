using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using doan.Models;

namespace doan.Controllers
{
    public class DoiBongController : Controller
    {
        QLGIAIBONGDAEntities1 database = new QLGIAIBONGDAEntities1();
        public ActionResult DoiBong()
        {
            return View(database.DOIBONG.ToList());
        }
        public ActionResult DoiBongHome()
        {
            return View(database.DOIBONG.ToList());
        }
        public ActionResult Home()
        {
            return View(database.DOIBONG.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DOIBONG doibong, HttpPostedFileBase upload)
        {
            try
            {
                // Kiểm tra file upload
                if (upload != null && upload.ContentLength > 0)
                {
                    // Kiểm tra loại file (chỉ cho phép ảnh JPEG và PNG)
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(upload.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("", "Chỉ hỗ trợ file ảnh (JPEG, PNG).");
                        return View(doibong);
                    }

                    // Đường dẫn lưu file
                    string uploadDir = "~/UploadedImages/";
                    string uploadPath = Server.MapPath(uploadDir);

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Tạo tên file không trùng
                    string fileName = Guid.NewGuid() + fileExtension;
                    string path = Path.Combine(uploadPath, fileName);

                    // Lưu file vào server
                    upload.SaveAs(path);

                    // Lưu đường dẫn file vào model
                    doibong.Anh = uploadDir + fileName;
                }

                // Kiểm tra dữ liệu có hợp lệ không
                if (ModelState.IsValid)
                {
                    // Lưu vào database
                    database.DOIBONG.Add(doibong);
                    database.SaveChanges();

                    // Chuyển hướng sau khi lưu thành công
                    return RedirectToAction("DoiBong");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Ghi lỗi xác thực dữ liệu từ database
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
                // Ghi lỗi chung
                ModelState.AddModelError("", "Đã xảy ra lỗi: " + ex.Message);
            }

            // Nếu có lỗi, trả về view và hiển thị lỗi
            return View(doibong);
        }

        // GET Edit
        public ActionResult Edit(string madoi)
        {

            if (string.IsNullOrEmpty(madoi))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DOIBONG Mact  = database.DOIBONG.FirstOrDefault(s => s.MaDoi == madoi);

            if (Mact == null)
            {
                return HttpNotFound();
            }

            return View(Mact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DOIBONG doibong, string madoi, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra đội bóng có tồn tại hay không
                    var doiBongInDb = database.DOIBONG.FirstOrDefault(s => s.MaDoi == madoi);
                    if (doiBongInDb == null)
                    {
                        ModelState.AddModelError("", "Team not found.");
                        return View(doibong);
                    }

                    // Cập nhật thông tin cơ bản
                    doiBongInDb.TenDoi = doibong.TenDoi;

                    // Xử lý upload ảnh
                    if (upload != null && upload.ContentLength > 0)
                    {
                        // Kiểm tra định dạng file
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var fileExtension = Path.GetExtension(upload.FileName)?.ToLower();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("Anh", "Only .jpg, .jpeg, .png files are allowed.");
                            return View(doibong);
                        }

                        // Đường dẫn upload
                        string uploadDir = "~/UploadedImages/";
                        string uploadPath = Server.MapPath(uploadDir);

                        // Tạo thư mục nếu chưa tồn tại
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        // Lưu file
                        string fileName = Path.GetFileName(upload.FileName);
                        string path = Path.Combine(uploadPath, fileName);
                        upload.SaveAs(path);

                        // Lưu đường dẫn ảnh
                        doiBongInDb.Anh = uploadDir + fileName;
                    }

                    // Lưu thay đổi
                    database.Entry(doiBongInDb).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("DoiBong", "DoiBong"); // Redirect về trang danh sách
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
                ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            }

            // Nếu có lỗi, trả lại form với dữ liệu hiện tại
            return View(doibong);
        }



    }
}