using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        public Specialities Specialities { get; set; }  
    }
}
