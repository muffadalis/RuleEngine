using System.IO;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class ElseClauseAst : StatementAst
    {
        CSharpAstNode _elseStatements;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _elseStatements = treeNode.ChildNodes[1].AstNode as CSharpAstNode;
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            textWriter.WriteLine("else {");
            context.Indentation++;
            textWriter.Write(context.IndentationText);

            _elseStatements.GenerateCSharp(context, textWriter);
            textWriter.WriteLine();

            context.Indentation--;
            textWriter.Write(context.IndentationText);
            textWriter.WriteLine("}");

        }    
    }
}