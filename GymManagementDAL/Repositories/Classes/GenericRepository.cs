using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {
        private readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);
       
        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
                return _context.Set<TEntity>().AsNoTracking().ToList();
            else
                return _context.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }
        public TEntity? GetById(int Id) => _context.Set<TEntity>().Find(Id);

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);




    }
}
