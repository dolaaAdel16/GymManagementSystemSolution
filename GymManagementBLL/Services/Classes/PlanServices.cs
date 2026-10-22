using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanServices : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanServices(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRpository<Plan>().GetAll();
            if (Plans is null || !Plans.Any()) return [];
            return Plans.Select(P => new PlanViewModel()
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                Duration = P.DurationDays,
                Price = P.Price,
                IsActive = P.IsActive
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var Plan = _unitOfWork.GetRpository<Plan>().GetById(PlanId);
            if (Plan is null) return null; 
            return new PlanViewModel()
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                Duration = Plan.DurationDays,
                Price = Plan.Price,
                IsActive = Plan.IsActive

            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = _unitOfWork.GetRpository<Plan>().GetById(PlanId);
            if (Plan is null || Plan.IsActive == false || HasActiveMembership(PlanId)) return null;

            return new UpdatePlanViewModel()
            {
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
            };

        }

        public bool ToggleStatus(int Id)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan)
        {
            var Plan = _unitOfWork.GetRpository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMembership(PlanId)) return false ;

            try 
            {
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.CreatedAt)
                = (updatedPlan.Description, updatedPlan.DurationDays, updatedPlan.Price, DateTime.Now);
                _unitOfWork.GetRpository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // Helper Method 

        private bool HasActiveMembership(int PlanId)
        {
            var ActiveMemberships = _unitOfWork.GetRpository<Membership>().
                GetAll(X => X.PlanId == PlanId && X.Status == "Active");
            return ActiveMemberships.Any();
        }
    }
}
