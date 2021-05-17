using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        public AppUser User { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        //public virtual IEnumerable<Tag> Tags { get; set; }
        //public Meal()
        //{
        //    this.Tags = new HashSet<Tag>();
        //}
    }
}
