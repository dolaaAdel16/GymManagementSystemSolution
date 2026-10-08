using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.Repositories.Interfaces;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.Repositories.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbcontext;

        // Ask CLR to inject an object from DBContext class into this constructor
        // DbContext object is Injected , not created manually
        public MemberRepository(GymDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public int Add(Member member)
        {
            _dbcontext.Members.Add(member);
            return _dbcontext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var member = _dbcontext.Members.Find(Id);   
            if (member is null)
                return 0;
            _dbcontext.Members.Remove(member);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Member> GetAllMembers() => _dbcontext.Members.ToList();

        public Member? GetById(int Id) => _dbcontext.Members.Find(Id);
       
       
        public int Update(Member member)
        {
            _dbcontext.Members.Update(member);  
            return _dbcontext.SaveChanges();
        }
    }
}
