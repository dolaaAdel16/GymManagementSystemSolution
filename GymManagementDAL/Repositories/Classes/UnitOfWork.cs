using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbcontext;

        public UnitOfWork(GymDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IGenericRepository<TEntity> GetRpository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var Repo))
                return (IGenericRepository < TEntity >)Repo;

            var NewRepo = new GenericRepository<TEntity>(_dbcontext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
