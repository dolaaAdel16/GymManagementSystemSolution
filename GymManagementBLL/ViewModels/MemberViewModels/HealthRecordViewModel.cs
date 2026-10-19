using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is required")]
        [Range(0.1 , 300 , ErrorMessage = "Height must be greater than 0 and less than 300 ")]
        public decimal Height { get; set; }


        [Required(ErrorMessage = "Weight is required")]
        [Range(0, 500, ErrorMessage = "Weight must be greater than 0 and less than 500 ")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        [StringLength(3 , ErrorMessage = "Blood Type must be 3 letters or less")]
        public string BloodType { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
