using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class IntakeViewModel
    {
        public int IntakeId { get; set; }
        //[Required]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        //[Required]
        public string Type { get; set; }
        public string UserId {get;set;}
        public DateTime Day { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<Intake> Intakes { get; set; }

        public IntakeViewModel()
        {

        }
    }
}
