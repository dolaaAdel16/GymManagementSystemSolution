using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.Repositories.Interfaces;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.Repositories.Classes
{
    internal class TrainerRepository : ITrainerRepository
    {

        private readonly GymDbContext _dbcontext;

        public TrainerRepository (GymDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
    
        public int Add(Trainer trainer)
        {
            _dbcontext.Trainers.Add(trainer);
            return _dbcontext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var trainer = _dbcontext.Trainers.Find(Id);
            _dbcontext.Trainers.Remove(_dbcontext.Trainers.Find(Id));
            if (trainer is null)
                return 0;
            _dbcontext.Trainers.Remove(trainer);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers() => _dbcontext.Trainers.ToList();
       

        public Trainer? GetById(int Id) => _dbcontext.Trainers.Find(Id);
        

        public int Update(Trainer trainer)
        {
            _dbcontext.Trainers.Update(trainer);
            return _dbcontext.SaveChanges();
        }
    }
}
