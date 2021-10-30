using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDBEntities _db = new EmployeeDBEntities();
        // GET: Employee
        public ActionResult Index()
        {
            var list = _db.Employees.ToList();
            return View(list);
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create([Bind(Include = "EmpId,EmpName,EmpSal,EmpAddress")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return Json(new { success = true, message = "ok", status = 200 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "test error", status = 500 });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();

            }
            return View(employee);
        }
        [HttpPost]

        public ActionResult Edit([Bind(Include = "EmpId,EmpName,EmpSal,EmpAddress")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return Json(new { success = true, message = "ok", status = 200 }, JsonRequestBehavior.AllowGet);

            }
            return View(employee);

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
    
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _db.Employees.Find(id);
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}