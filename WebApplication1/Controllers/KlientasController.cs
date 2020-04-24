using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class KlientasController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos šiame valdiklyje
        KlientasRepository klientasRepository = new KlientasRepository();
        // GET: Klientas
        public ActionResult Index()
        {
            //grazinamas klientų sarašas
            return View(klientasRepository.getKlientai());
        }

        // GET: Klientas/Create
        public ActionResult Create()
        {
            Klientas klientas = new Klientas();
            return View(klientas);
        }

        // POST: Klientas/Create
        [HttpPost]
        public ActionResult Create(Klientas collection)
        {
            try
            {
                // Pridedamas naujas klientas
                klientasRepository.addKlientas(collection);

                // Nukreipia į sąrašą
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Edit/5
        public ActionResult Edit(int id)
        {
            return View(klientasRepository.getKlientas(id));
        }

        // POST: Klientas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Klientas collection)
        {
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    klientasRepository.updateKlientas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(klientasRepository.getKlientas(id));
        }

        // POST: Klientas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (klientasRepository.getKlientasSutarciuCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Negalima pašalinti klientas turėjo sudarytų sutarčių";
                    return View(klientasRepository.getKlientas(id));
                }

                if (!naudojama)
                {
                    klientasRepository.deleteKlientas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}