using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.VisualScripting.ElementInterface;
using PlanSumComponentsAP.ViewModels;
using System.Windows.Documents;
using PlanSumComponentsAP.Views;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

namespace PlanSumComponentsAP
{
    // TODO: Replace the existing class name with your own class name.
    public class GS_PlanSumPackElement : VisualScriptElement
    {
        public GS_PlanSumPackElement()
        {
            CategorizationTag = "GS Flow Reporting";
            Description = new ElementDescription()
            {
                ElementFunction = "Reports Plan Sum Components",
                Inputs = "Plan Sum",
                Output = "IEnumerable<BlockUIContainer> with Plan Sum table"
            };
        }
        public GS_PlanSumPackElement(IVisualScriptElementRuntimeHost host) { }

        public override bool RequiresRuntimeConsole { get { return false; } }
        public override bool RequiresDatabaseModifications { get { return false; } }
        public override bool RequiresDetailedView { get { return true; } }

        [ActionPackExecuteMethod]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public IEnumerable<BlockUIContainer> Execute(PlanSum pSum)
        {
            // TODO: Add your code here.
            List<BlockUIContainer> blocks = new List<BlockUIContainer>();
            //pull data from xml file. 
            PlanSumDesignViewModel.PlanSumDetails planSumDetails =
                (PlanSumDesignViewModel.PlanSumDetails)(m_designTimeDetails as PlanSumDesignViewModel.PlanSumDesignTimeDetails).Data;
            blocks.Add(new BlockUIContainer(new PlanSumView { DataContext = new PlanSumViewModel(pSum, planSumDetails) }));
            return blocks;
        }

        public override string DisplayName
        {
            get
            {
                // TODO: Replace "Element Name" with the name that you want to be displayed in the Visual Scripting UI.
                return "PlanSum Info";
            }
        }

        //IDictionary<string, string> m_options = new Dictionary<string, string>();
        //public override void SetOption(string key, string value)
        //{
        //    m_options.Add(key, value);
        //}

        //public override IEnumerable<KeyValuePair<string, IEnumerable<string>>> AllowedOptions
        //{
        //    get
        //    {
        //        return new KeyValuePair<string, IEnumerable<string>>[] {
        //    new KeyValuePair<string, IEnumerable<string>>("TestOption", new string[] { "Test Value" })
        //  };
        //    }
        //}
        private IDesignTimeDetails m_designTimeDetails { get; set; }
        public override IDesignTimeDetails DesignTimeDetails
        {
            get
            {
                if(m_designTimeDetails == null)
                {
                    m_designTimeDetails = new PlanSumDesignViewModel.PlanSumDesignTimeDetails();
                }
                return m_designTimeDetails;
            }
        }
    }
}
