using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Kartingas
    {
        public int kodas { get; set; }
        public DateTime pagaminimoData { get; set; }
        public double rida { get; set; }
        public double verte { get; set; }
        public int vietuSkaicius { get; set; }
        public string busena { get; set; }
        public string galingumas { get; set; }
        public Modelis modelis { get; set; }
    }
}