using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomeSell.Models;

namespace DomeSell.Controllers
{    
    public class OrderAdminController : Controller
    {
        private readonly  SellOTOP5929Entities db = new SellOTOP5929Entities();
        // GET: OrderAdmin
        public ActionResult Index()
        {
            var data = db.Table_Oder.Where(a => a.O_Status == 1 ).ToList();
            return View(data);
        }
        public ActionResult Arrange()
        {
            var data = db.Table_Oder.Where(a => a.O_Status == 3).ToList();
            return View(data);
        }
        public ActionResult Neat()
        {
            var data = db.Table_Oder.Where(a => a.O_Status == 4).ToList();
            return View(data);
        }

        public ActionResult DetailOder(int id)
        {
            var data = db.Table_OderDetail.Where(a => a.OD_Oder == id).ToList();
            return View(data);
        }

        public ActionResult DetailOr(int id)
        {
            var data = db.Table_Oder.Where(a => a.O_OderID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Confirm(int id)
        {
            var data = db.Table_Oder.Where(a => a.O_OderID == id).FirstOrDefault();
            data.O_Status = 3;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult transport(int id)
        {
            var data = db.Table_Oder.Where(a => a.O_OderID == id).FirstOrDefault();
            data.O_Status = 4;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



    }
}