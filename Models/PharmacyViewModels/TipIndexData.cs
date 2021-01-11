using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyModel.Models.PharmacyViewModels
{
    public class TipIndexData
    {
        public IEnumerable<Tip> Tipuri { get; set; }
        public IEnumerable<Produs> Produse { get; set; }
        public IEnumerable<Furnizor> Furnizori { get; set; }
    }
}
