using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Plan : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationInMonths { get; set; }
        public decimal Price { get; set; }  
        public bool IsActive { get; set; }  
    }
}
