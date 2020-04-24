using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebApplication1.ViewModels
{
    public class KartingaiListViewModel
    {
        [DisplayName("ID")]
        public int kodas { get; set; }
        [DisplayName("Spalva")]
        public string busena { get; set; }
        [DisplayName("Modelis")]
        public string modelis { get; set; }
        [DisplayName("Markė")]
        public string marke { get; set; }
        [DisplayName("Grupė")]
        public string grupe { get; set; }
    }
}