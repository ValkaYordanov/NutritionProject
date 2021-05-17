using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class OverviewViewModel
    {
        //public int IntakeId { get; set; }

        //public int ItemId { get; set; }
        //public string ItemName { get; set; }
        //public decimal Quantity { get; set; }
        //public string Type { get; set; }
        //public string UserId {get;set;}
        //public DateTime Day { get; set; }
        //public IEnumerable<Ingredient> Ingredients { get; set; }
        //public IEnumerable<Intake> Intakes { get; set; }

        public int Kcal { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fat { get; set; }

        public OverviewViewModel()
        {

        }
    }
}
