using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlanSumComponentsAP.Models
{
    public class PlanItem
    {
        public string PlanId { get; set; }
        public double Weight { get; set; }
        public string Operation { get; set; }
        public string MaxDoseContribution { get; set; }
        public GridLength IdColWidth { get; set; }
        public GridLength WeightColWidth { get; set; }
        public GridLength OperationColWidth { get; set; }
        public GridLength MaxColWidth { get; set; }
    }
}
