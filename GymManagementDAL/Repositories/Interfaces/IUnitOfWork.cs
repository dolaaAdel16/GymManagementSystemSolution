using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRpository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChanges(); 
    }
}
