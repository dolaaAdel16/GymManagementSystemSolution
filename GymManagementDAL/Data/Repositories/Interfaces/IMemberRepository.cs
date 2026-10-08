using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // Get All 
        IEnumerable<Member> GetAllMembers();

        // Get By Id
        Member? GetById(int Id);

        // Add
        int Add(Member member);

        //Update
        int Update(Member member);

        //Delete
        int Delete(int Id); 

    }
}
