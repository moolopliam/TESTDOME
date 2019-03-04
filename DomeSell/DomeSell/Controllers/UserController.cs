using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomeSell.Models;

namespace DomeSell.Controllers
{
    public class UserController : Controller
    {
        private readonly SellOTOP5929Entities db = new SellOTOP5929Entities();
        // GET: User

        public ActionResult Login()
        {
            Session["Username1"] = "2";
            Session["type"] = db.Table_TypeProduct.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Login(Table_User data)
        {
            ModelState.Remove("U_Amper");
            ModelState.Remove("U_HBD");
            ModelState.Remove("U_Phone");
            ModelState.Remove("U_LastName");
            ModelState.Remove("U_Name");
            ModelState.Remove("U_Province");
            ModelState.Remove("U_Tampo");
            ModelState.Remove("U_Tilie");
            ModelState.Remove("U_Address");
            ModelState.Remove("U_Zipcode");


            var CK = db.Table_User.Where(a => a.U_UserName == data.U_UserName && a.U_PassWord == data.U_PassWord)
                .FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (CK != null)
                {
                    if (CK.U_Role == 2)
                    {
                        Session["Username1"] = 55;
                        Session["Rol"] = 2;
                        Session["Ad"] = data.U_UserName;
                        AddBag(CK.U_UserName, CK.U_Role);
                        return RedirectToAction("Index", "Product");
                    }

                    Session["Username1"] = 55;
                    Session["Rol"] = 1;
                    Session["Username"] = data.U_UserName;
                    AddBag(CK.U_UserName, CK.U_Role);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError("U_UserName", "ชื่อผู้ใช้หรือรหัสผ่านผิด");
                }
            }

            return View(data);
        }

        public ActionResult Loguot()
        {

            Session.Remove("Username");
            return RedirectToAction(nameof(Index));
        }

