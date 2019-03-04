using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomeSell.Models;

namespace DomeSell.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();
        public ActionResult Index(string i = null, string  message=null)
        {
            TempData["message"] = message;
            TempData["action"] = i;
            if (i != null)
            {
                TempData["action"] = i;
                TempData["message"] =  message;
            }
            var data = db.Table_Product.ToList();
            return View(data);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
            return View(data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.P_Type = new SelectList(db.Table_TypeProduct, "T_TypeProID", "T_Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Table_Product data)
        {
            ViewBag.P_Type = new SelectList(db.Table_TypeProduct, "T_TypeProID", "T_Name");
            try
            {
                if (data.UPostedFile != null)
                {
                    byte[] Temp = new byte[data.UPostedFile.ContentLength];
                    data.UPostedFile.InputStream.Read(Temp, 0, data.UPostedFile.ContentLength);
                    data.P_IMG = Temp;
                }
                if (ModelState.IsValid)
                {
                    db.Table_Product.Add(data);
                    db.SaveChanges();

                    AddAmout(data.P_ProductID,data.P_Amout);
                    return RedirectToAction("Index",new {i = "alert-success", message ="บันทึกข้อมูลเรียบร้อย"});

                }
                return View(data);
            }
            catch (Exception ex) // catches all exceptions
            {
                return View(data);
            }
        }

        public void AddAmout(int id,int? amount)
        {
            var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
            data.P_Amout += amount;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
        }
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {

            var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
            ViewBag.P_Type = new SelectList(db.Table_TypeProduct, "T_TypeProID", "T_Name",data.P_Type);

            return View(data);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Table_Product data)
        {
            ViewBag.P_Type = new SelectList(db.Table_TypeProduct, "T_TypeProID", "T_Name", data.P_Type);
            try
            {
                if (data.UPostedFile != null)
                {
                    byte[] Temp = new byte[data.UPostedFile.ContentLength];
                    data.UPostedFile.InputStream.Read(Temp, 0, data.UPostedFile.ContentLength);
                    data.P_IMG = Temp;
                }
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
            var CHK = db.Table_OderDetail.Where(a => a.OD_Product == id).ToList();
            if (CHK.Count == 0)
            {
                var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
                db.Table_Product.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Index", new { i = "alert-success", message = "ลบข้อมูลดังกล่าวเรียบร้อย" });
            }
            return RedirectToAction("Index", new { i = "alert-danger", message = "ไม่สามาารถลบข้อมูลได้" });

        }

    }
}
