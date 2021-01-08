using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_PatrascuAndreeaLarisa.Models
{
    public class Furnizor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FurnizorID { get; set; }
        public string NumeFurnizor { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public ICollection<Produs> Produse { get; set; }
    }
}
