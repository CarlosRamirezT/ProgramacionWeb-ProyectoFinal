using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_Final___Programacion_Web_3.Models;

namespace Proyecto_Final___Programacion_Web_3.Controllers
{
    public class PartnersController : Controller
    {
        private PartnersEntiti db = new PartnersEntiti();

        // GET: Partners
        public ActionResult Index()
        {
            var partners = db.partners.Include(p => p.gender1).Include(p => p.partner1).Include(p => p.partnership1);
            return View(partners.ToList());
        }

        // GET: Partners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // GET: Partners/Create
        public ActionResult Create()
        {
            ViewBag.gender = new SelectList(db.genders, "idGender", "description");
            ViewBag.afilliate = new SelectList(db.partners, "idPartner", "name");
            ViewBag.partnership = new SelectList(db.partnerships, "idPartnership", "description");
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,lastname,idcard,photo,address,phone,gender,age,birthdate,afilliate,partnership,workplace,officeaddress,officephone,partnershipstatus,addmisiondate,departuredate")] partner partner)
        {
            if (ModelState.IsValid)
            {
                db.partners.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gender = new SelectList(db.genders, "idGender", "description", partner.gender);
            ViewBag.afilliate = new SelectList(db.partners, "idPartner", "name", partner.afilliate);
            ViewBag.partnership = new SelectList(db.partnerships, "idPartnership", "description", partner.partnership);
            return View(partner);
        }

        // GET: Partners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            ViewBag.gender = new SelectList(db.genders, "idGender", "description", partner.gender);
            ViewBag.afilliate = new SelectList(db.partners, "idPartner", "name", partner.afilliate);
            ViewBag.partnership = new SelectList(db.partnerships, "idPartnership", "description", partner.partnership);
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPartner,name,lastname,idcard,photo,address,phone,gender,age,birthdate,afilliate,partnership,workplace,officeaddress,officephone,partnershipstatus,addmisiondate,departuredate")] partner partner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gender = new SelectList(db.genders, "idGender", "description", partner.gender);
            ViewBag.afilliate = new SelectList(db.partners, "idPartner", "name", partner.afilliate);
            ViewBag.partnership = new SelectList(db.partnerships, "idPartnership", "description", partner.partnership);
            return View(partner);
        }

        // GET: Partners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partner partner = db.partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            partner partner = db.partners.Find(id);
            db.partners.Remove(partner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
