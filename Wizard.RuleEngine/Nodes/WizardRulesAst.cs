using System.Collections.Generic;
using System.IO;
using System.Linq;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class WizardRulesAst : CSharpAstNode
    {
        List<CSharpAstNode> _statements;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _statements = new List<CSharpAstNode>();

            foreach (var item in treeNode.ChildNodes)
            {
                var statement = item.AstNode as CSharpAstNode;
                _statements.Add(statement);
            }
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            var listOfTags = Tags()
                                        .GroupBy(t => t.TagName)
                                        .Select(g => g.First())
                                        .ToList();

            foreach (var tag in listOfTags)
            {
                textWriter.WriteLine(@"dynamic {0} = GetPropFormValue(""{0}"", quoteId);", tag.TagName);
            }

            foreach (var statement in _statements)
            {
                statement.GenerateCSharp(context, textWriter);
            }
        }
    }

}
