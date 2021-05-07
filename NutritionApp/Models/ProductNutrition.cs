using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class ProductNutrition
    {
        public int ProductNutritionId { get; set; }
        public decimal Amount { get; set; }
        public int Kcal { get; set; }
        public int KJ { get; set; }
        public decimal Fat { get; set; }
        public decimal FromFatSaturated { get; set; }
        public decimal Carbs { get; set; }
        public decimal FromCarbsSuggar { get; set; }
        public decimal Protein { get; set; }
        public decimal Salt { get; set; }

    }
}
