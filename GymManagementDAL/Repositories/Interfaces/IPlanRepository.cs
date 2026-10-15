using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        public int Add(Plan plan);  

        public IEnumerable<Plan> GetAllPlans();

        public Plan? GetById(int Id);   

        public int Update(Plan plan);
        
        public int Delete(int Id);

    }
}
