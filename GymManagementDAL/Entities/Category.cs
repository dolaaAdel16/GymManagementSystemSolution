using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;    

        public ICollection<Session> CategorySessions { get; set; } = null!;   
    }
}
