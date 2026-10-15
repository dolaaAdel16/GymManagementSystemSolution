using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ISeesionRepository
    {
        public int Add(Session session);    

        public IEnumerable<Session> GetAllSessions();   

        public int Delete(int Id);  

        public int Update(Session session); 

        public Session? GetById(int Id);    
    }
}
