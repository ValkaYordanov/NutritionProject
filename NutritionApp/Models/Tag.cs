using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        //public virtual IEnumerable<Product> Products { get; set; }
        //public virtual IEnumerable<Meal> Meals { get; set; }
        //public Tag()
        //{
        //    this.Products = new HashSet<Product>();
        //    this.Meals = new HashSet<Meal>();
        //}
    }
}
