using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repos;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class KartingasController : Controller
    {
        KartingaiRepository kartingaiRepository = new KartingaiRepository();
        ModeliuRepository modeliuRepository = new ModeliuRepository();
        GrupeRepository grupeRepository = new GrupeRepository();

        // GET: Kartingas
        // Grąžinamas kartingų sąrašo vaizdas
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(kartingaiRepository.getKarts());
        }

        // GET: Kartingas/Create
        public ActionResult Create()
        {
            KartingaiEditViewModel kartingaiEditViewModel = new KartingaiEditViewModel();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            PopulateSelections(kartingaiEditViewModel);
            return View(kartingaiEditViewModel);
        }

        // POST: Kartingas/Create
        [HttpPost]
        public ActionResult Create(KartingaiEditViewModel collection)
        {
            try
            {
                //Pridedamas naujas kartingas
                kartingaiRepository.addKart(collection);

                //Nukreipia i sąrašą
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Kartingas/Edit/5
        public ActionResult Edit(int kodas)
        {
            //Surenkama automobilio informacija iš duomenų bazės
            KartingaiEditViewModel autoEditViewModel = kartingaiRepository.getKart(kodas);
            //Užpildomi pasirinkimų sąrašai
            PopulateSelections(autoEditViewModel);
            return View(autoEditViewModel);
        }

        // POST: Automobilis/Edit/5
        [HttpPost]
        public ActionResult Edit(int kodas, KartingaiEditViewModel collection)
        {
            try
            {
                // Atnaujinama automobilio informacija
                kartingaiRepository.updateKart(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Automobilis/Delete/5
        public ActionResult Delete(int kodas)
        {
            KartingaiEditViewModel autoEditViewModel = kartingaiRepository.getKart(kodas);
            return View(autoEditViewModel);
        }

        // POST: Kartingas/Delete/5
        [HttpPost]
        public ActionResult Delete(int kodas, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (kartingaiRepository.getKartingasSutarciuCount(kodas) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Kartingas naudojamas sutartyse, negalima pašalinti.";
                    return View(kartingaiRepository.getKart(kodas));
                }

                if (!naudojama)
                {
                    kartingaiRepository.deleteKartingas(kodas);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(KartingaiEditViewModel kartingaiEditViewModel)
        {
            var modeliai = modeliuRepository.getModeliai();
            var grupes = grupeRepository.getGrupes();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<SelectListItem> selectListGrupe = new List<SelectListItem>();
            List<SelectListGroup> groups = new List<SelectListGroup>();
            bool yra = false;

            //Sukuriamos pasirinkimo grupės
            foreach (var item in modeliai)
            {
                yra = false;
                foreach (var i in groups)
                {
                    if (i.Name.Equals(item.marke))
                    {
                        yra = true;
                    }
                }
                if (!yra)
                {
                    groups.Add(new SelectListGroup() { Name = item.marke });
                }
            }

            //Užpildomas pasirinkimo sąrašas pagal grupes(markes) autombolių modelių
            foreach (var item in modeliai)
            {
                var optGroup = new SelectListGroup() { Name = "--------" };
                foreach (var i in groups)
                {
                    if (i.Name.Equals(item.marke))
                    {
                        optGroup = i;
                    }
                }
                selectListItems.Add(
                    new SelectListItem() { Value = Convert.ToString(item.kodas), Text = item.pavadinimas, Group = optGroup }
                    );
            }

            //Užpildomas būsenų sąrašas iš duomenų bazės
            foreach (var item in grupes)
            {
                selectListGrupe.Add(new SelectListItem() { Value = Convert.ToString(item.kodas), Text = item.pavadinimas });
            }


            //Sarašai priskiriami vaizdo objektui
            kartingaiEditViewModel.ModeliaiList = selectListItems;
            kartingaiEditViewModel.GrupesList = selectListGrupe;
        }
    }
}