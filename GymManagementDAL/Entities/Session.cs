using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Session : BaseEntity
    {
        public string Description { get; set; } = null!;    
        public int Capacity { get; set; }   
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CategoryId { get; set; } 
        public Category Category { get; set; } = null!; 
        public int TrainerId { get; set; }  
        public Trainer Trainer { get; set; } = null!;  
        
        public ICollection<Booking> SessionBookings { get; set; } = null!;  
    }
}
