using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    internal class MemberViewModel
    { 
        public int Id { get; set; }
        public string? Photo { get; set; }  
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;  
        public string Gender { get; set; } = null!; 
        public string? PlanName { get; set; }
        public string? DateOfbirth { get; set; }
        public string? MmebershipStartDate { get; set; }
        public string? MmebershipEndDate { get; set; }
        public string? Address { get; set; } 
    }
}
