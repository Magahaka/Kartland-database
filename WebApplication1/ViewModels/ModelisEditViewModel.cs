using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ViewModels
{
    public class ModelisEditViewModel
    {
        [DisplayName("ID")]
        public int kodas { get; set; }
        [DisplayName("Pavadinimas")]
        [MaxLength(20)]
        [Required]
        public string pavadinimas { get; set; }
        [DisplayName("Markė")]
        [Required]
        public int fk_marke { get; set; }

        //Markiu sąrašas pasirinkimui
        public IList<SelectListItem> MarkesList { get; set; }
    }
}