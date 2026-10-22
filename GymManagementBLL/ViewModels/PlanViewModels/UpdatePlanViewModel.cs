using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class UpdatePlanViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Plan must be between 5 and 200 chars")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration is required")]
        [Range(1,365,ErrorMessage = "Duration days must be between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.1 , 10000 ,ErrorMessage = "Price must be between 0.1 and 10000")]
        public decimal Price { get; set; }
    }
}
