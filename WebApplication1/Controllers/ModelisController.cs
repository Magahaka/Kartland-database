using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repos;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ModelisController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        ModeliuRepository modeliuRepository = new ModeliuRepository();
        MarkeRepository markeRepository = new MarkeRepository();
        // GET: Modelis
        public ActionResult Index()
        {
            return View(modeliuRepository.getModeliai());
        }

        // GET: Modelis/Create
        public ActionResult Create()
        {
            ModelisEditViewModel modelis = new ModelisEditViewModel();
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Create
        [HttpPost]
        public ActionResult Create(ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.addModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Modelis/Edit/5
        public ActionResult Edit(int kodas)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(kodas);
            PopulateSelections(modelis);
            return View(modelis);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult Edit(int kodas, ModelisEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    modeliuRepository.updateModelis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Modelis/Delete/5
        public ActionResult Delete(int kodas)
        {
            ModelisEditViewModel modelis = modeliuRepository.getModelis(kodas);
            return View(modelis);
        }

        // POST: Modelis/Delete/5
        [HttpPost]
        public ActionResult Delete(int kodas, FormCollection collection)
        {
            try
            {
                ModelisEditViewModel modelis = modeliuRepository.getModelis(kodas);
                bool naudojama = false;

                if (modeliuRepository.getModelisCount(kodas) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Negalima pašalinti modelio, yra sukurtų automobilių su šiuo modeliu.";
                    return View(modelis);
                }

                if (!naudojama)
                {
                    modeliuRepository.deleteModelis(kodas);
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(ModelisEditViewModel modelis)
        {
            var markes = markeRepository.getMarkes();
            List<SelectListItem> selectListmarkes = new List<SelectListItem>();

            foreach (var item in markes)
            {
                selectListmarkes.Add(new SelectListItem() { Value = Convert.ToString(item.kodas), Text = item.pavadinimas });
            }

            modelis.MarkesList = selectListmarkes;
        }
    }
}