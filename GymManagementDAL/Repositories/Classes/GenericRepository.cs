using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity , new()
    {
        private readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
             return _context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();


        public TEntity? GetById(int Id) => _context.Set<TEntity>().Find(Id);
       

        public int Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return _context.SaveChanges();
        }   
    }
}
