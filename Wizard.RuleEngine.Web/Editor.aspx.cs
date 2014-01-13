using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Irony.Parsing;
using Microsoft.CSharp;
using Wizard.RuleEngine.Nodes;

namespace Wizard.RuleEngine.TestWeb
{
    public partial class Editor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            WizardRulesGrammar grammar = new WizardRulesGrammar();
            Parser parser = new Parser(grammar);
            string CSSource = string.Empty;

            var result = parser.Parse(editor.InnerText);

            if (result.HasErrors())
            {
                CSSource = string.Empty;
                lblOutput.Text = string.Empty;

                foreach (var msg in result.ParserMessages)
                {
                    lblOutput.Text += string.Format("Line {0} Position {1}, {2}\n", msg.Location.Line, msg.Location.Position, msg.Message);
                }
            }
            else
            {
                CSharpAstNode root = result.Root.AstNode as CSharpAstNode;

                // Now generate JavaScript from an abstract syntax tree.
                StringBuilder sb = new StringBuilder();
                using (TextWriter tw = new StringWriter(sb, CultureInfo.InvariantCulture))
                {
                    root.GenerateCSharp(new CSharpContext(), tw);
                    string baseRuleFilePath = Server.MapPath("~/BaseRule.cs");
                    string code = File.ReadAllText(baseRuleFilePath);

                    CSSource = code.Replace("///RULES///", sb.ToString());
                }

                var provider = new CSharpCodeProvider();

                var options = new CompilerParameters();
                options.ReferencedAssemblies.Add("System.dll");
                options.ReferencedAssemblies.Add("System.Core.dll");
                options.ReferencedAssemblies.Add("System.Data.dll");
                options.ReferencedAssemblies.Add("System.configuration.dll");
                options.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
                //options.GenerateInMemory = true;
                options.OutputAssembly = System.Web.HttpRuntime.BinDirectory + @"WizardRules.dll";

                var results = provider.CompileAssemblyFromSource(options, new[] 
                { 
                    CSSource
                });

                lblOutput.Text = string.Empty;

                if (results.Errors.Count > 0)
                {
                    foreach (var error in results.Errors)
                    {
                        lblOutput.Text += string.Format("{0}\n", error);
                    }
                }
            }
        }
    }
}