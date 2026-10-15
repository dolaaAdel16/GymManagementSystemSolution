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

        public int Add(Plan plan)
        {
            _context.Plans.Add(plan);
            return _context.SaveChanges();
        }

        public int Delete(int Id)
        {
            var plan = _context.Plans.Find(Id);
            if (plan == null)
                return 0;
            _context.Plans.Remove(plan);
            return _context.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlans() => _context.Plans.ToList();


        public Plan? GetById(int Id) => _context.Plans.Find(Id);
        

        public int Update(Plan plan)
        {
            _context.Plans.Update(plan);    
            return _context.SaveChanges();
        }
    }
}
