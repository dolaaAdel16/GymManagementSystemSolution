using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    public class Trainer : GymUser
    {
        public Specialities Specialities { get; set; }  

        public ICollection<Session> TrainerSessions { get; set; } = null!; 
    }
}
