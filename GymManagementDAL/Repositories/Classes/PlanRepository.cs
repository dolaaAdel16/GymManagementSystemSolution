using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _context;
        public PlanRepository(GymDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Plan> GetAll() => _context.Plans.ToList();

        public Plan? GetById(int Id) => _context.Plans.Find(Id);
        

        public int Update(Plan plan)
        {
            _context.Plans.Update(plan);
            return _context.SaveChanges();
        }
    }
}
