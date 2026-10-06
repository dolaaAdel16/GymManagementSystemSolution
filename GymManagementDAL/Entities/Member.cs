using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
      public string Photo { get; set; } = null!;    
    }
}
