using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class Intake
    {
        public int IntakeId { get; set; }
        public int UserId { get; }
        public int MealId { get; }
        public int ProductId { get; }
        public decimal Quantity { get; set; }
        public DateTime Day { get; set; }
    }
}
