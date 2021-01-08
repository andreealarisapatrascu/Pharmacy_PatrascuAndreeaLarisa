using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy_PatrascuAndreeaLarisa.Models
{
    public class Produs
    {
        public int ProdusID { get; set; }
        public string NumeMedicament { get; set; }
        public int CategorieID { get; set; }
        public int FurnizorID { get; set; }
        public string Doza { get; set; }
        public decimal Pret { get; set; }
        public DateTime DataExpirare { get; set; }

        public Categorie Categorie { get; set; }
        public Furnizor Furnizor { get; set; }
    }
}
