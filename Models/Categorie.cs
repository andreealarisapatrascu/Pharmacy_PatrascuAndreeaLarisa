using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy_PatrascuAndreeaLarisa.Models
{
    public class Categorie
    {
        public int CategorieID { get; set; }

        public string NumeCategorie { get; set; }

        public ICollection<Produs> Produse { get; set; }
    }
}
