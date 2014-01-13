using System.IO;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class QuoteDecisionStatmentAst : StatementAst
    {
        CSharpAstNode _expression;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _expression = treeNode.ChildNodes[2].AstNode as CSharpAstNode;
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            textWriter.Write("Decision = ");
            _expression.GenerateCSharp(context, textWriter);
            textWriter.Write(";");
        }
    }
}