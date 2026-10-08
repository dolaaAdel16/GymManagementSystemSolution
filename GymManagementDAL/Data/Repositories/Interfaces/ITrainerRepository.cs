using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAllTrainers();

        Trainer? GetById(int Id);

        int Add(Trainer trainer);   

        int Update(Trainer trainer);    

        int Delete(int Id); 
    }
}
