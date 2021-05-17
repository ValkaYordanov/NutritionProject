using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace NutritionApp.Models
{
    public class Intake
    {
        public int IntakeId { get; set; }
        public string UserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }
        public DateTime Day { get; set; }

        public AppUser User { get; set; }
        public Meal Meal { get; set; }
        public Product Product { get; set; }
    }
}
