using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    [Table("AspNetUsers")]
    public class AppUser : IdentityUser
    {
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightGoal { get; set; }
    }
}
