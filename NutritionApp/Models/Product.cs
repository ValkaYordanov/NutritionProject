using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProductNutritionId { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public int GTIN { get; set; }
    }
}
