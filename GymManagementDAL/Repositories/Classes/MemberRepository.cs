using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.Repositories.Interfaces;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _context;

        public MemberRepository(GymDbContext context)
        {
            _context = context;
        }

        public int Add(Member member)
        {
            _context.Members.Add(member);
            return _context.SaveChanges(); 
        }

        public int Delete(int Id)
        {
            var member = _context.Members.Find(Id);
            if (member == null)
                return 0;
            _context.Members.Remove(member);
            return _context.SaveChanges();
            
        }

        public IEnumerable<Member> GetAllMembers() => _context.Members.ToList();    


        public Member? GetById(int Id) => _context.Members.Find(Id);    


        public int Update(Member member)
        {
            _context.Members.Update(member);
            return _context.SaveChanges();
        }
    }
}
