using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Ingredient
    {
        
        public int IngredientId { get; set; }
        public int MealId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Product Product { get; set; }
        public Meal Meal { get; set; }


    }
}
