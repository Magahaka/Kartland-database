using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class KartingaiEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int kodas { get; set; }
        [DisplayName("Pagaminimo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime pagaminimoData { get; set; }
        [DisplayName("Rida")]
        [Required]
        public decimal rida { get; set; }
        [DisplayName("Vertė")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal verte { get; set; }
        [DisplayName("Vietų skaičius")]
        [Required]
        public int vietuSkaicius { get; set; }
        [DisplayName("Spalva")]
        [Required]
        public string busena { get; set; }
        [DisplayName("Galingumas")]
        [Required]
        public string galingumas { get; set; }
        [DisplayName("Grupė")]
        [Required]
        public int fk_grupe { get; set; }
        [DisplayName("Modelis")]
        [Required]
        public int fk_modelis { get; set; }

        //Sąrašai skirti pasirinkimams 
        public IList<SelectListItem> ModeliaiList { get; set; }
        public IList<SelectListItem> GrupesList { get; set; }
    }
}