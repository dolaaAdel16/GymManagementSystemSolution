using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanById(int PlanId);

        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);

        bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan);

        bool ToggleStatus(int PlanId);


    }
}
