using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomeSell.Models;

namespace DomeSell.Controllers
{
    public class TypeProductController : Controller
    {
        // GET: Product
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();
        public ActionResult Index(string i = null, string message = null)
        {
            TempData["message"] = message;
            TempData["action"] = i;
            if (i != null)
            {
                TempData["action"] = i;
                TempData["message"] = message;
            }
            var data = db.Table_TypeProduct.ToList();
            return View(data);
        }


        // GET: Product/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Table_TypeProduct data)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Table_TypeProduct.Add(data);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { i = "alert-success", message = "บันทึกข้อมูลเรียบร้อย" });

                }
                return View(data);
            }
            catch
            {
                return View(data);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Table_TypeProduct.Where(a => a.T_TypeProID == id).FirstOrDefault();
            return View(data);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Table_TypeProduct data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { i = "alert-success", message = "แก้ไขข้อมูลเรียบร้อย" });
                }

                return View(data);
            }
            catch
            {
                return View(data);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var CHK = db.Table_Product.Where(a => a.P_Type == id).ToList();
            if (CHK.Count == 0)
            {
                var data = db.Table_TypeProduct.Where(a => a.T_TypeProID == id).FirstOrDefault();
                db.Table_TypeProduct.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Index", new { i = "alert-success", message = "ลบข้อมูลดังกล่าวเรียบร้อย" });
            }
            return RedirectToAction("Index", new { i = "alert-danger", message = "ไม่สามาารถลบข้อมูลได้" });

        }

    }
}