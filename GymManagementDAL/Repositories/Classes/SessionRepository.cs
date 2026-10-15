using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    internal class SessionRepository : ISeesionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext context)
        {
            _context = context;
        }
        public int Add(Session session)
        {
            _context.Sessions.Add(session);
            return _context.SaveChanges();
        }

        public int Delete(int Id)
        {
              var session = _context.Sessions.Find(Id);
            if (session == null)
                return 0;   
            _context.Sessions.Remove(session);
            return _context.SaveChanges(); 
        }

        public IEnumerable<Session> GetAllSessions() => _context.Sessions.ToList(); 


        public Session? GetById(int Id) => _context.Sessions.Find(Id);  


        public int Update(Session session)
        {
            _context.Sessions.Update(session);  
            return _context.SaveChanges();  
        }
    }
}
