using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repos;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AtaskaitaController : Controller
    {
        AtaskaituRepository ataskaituRepository = new AtaskaituRepository();
        // GET: Ataskaita
        // Gali būti nenurodytos datos dėl to prie kintamuju ?
        public ActionResult Index(DateTime? nuo, DateTime? iki)
        {
            AtaskaitaViewModel ataskaita = ataskaituRepository.getKartinguSkaiciu(nuo, iki);
            ataskaita.nuo = nuo == null ? null : nuo;
            ataskaita.iki = iki == null ? null : iki;
            ataskaita.kartingai = ataskaituRepository.getKartingai(nuo, iki);

            return View(ataskaita);
        }
    }
}