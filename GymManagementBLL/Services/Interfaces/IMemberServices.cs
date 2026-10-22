using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createdMember);

        MemberViewModel? GetMmemberDetails(int MmemberId);

        HealthRecordViewModel? GetHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember);

        bool RemoveMember(int MemberId);
    }
}