        public ActionResult HitoryOrder()
        {

            if (Session["Username"] != null)
            {
                var ID = Session["Username"].ToString();
                var data = db.Table_Oder.Where(a => a.O_Status != 2 && a.O_User == ID).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public ActionResult NeedPay()
        {

            if (Session["Username"] != null)
            {
                var ID = Session["Username"].ToString();
                var data = db.Table_Oder.Where(a => a.O_Status == 2 && a.O_User == ID).ToList();
                var vOder = db.Table_Oder.Where(a => a.O_Status == 2 && a.O_User == ID).FirstOrDefault();
                var total = db.Table_OderDetail.Where(a => a.OD_Oder == vOder.O_OderID).ToList();
                TempData["total"] = total.Sum(a => a.Table_Product.P_Price);
                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public ActionResult OrderDetail(int id)
        {
            var data = db.Table_OderDetail.Where(a => a.OD_Oder == id).ToList();
            return View(data);
        }

        public ActionResult DetailUser()
        {

            if (Session["Username"] != null)
            {
                var ID = Session["Username"].ToString();
                var data = db.Table_User.Where(a => a.U_UserName == ID).FirstOrDefault();
                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }
        public void AddBag(string Usernaem, int? TypeUser)
        {
            var HKoRder = db.Table_Oder.Where(a => a.O_User == Usernaem).ToList();
            if (TypeUser == 1)
            {
                if (HKoRder.Count == 0) AddValue(Usernaem);

                var i = HKoRder.Where(a => a.O_Status == 1).ToList();
                var r = HKoRder.Where(a => a.O_Status == 2).ToList();

                if (i.Count != 0)
                {
                    if (r.Count == 0) AddValue(Usernaem);
                }
                else
                {
                    if (HKoRder.Count > 0)
                        if (r.Count == 0)
                            AddValue(Usernaem);
                }
            }
        }

        public void AddValue(string Username)
        {
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            var _Order = new Table_Oder()
            {
                O_Date = date,
                O_User = Username,
                O_Status = 2,
                O_Total = 0
            };
            db.Table_Oder.Add(_Order);
            db.SaveChanges();
        }
        public ActionResult Index(int id = 0)
        {

            if (Session["Username"] == null)
            {
                Session["Username1"] = "2";
            }
            var data = db.Table_Product.ToList();
            Session["type"] = db.Table_TypeProduct.ToList();
            if (id != 0)
            {
                data = data.Where(x => x.P_Type == id).ToList();
            }
            return View(data);
        }

        public ActionResult DetailProduct(int id, int i = 0,string mg ="")
        {
            if (Session["Username"] != null)
            {
                if (i != 0)
                {
                    TempData["action"] = mg;
                }
                var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
                if (i != 0)
                {
                    TempData["Status"] = i;
                }
                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }


        [HttpPost]
        public ActionResult Bag(Table_Product da, int id, int amount)
        {

            var USe = Session["Username"].ToString();
            var data = db.Table_Oder.Where(a => a.O_User == USe && a.O_Status == 2).FirstOrDefault();
            Table_OderDetail Detail = new Table_OderDetail()
            {
                OD_Oder = data.O_OderID,
                OD_Product = id,
                OD_Amount = amount
            };
            if (CutPro(id, amount))
            {
                db.Table_OderDetail.Add(Detail);
                db.SaveChanges();
                return RedirectToAction(nameof(OrderDetail), new { id = data.O_OderID });
            }
            else
            {
                return RedirectToAction(nameof(DetailProduct), new { id = id, i = 1 ,mg ="สินค้าไม่เพียงพอ" });
            }



        }

        public bool CutPro(int id, int amount)
        {
            var data = db.Table_Product.Where(a => a.P_ProductID == id).FirstOrDefault();
            if (data.P_Amout >= amount)
            {
                data.P_Amout = data.P_Amout - amount;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public ActionResult BagDetai(int id)
        {
            Session["type"] = db.Table_TypeProduct.ToList();
            var data = db.Table_OderDetail.Where(a => a.OD_Oder == id).ToList();
            return View(data);
        }

        public ActionResult RemovtItem(int idOrDe, int idOr)
        {
            var data = db.Table_OderDetail.Where(a => a.OD_OrderDetail == idOrDe).FirstOrDefault();
            db.Table_OderDetail.Remove(data);
            db.SaveChanges();
            return RedirectToAction(nameof(OrderDetail), new { id = idOr });
        }

        public ActionResult ConfiramPay(int idC)
        {
            var data = db.Table_Oder.Where(a => a.O_OderID == idC).FirstOrDefault();
            var value = db.Table_OderDetail.Where(a => a.OD_Oder == data.O_OderID).ToList();
            int? to = 0;
            foreach (var iteDetail in value)
            {
                to += (iteDetail.Table_Product.P_Price * iteDetail.OD_Amount);
            }
            TempData["sum"] = to;
            return View(data);
        }
        [HttpPost]
        public ActionResult ConfiramPay(Table_Oder data)
        {

            if (Session["Username"] != null)
            {
                var ID = Session["Username"].ToString();
                var value = db.Table_OderDetail.Where(a => a.OD_Oder == data.O_OderID).ToList();
                int? to = 0;
                foreach (var iteDetail in value)
                {
                    to += (iteDetail.Table_Product.P_Price * iteDetail.OD_Amount);
                }
                TempData["sum"] = to;
                ModelState.Remove("O_IMGPAY");
                if (data.OPostedFile != null)
                {
                    byte[] Temp = new byte[data.OPostedFile.ContentLength];
                    data.OPostedFile.InputStream.Read(Temp, 0, data.OPostedFile.ContentLength);
                    data.O_IMGPAY = Temp;
                    data.O_Status = 1;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    var date = DateTime.Now.ToString("dd/MM/yyyy");
                    var _Order = new Table_Oder()
                    {
                        O_Date = date,
                        O_User = ID,
                        O_Status = 2,
                        O_Total = 0
                    };
                    db.Table_Oder.Add(_Order);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("O_IMGPAY", "กรุณาส่งหลักฐานการชำระเงิน");
                }

                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public ActionResult ProfileUse()
        {

            if (Session["Username"] != null)
            {
                var ID = Session["Username"].ToString();
                var data = db.Table_User.Where(a => a.U_UserName == ID).FirstOrDefault();
                ViewBag.U_Tilie = new SelectList(db.Table_TilieName, "T_TItleID", "T_TilName", data.U_Tilie);
                return View(data);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }
        [HttpPost]
        public ActionResult ProfileUse(Table_User data)
        {
            ModelState.Remove("UPostedFile");
            ViewBag.U_Tilie = new SelectList(db.Table_TilieName, "T_TItleID", "T_TilName", data.U_Tilie);
            if (ModelState.IsValid)
            {
                if (data.UPostedFile != null)
                {
                    byte[] Temp = new byte[data.UPostedFile.ContentLength];
                    data.UPostedFile.InputStream.Read(Temp, 0, data.UPostedFile.ContentLength);
                    data.U_IMGUSER = Temp;
                }
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(DetailUser));
            }

            return View(data);
        }

        public ActionResult register()
        {
            ViewBag.U_Tilie = new SelectList(db.Table_TilieName, "T_TItleID", "T_TilName");

            return View();
        }

        [HttpPost]
        public ActionResult register(Table_User data)
        {
            ViewBag.U_Tilie = new SelectList(db.Table_TilieName, "T_TItleID", "T_TilName");
            data.U_Role = 1;
            if (ModelState.IsValid)
            {
                if (data.UPostedFile != null)
                {
                    byte[] Temp = new byte[data.UPostedFile.ContentLength];
                    data.UPostedFile.InputStream.Read(Temp, 0, data.UPostedFile.ContentLength);
                    data.U_IMGUSER = Temp;
                }
                db.Table_User.Add(data);
                db.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            return View(data);
        }
    }
}