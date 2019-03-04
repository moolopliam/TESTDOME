using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomeSell.Models;

namespace DomeSell.Controllers
{
    public class AddProductController : Controller
    {
        // GET: Product
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();
        public ActionResult Index(string i = null, string message = null)
        {
            TempData["message"] = message;
            TempData["action"] = i;
            i = null;
            if (i != null)
            {
                TempData["action"] = i;
                TempData["message"] = message;
            }
            var data = db.Table_AddProduct.ToList();
            return View(data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.A_Product = new SelectList(db.Table_Product, "P_ProductID", "P_ProName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Table_AddProduct data)
        {
            ViewBag.A_Product = new SelectList(db.Table_Product, "P_ProductID", "P_ProName");
            try
            {
                if (ModelState.IsValid)
                {
                    data.A_StatusAdd = 1;
                    db.Table_AddProduct.Add(data);
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

        public ActionResult ChangeSta(int id)
        {
            var data = db.Table_AddProduct.Where(a => a.A_AddProID == id).FirstOrDefault();
            data.A_StatusAdd = 2;
            db.Entry(data).State = EntityState.Modified;

            var dataPro = db.Table_Product.Where(a => a.P_ProductID == data.A_Product).FirstOrDefault();
            dataPro.P_Amout = dataPro.P_Amout + data.A_amount;
            db.Entry(dataPro).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { i = "alert-success", message = "ยืนยันสินค้าเข้าคลังเรียบร้อย" });
        }
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            var data = db.Table_AddProduct.Where(a => a.A_Product == id).FirstOrDefault();
            ViewBag.A_Product = new SelectList(db.Table_Product, "P_ProductID", "P_ProName", data.A_Product);

            return View(data);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Table_AddProduct data)
        {
            ViewBag.A_Product = new SelectList(db.Table_Product, "P_ProductID", "P_ProName", data.A_Product);
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

        public ActionResult Delete(int id)
        {
            var data = db.Table_AddProduct.Where(a => a.A_AddProID == id).FirstOrDefault();
            db.Table_AddProduct.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index", new { i = "alert-success", message = "ลบข้อมูลดังกล่าวเรียบร้อย" });
        }
    }
}
