using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Irony.Ast;
using Irony.Parsing;

namespace Wizard.RuleEngine.Nodes
{
    public class TagAst : CSharpAstNode
    {
        public string TagName { get; set; }

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            TagName = treeNode.ChildNodes[1].FindTokenAndGetText();
        }

        public override void GenerateCSharp(CSharpContext context, TextWriter textWriter)
        {
            textWriter.Write(TagName);
        }
    }
}
