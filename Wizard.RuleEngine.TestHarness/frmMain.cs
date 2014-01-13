using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Irony.Parsing;
using Microsoft.CSharp;
using Wizard.RuleEngine.Nodes;

namespace Wizard.RuleEngine.TestHarness
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            var grammar = new WizardRulesGrammar();
            var parser = new Parser(grammar);

            var result = parser.Parse(txtRules.Text);

            if (result.HasErrors())
            {
                txtCSSource.Text = string.Empty;
                lblOutput.Text = string.Empty;

                foreach (var msg in result.ParserMessages)
                {
                    lblOutput.Text += string.Format("Line {0} Position {1}, {2}\n", msg.Location.Line,
                                                    msg.Location.Position, msg.Message);
                }
            }
            else
            {
                var root = result.Root.AstNode as CSharpAstNode;

                // Now generate JavaScript from an abstract syntax tree.
                var sb = new StringBuilder();
                using (TextWriter tw = new StringWriter(sb, CultureInfo.InvariantCulture))
                {
                    root.GenerateCSharp(new CSharpContext(), tw);

                    var code = File.ReadAllText("BaseRule.cs");

                    txtCSSource.Text = code.Replace("///RULES///", sb.ToString());
                }
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            btnParse_Click(sender, e);

            if (string.IsNullOrEmpty(txtCSSource.Text)) return;

            var id = new Guid(txtQuoteId.Text);

            var provider = new CSharpCodeProvider();

            var options = new CompilerParameters();
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Core.dll");
            options.ReferencedAssemblies.Add("System.Data.dll");
            options.ReferencedAssemblies.Add("System.configuration.dll");
            options.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            options.GenerateInMemory = true;
            options.OutputAssembly = "WizardRules.dll";

            var results = provider.CompileAssemblyFromSource(options, new[]
                                                                                      {
                                                                                          txtCSSource.Text
                                                                                      });

            lblOutput.Text = string.Empty;

            if (results.Errors.Count > 0)
            {
                foreach (var error in results.Errors)
                {
                    lblOutput.Text += string.Format("{0}\n", error);
                }
            }
            else
            {
                var t = results.CompiledAssembly.GetType("WizardRules");
                var obj = Activator.CreateInstance(t);
                t.GetMethod("RunRule").Invoke(obj, new object[] {id});

                lblOutput.Text = "Decision = " + obj.GetType().GetProperty("Decision").GetValue(obj);
                lblOutput.Text += "\nPremium = " + obj.GetType().GetProperty("Premium").GetValue(obj);
            }
        }
    }
}