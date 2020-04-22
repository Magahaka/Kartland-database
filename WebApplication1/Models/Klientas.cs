using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Klientas
    {
        [DisplayName("Asmens kodas")]
        [Required]
        public string asmensKodas { get; set; }
        [DisplayName("Vardas")]
        [Required]
        public string vardas { get; set; }
        [DisplayName("Pavardė")]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Gimimo data")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime gimimoData { get; set; }
        [DisplayName("Telefonas")]
        [Required]
        public string telefonas { get; set; }
        [DisplayName("Elektroninis paštas")]
        [EmailAddress]
        [Required]
        public string epastas { get; set; }
        [DisplayName("Slapyvardis")]
        [Required]
        public string slapyvardis { get; set; }
        public int id { get; set; }
    }
}