using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string UserId { get; }
        public decimal Quantity { get; set; }
    }
}
