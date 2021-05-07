using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public int UserId { get; }
        public decimal Qunatity { get; set; }
    }
}
