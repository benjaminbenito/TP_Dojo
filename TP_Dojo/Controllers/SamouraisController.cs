using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using TP_Dojo.Data;
using TP_Dojo.Models;

namespace TP_Dojo.Controllers
{
    public class SamouraisController : Controller
    {
        private TP_DojoContext db = new TP_DojoContext();

        // GET: Samourais
        public async Task<ActionResult> Index()
        {
            return View(await db.Samourais.ToListAsync());
        }

        // GET: Samourais/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = await db.Samourais.FindAsync(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            var SamouraiVM = new SamouraiVM();
            SamouraiVM.Armes = db.Armes.ToList();
            return View(SamouraiVM);
        }

        // POST: Samourais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM samouraiVM)
        {
            if (ModelState.IsValid)
            {

                if (samouraiVM.IdArme.HasValue)
                {
                    samouraiVM.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiVM.IdArme.Value);
                }

                db.Samourais.Add(samouraiVM.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            samouraiVM.Armes = db.Armes.ToList();
            return View(samouraiVM);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            var vm = new SamouraiVM();
            vm.Armes = db.Armes.ToList();
            vm.Samourai = samourai;

            if (samourai.Arme != null)
            {
                vm.IdArme = samourai.Arme.Id;
            }
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            {
            
                var samourai = db.Samourais.Find(vm.Samourai.Id);
                samourai.Force = vm.Samourai.Force;
                samourai.Nom = vm.Samourai.Nom;
                samourai.Arme = null;
                if (vm.IdArme.HasValue)
                {
                    samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == vm.IdArme.Value);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Armes = db.Armes.ToList();
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = await db.Samourais.FindAsync(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Samourai samourai = await db.Samourais.FindAsync(id);
            db.Samourais.Remove(samourai);
            await db.SaveChangesAsync();
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
