using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string UserId { get; set; }
        public decimal Quantity { get; set; }
        public AppUser User { get; set; }
        //public virtual IEnumerable<Tag> Tags { get; set; }
        //public Meal()
        //{
        //    this.Tags = new HashSet<Tag>();
        //}
    }
}
