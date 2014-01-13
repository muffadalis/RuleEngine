using System.Collections.Generic;
using System.IO;
using System.Linq;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class CSharpAstNode : AstNode
    {
        protected ParseTreeNode TreeNode { get; private set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            TreeNode = treeNode;
        }

        public virtual void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            foreach (var item in TreeNode.ChildNodes)
            {
                if (item.AstNode is CSharpAstNode)
                {
                    ((CSharpAstNode)item.AstNode).GenerateCSharp(context, textWriter);
                }
                else if (item.Token != null)
                {
                    textWriter.Write(item.FindTokenAndGetText());
                }
            }
        }

        protected IEnumerable<TagAst> Tags()
        {
            foreach (var baseNode in TreeNode.ChildNodes.Select(child => child.AstNode).OfType<CSharpAstNode>())
            {
                if (baseNode is TagAst)
                {
                    yield return baseNode as TagAst;
                }

                foreach (var grandChild in baseNode.Tags())
                {
                    yield return grandChild;
                }
            }
        }
    }
}
