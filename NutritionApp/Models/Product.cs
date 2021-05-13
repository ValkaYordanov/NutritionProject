using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        //public int ProductNutritionId { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string GTIN { get; set; }
        public int Kcal { get; set; }
        public int KJ { get; set; }
        public decimal Fat { get; set; }
        public decimal FromFatSaturated { get; set; }
        public decimal Carbs { get; set; }
        public decimal FromCarbsSuggar { get; set; }
        public decimal Protein { get; set; }
        public decimal Salt { get; set; }
        //public ProductNutrition ProductNutrition { get; set; }
        //public virtual IEnumerable<Tag> Tags { get; set; }
        //public Product()
        //{
        //    this.Tags = new HashSet<Tag>();
        //}
    }
}
