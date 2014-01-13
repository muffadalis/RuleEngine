using System.IO;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class IfStatementAst : StatementAst
    {
        CSharpAstNode _condition;
        CSharpAstNode _thenExpression;
        CSharpAstNode _elseClause;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            _condition = treeNode.ChildNodes[1].AstNode as CSharpAstNode;
            _thenExpression = treeNode.ChildNodes[3].AstNode as CSharpAstNode;
            _elseClause = treeNode.ChildNodes[4].AstNode as CSharpAstNode;
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            textWriter.Write("if (");
            bool isTag = _condition is TagAst;

            if (isTag)
            {
                textWriter.Write("Convert.ToBoolean(");
                _condition.GenerateCSharp(context, textWriter);
                textWriter.WriteLine(")");
            }
            else
                _condition.GenerateCSharp(context, textWriter);

            textWriter.WriteLine(") {");
            context.Indentation++;
            textWriter.Write(context.IndentationText);
            
            _thenExpression.GenerateCSharp(context, textWriter);
            textWriter.WriteLine();

            context.Indentation--;
            textWriter.Write(context.IndentationText);
            
            textWriter.WriteLine("}");

            _elseClause.GenerateCSharp(context, textWriter);
        }
    }
}