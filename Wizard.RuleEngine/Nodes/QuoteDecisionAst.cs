using System.IO;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class QuoteDecisionAst : CSharpAstNode
    {
        string _decision = string.Empty;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _decision = treeNode.FindTokenAndGetText().ToLower();
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            if (_decision == "refer")
                textWriter.Write("QuoteDecision.Refer");
            else if (_decision == "accept")
                textWriter.Write("QuoteDecision.Accept");
            else if (_decision == "decline")
                textWriter.Write("QuoteDecision.Decline");
            else if (_decision == "notrequired")
                textWriter.Write("QuoteDecision.NotRequired");
        }
    }
}
