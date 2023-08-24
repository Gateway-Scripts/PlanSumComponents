using PlanSumComponentsAP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.VisualScripting.ElementInterface;

namespace PlanSumComponentsAP.ViewModels
{
    public class PlanSumDesignViewModel
    {
        //this class is going to communicate with the view. (my viewmodel)
        public class PlanSumViewModel
        {
            private bool _bPlanId;

            public bool bPlanId
            {
                get { return _bPlanId; }
                set { _bPlanId = value; }
            }
            private bool _bWeight;

            public bool bWeight
            {
                get { return _bWeight; }
                set { _bWeight = value; }
            }
            private bool _bOperation;

            public bool bOperation
            {
                get { return _bOperation; }
                set { _bOperation = value; }
            }
            private bool _bMaxDose;//private backing field

            public bool bMaxDose //public property
            {
                get { return _bMaxDose; }
                set { _bMaxDose = value; }
            }
            public PlanSumViewModel()
            {

            }
            public void SetDefaults()
            {
                bPlanId = true;
            }
        }
        //this class is going to be the data that saves back to the XML file. (To be stored in the Data property of DesignTimeDetails). 
        public class PlanSumDetails
        {
            public bool bPlanId { get; set; }
            public bool bWeight { get; set; }
            public bool bOperation { get; set; }
            public bool bMaxDose { get; set; }
        }
        //this class is going to communicate with Visual Scripting by inheriting IDesignTimeDetails
        public class PlanSumDesignTimeDetails : IDesignTimeDetails
        {
            public PlanSumViewModel PlanSumVM = new PlanSumViewModel();
            public PlanSumViewModel PlanSumVMKeep = new PlanSumViewModel();
            public PlanSumDetails m_data = new PlanSumDetails();
            private UserControl m_view;
            public UserControl View
            {
                // Clicking the"i" in Visual scripting will "get" this view.
                get
                {
                    if (!PlanSumVM.bPlanId)
                    {
                        PlanSumVM.SetDefaults();
                    }
                    if (m_view == null)
                    {
                        m_view = new PlanSumDesignView { DataContext = PlanSumVM };
                    }
                    return m_view;
                }
            }

            public object Data
            {
                //the "get" is storing the data from the action pack into the XML file. 
                get
                {
                    m_data.bPlanId = PlanSumVM.bPlanId;
                    m_data.bWeight = PlanSumVM.bWeight;
                    m_data.bOperation = PlanSumVM.bOperation;
                    m_data.bMaxDose = PlanSumVM.bMaxDose;
                    return m_data;
                }
                // the "set" is getting the values from the xml file if those values exist. 
                set
                {
                    PlanSumDetails planSumDetails = value as PlanSumDetails;
                    if (value != null)
                    {
                        if (planSumDetails.bPlanId)
                        {
                            //if the xml file can be converted to a PlanSumDetails and the plan Id is true, then read the xml to set the defaults.
                            PlanSumVM.bPlanId = planSumDetails.bPlanId;
                            PlanSumVM.bWeight = planSumDetails.bWeight;
                            PlanSumVM.bOperation = planSumDetails.bOperation;
                            PlanSumVM.bMaxDose = planSumDetails.bMaxDose;
                            //assigning keep variable to store value of xml. 
                            PlanSumVMKeep.bPlanId = planSumDetails.bPlanId;
                            PlanSumVMKeep.bWeight = planSumDetails.bWeight;
                            PlanSumVMKeep.bOperation = planSumDetails.bOperation;
                            PlanSumVMKeep.bMaxDose = planSumDetails.bMaxDose;
                        }
                        else
                        {
                            PlanSumVM.SetDefaults();
                            PlanSumVMKeep.SetDefaults();
                        }
                    }
                    else
                    {
                        PlanSumVM.SetDefaults();
                        PlanSumVMKeep.SetDefaults();
                    }
                }
            }

            public Type DataType => typeof(PlanSumDetails);

            public Type[] IncomingType { get; set; }
            public DoseValue.DoseUnit EclipseDoseUnit { get; set; }

            public bool IsContentValid => PlanSumVM.bPlanId;

            public void DiscardChanges()
            {
                PlanSumVM.bPlanId = PlanSumVMKeep.bPlanId;
                PlanSumVM.bOperation = PlanSumVMKeep.bOperation;
                PlanSumVM.bWeight = PlanSumVMKeep.bWeight;
                PlanSumVM.bMaxDose = PlanSumVMKeep.bMaxDose;
            }

            public void SaveChanges()
            {
                PlanSumVMKeep.bPlanId = PlanSumVM.bPlanId;
                PlanSumVMKeep.bOperation = PlanSumVM.bOperation;
                PlanSumVMKeep.bWeight = PlanSumVM.bWeight;
                PlanSumVMKeep.bMaxDose = PlanSumVM.bMaxDose;
            }
        }
    }
}
