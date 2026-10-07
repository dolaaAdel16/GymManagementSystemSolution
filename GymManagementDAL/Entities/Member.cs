using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
      public string Photo { get; set; } = null!;    

      public HealthRecord HealthRecord { get; set; } = null!;   

      public ICollection<Membership> Memberships { get; set; } = null!; 
      public ICollection<Booking> Bookings { get; set; } = null!;       

    }
}
