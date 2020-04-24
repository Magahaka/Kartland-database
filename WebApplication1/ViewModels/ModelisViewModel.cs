using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class ModelisViewModel
    {
        [DisplayName("ID")]
        public int kodas { get; set; }
        [DisplayName("Pavadinimas")]
        public string pavadinimas { get; set; }
        [DisplayName("Gamintojas")]
        public string marke { get; set; }
    }
}