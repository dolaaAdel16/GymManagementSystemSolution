using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Booking : BaseEntity
    {
        // Booking Date - Use CreatedAt from BaseEntity
        public int MemberId { get; set; }
        public int SessionId { get; set; }  
        public Member Member { get; set; } = null!; 
        public Session Session { get; set; } = null!;    
        public bool Attendance { get; set; }
    }
}
