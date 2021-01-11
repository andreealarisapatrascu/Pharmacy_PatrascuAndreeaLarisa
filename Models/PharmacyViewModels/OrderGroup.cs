using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_PatrascuAndreeaLarisa.Models.PharmacyViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        public int NumarProduse { get; set; }

    }
}