using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class KartinguAtaskaitaViewModel
    {
        [DisplayName("ID")]
        public int kodas { get; set; }
        [DisplayName("Data")]
        public DateTime pagaminimo_data { get; set; }
        [DisplayName("Modelis")]
        public string modelis { get; set; }
        [DisplayName("Markė")]
        public string marke { get; set; }
        [DisplayName("Grupė")]
        public string grupe { get; set; }
    }
}