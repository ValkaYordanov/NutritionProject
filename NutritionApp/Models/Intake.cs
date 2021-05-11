using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace NutritionApp.Models
{
    public class Intake
    {
        public int IntakeId { get; set; }
        public string UserId { get; set; }
        public int MealId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Day { get; set; }
        public AppUser User { get; set; }
        public Meal Meal { get; set; }
        public Product Product { get; set; }
    }
}
