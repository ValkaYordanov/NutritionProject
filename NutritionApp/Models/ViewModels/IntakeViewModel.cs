using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class IntakeViewModel
    {
        public int IntakeId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string Type { get; set; }
        public string UserId {get;set;}
        public DateTime Day { get; set; }

        public IntakeViewModel()
        {

        }
    }
}
