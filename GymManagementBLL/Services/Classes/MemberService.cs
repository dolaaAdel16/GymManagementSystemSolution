using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberServices
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IPlanRepository _planRepository;

        public MemberService(IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> membershipRepository,
            IPlanRepository planRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
        }

        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {
                // check if phone exists
                var phoneExists = _memberRepository.GetAll(x => x.Phone == createdMember.Phone).Any();

                // check if email exists
                var emailExists = _memberRepository.GetAll(x => x.Email == createdMember.Email).Any();

                //if one of them esists return false
                if (emailExists || phoneExists) return false;

                //if not add member and return true if added
                var member = new Member()
                {
                    Name = createdMember.Name,
                    Email = createdMember.Email,
                    Phone = createdMember.Phone,
                    Gender = createdMember.Gender,
                    DateOfBirth = createdMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createdMember.BuildingNumber,
                        Street = createdMember.Street,
                        City = createdMember.City
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createdMember.HealthRecordViewModel.Height,
                        Weight = createdMember.HealthRecordViewModel.Weight,
                        BloodType = createdMember.HealthRecordViewModel.BloodType,
                        Notes = createdMember.HealthRecordViewModel.Notes
                    }
                };
                return _memberRepository.Add(member) > 0;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _memberRepository.GetAll();
            if (Members is null || !Members.Any()) return [];

            #region Manual Mapping 01
            //var MemberViewModels = new List<MemberViewModel>();

            //foreach(var Member in Members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Photo = Member.Photo,
            //        Email = Member.Email,
            //        Phone = Member.Phone,
            //        Gender = Member.Gender.ToString()

            //    };
            //    MemberViewModels.Add(memberViewModel);
            #endregion

            var MemberViewModels = Members.Select(X => new MemberViewModel
            {
                Id = X.Id,
                Name = X.Name,
                Photo = X.Photo,
                Email = X.Email,
                Phone = X.Phone,
                Gender = X.Gender.ToString()
            });
            
            return MemberViewModels;
        }

        public MemberViewModel? GetMmemberDetails(int MemberId)
        {
            var Member = _memberRepository.GetById(MemberId);
            if (Member is null) return null;

            var viewModel = new MemberViewModel()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Gender = Member.Gender.ToString(),
                DateOfbirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.Street} - {Member.Address.City}",
                Photo = Member.Photo,
            };
            // Active Mmembership

            var ActiveMembership = _membershipRepository.GetAll(x => x.MemberId == MemberId && x.Status == "Active")
                                                        .FirstOrDefault();
            if (ActiveMembership is not null)
            {
                viewModel.MmebershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                viewModel.MmebershipEndDate = ActiveMembership.EndDate.ToShortDateString();

                var plan = _planRepository.GetById(ActiveMembership.PlanId);
                viewModel.PlanName = plan?.Name;

            }

            return viewModel;
        }
    }
}
