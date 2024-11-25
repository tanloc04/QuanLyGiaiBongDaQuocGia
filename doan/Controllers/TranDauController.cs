using doan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan.Models;
using System.Net;

namespace doan.Controllers
{
    public class TranDauController : Controller
    {
        QLGIAIBONGDAEntities1 database = new QLGIAIBONGDAEntities1();
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
                try
                {
                 
                    database.TRANDAU.Add(tranDau);
                    database.SaveChanges();
                    return RedirectToAction("TranDau");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi khi lưu dữ liệu: " + ex.Message);
                    TempData["ErrorMessage"] = "Lỗi khi lưu dữ liệu: " + ex.Message;
                    return View(tranDau);
                }
            }
            else
            {
                return View(tranDau);
            }



        }
        public ActionResult Edit(string matd)
        {
            if (string.IsNullOrEmpty(matd))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Mã trận đấu không hợp lệ.");
            }

            var tranDau = database.TRANDAU.FirstOrDefault(s => s.MaTD == matd);
            if (tranDau == null)
            {
                return HttpNotFound("Không tìm thấy trận đấu với mã: " + matd);
            }

            return View(tranDau);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TRANDAU tranDau)
        {
            // Kiểm tra URL của video có hợp lệ không
            if (!string.IsNullOrWhiteSpace(tranDau.Videos) &&
                !Uri.IsWellFormedUriString(tranDau.Videos, UriKind.Absolute))
            {
                ModelState.AddModelError("Videos", "URL không hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Chỉ cập nhật những trường thay đổi
                    var tranDauInDb = database.TRANDAU.FirstOrDefault(s => s.MaTD == tranDau.MaTD);
                    if (tranDauInDb == null)
                    {
                        return HttpNotFound("Không tìm thấy trận đấu.");
                    }

                    // Cập nhật các trường từ model
                    tranDauInDb.SanTD = tranDau.SanTD;
                    tranDauInDb.NgayTD = tranDau.NgayTD;
                    tranDauInDb.TySo = tranDau.TySo;
                    tranDauInDb.DoiKhach = tranDau.DoiKhach;
                    tranDauInDb.DoiChuNha = tranDau.DoiChuNha;
                    tranDauInDb.Videos = tranDau.Videos;
                    tranDauInDb.MaVD = tranDau.MaVD;

                    // Lưu thay đổi
                    database.SaveChanges();

                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction("TranDau");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi khi cập nhật dữ liệu: " + ex.Message);
                    TempData["ErrorMessage"] = "Lỗi khi cập nhật dữ liệu: " + ex.Message;
                }
            }

            return View(tranDau);
        }


    }
}