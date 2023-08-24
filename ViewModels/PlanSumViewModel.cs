using PlanSumComponentsAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace PlanSumComponentsAP.ViewModels
{
    public class PlanSumViewModel
    {
        public GridLength IdColWidth { get; set; }
        public GridLength WeightColWidth { get; set; }
        public GridLength OperationColWidth { get; set; }
        public GridLength MaxColWidth { get; set; }
        public List<PlanItem> SumPlans { get; set; }

        private PlanSum _planSum;
        private PlanSumDesignViewModel.PlanSumDetails _planSumDetails;
        public string PlanSumId { get; set; }
        public PlanSumViewModel(PlanSum planSum, PlanSumDesignViewModel.PlanSumDetails planSumDetails)
        {
            SumPlans = new List<PlanItem>();
            _planSum = planSum;
            PlanSumId = planSum.Id;
            _planSumDetails = planSumDetails;
            SetColumnWidths();
            SetPlanItems();
        }

        private void SetPlanItems()
        {
            bool sameStructureSet = _planSum.PlanSetups.Select(ps => ps.StructureSet).Distinct().Count() == 1;
            foreach(var planComponent in _planSum.PlanSumComponents)
            {
                PlanItem pItem = new PlanItem();
                pItem.PlanId = planComponent.PlanSetupId;
                pItem.Weight = planComponent.PlanWeight;
                pItem.Operation = planComponent.PlanSumOperation == VMS.TPS.Common.Model.Types.PlanSumOperation.Addition ? "+" : "-";
                if (_planSumDetails.bMaxDose && sameStructureSet)
                {
                    var maxDoseLoc = _planSum.Dose.DoseMax3DLocation;
                    PlanSetup plan = _planSum.PlanSetups.FirstOrDefault(ps => ps.Id.Equals(planComponent.PlanSetupId));
                    plan.DoseValuePresentation = VMS.TPS.Common.Model.Types.DoseValuePresentation.Absolute;
                    pItem.MaxDoseContribution = $"{plan.Dose.GetDoseToPoint(maxDoseLoc)} (of {_planSum.Dose.DoseMax3D})";
                }
                pItem.IdColWidth = IdColWidth;
                pItem.WeightColWidth = WeightColWidth;
                pItem.OperationColWidth = OperationColWidth;
                pItem.MaxColWidth = MaxColWidth;
                SumPlans.Add(pItem);
            }
        }

        private void SetColumnWidths()
        {
            IdColWidth = _planSumDetails.bPlanId ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
            WeightColWidth = _planSumDetails.bWeight ? new GridLength(0.5, GridUnitType.Star) : new GridLength(0);
            OperationColWidth = _planSumDetails.bOperation ? new GridLength(0.5, GridUnitType.Star) : new GridLength(0);
            //MessageBox.Show(WeightColWidth.Value.ToString() + WeightColWidth.GridUnitType.ToString());
            MaxColWidth = _planSumDetails.bMaxDose?new GridLength(2,GridUnitType.Star) : new GridLength(0);
        }
    }
}
