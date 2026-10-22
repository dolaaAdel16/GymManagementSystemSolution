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
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;

        public MemberService(IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> membershipRepository,
            IPlanRepository planRepository, IGenericRepository<HealthRecord> healthRecordRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
            _healthRecordRepository = healthRecordRepository;
        }

        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {
                //if one of them esists return false
                if (IsPhoneExists(createdMember.Phone) || IsEmailExists(createdMember.Email)) return false;

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

        public HealthRecordViewModel? GetHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _healthRecordRepository.GetById(MemberId);
            if (MemberHealthRecord is null) return null;

            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Notes = MemberHealthRecord.Notes
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var Member = _memberRepository.GetById(MemberId);
            if (Member is null) return null;

            return new MemberToUpdateViewModel()
            {
                Email = Member.Email,
                Name = Member.Name,
                Photo = Member.Photo,
                Phone = Member.Phone,
                BuildingNumber = Member.Address.BuildingNumber,
                Street = Member.Address.Street,
                City = Member.Address.City
            };
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

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                if (IsPhoneExists(UpdatedMember.Phone) || IsEmailExists(UpdatedMember.Email)) return false;

                var Member = _memberRepository.GetById(Id);
                if (Member is null) return false;
                Member.Email = UpdatedMember.Email;
                Member.Phone = UpdatedMember.Phone;
                Member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                Member.Address.Street = UpdatedMember.Street;
                Member.Address.City = UpdatedMember.City;
                return _memberRepository.Update(Member) > 0;
            }
            catch
            {
                return false;
            }
        }

        private bool IsPhoneExists(string phone)
        {
            return _memberRepository.GetAll(x => x.Phone == phone).Any();
        }
        private bool IsEmailExists(string email)
        {
            return _memberRepository.GetAll(x => x.Email == email).Any();
        }
        #endregion
    }
}
