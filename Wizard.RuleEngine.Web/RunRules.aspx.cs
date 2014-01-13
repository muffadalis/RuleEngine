using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wizard.RuleEngine.TestWeb
{
    public partial class RunRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRunRules_Click(object sender, EventArgs e)
        {
            //Guid id = new Guid(txtQuoteId.Text);
            ////var t = results.CompiledAssembly.GetType("WizardRules");
            //var obj = Activator.CreateInstance(t);
            //t.GetMethod("RunRule").Invoke(obj, new object[] { id });

            //lblOutput.Text = "Decision = " + obj.GetType().GetProperty("Decision").GetValue(obj).ToString();
            //lblOutput.Text += "\nPremium = " + obj.GetType().GetProperty("Premium").GetValue(obj).ToString();
        }
    }
}