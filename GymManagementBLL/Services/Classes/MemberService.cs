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
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.GetRpository<Member>().Add(member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRpository<Member>().GetAll();
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
            var MemberHealthRecord = _unitOfWork.GetRpository<HealthRecord>().GetById(MemberId);
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
            var Member = _unitOfWork.GetRpository<Member>().GetById(MemberId);
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
            var Member = _unitOfWork.GetRpository<Member>().GetById(MemberId);
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

            var ActiveMembership = _unitOfWork.GetRpository<Membership>().GetAll(x => x.MemberId == MemberId && x.Status == "Active")
                                                        .FirstOrDefault();
            if (ActiveMembership is not null)
            {
                viewModel.MmebershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                viewModel.MmebershipEndDate = ActiveMembership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRpository<Plan>().GetById(ActiveMembership.PlanId);
                viewModel.PlanName = plan?.Name;

            }

            return viewModel;
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
                if (IsPhoneExists(UpdatedMember.Phone) || IsEmailExists(UpdatedMember.Email)) return false;

                var Member = _unitOfWork.GetRpository<Member>().GetById(Id);
                if (Member is null) return false;
                Member.Email = UpdatedMember.Email;
                Member.Phone = UpdatedMember.Phone;
                Member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                Member.Address.Street = UpdatedMember.Street;
                Member.Address.City = UpdatedMember.City;
                 _unitOfWork.GetRpository<Member>().Update(Member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRpository<Member>();
            var MembershipRepo = _unitOfWork.GetRpository<Membership>();
            var Member = MemberRepo.GetById(MemberId);
            if (Member is null) return false;
            var HasActiveMemberSessions = _unitOfWork.GetRpository<Session>().GetAll(x => x.Id == MemberId && x.StartTime > DateTime.Now).Any();
            if (HasActiveMemberSessions) return false;

            var Memberships = MembershipRepo.GetAll(x => x.MemberId == MemberId);
            try
            {
                if(Memberships.Any())
                {
                    foreach(var membership in Memberships)
                    {
                        MembershipRepo.Delete(membership);
                    }
                }
                MemberRepo.Delete(Member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // Helper Methods

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRpository<Member>().GetAll(x => x.Phone == phone).Any();
        }
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRpository<Member>().GetAll(x => x.Email == email).Any();
        }
    
    }
}
