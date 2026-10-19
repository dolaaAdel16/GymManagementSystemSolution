using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
         Plan? GetById(int Id);
         IEnumerable<Plan> GetAll();
         int Update(Plan plan);    
    }
}
