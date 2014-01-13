<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Wizard.RuleEngine.TestWeb.Editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wizard Rule Editor</title>
    <style type="text/css" media="screen">
    body {
        overflow: hidden;
        font-family:Monaco, Menlo, 'Ubuntu Mono', Consolas, source-code-pro, monospace;
        font-size:12px;
    }

    #toolbox {

    }
    #editor { 
        margin: 0;
        position: absolute;
        top: 0;
        bottom: 0;
        left: 350px;
        right: 0;
    }
  </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toolbox">
        <ul>
            <li>@Want_Product</li>
            <li>@Product_value</li>
        </ul>
    </div>
        <pre id="editor" runat="server">if @Want_Product then
	premium = @Product_Value * 2.5
else
	Decision = refer
endif</pre>
        <div>
            <asp:Button ID="btnParse" runat="server" Text="Parse" OnClick="btnParse_Click" />
        </div>
        <div>
            <asp:Label ID="lblOutput" runat="server" Text="Output"></asp:Label>
        </div>
    </form>

    <script src="Scripts/ace/src/ace.js" type="text/javascript" charset="utf-8"></script>
    <script>
        var editor = ace.edit("editor");
        editor.getSession().setMode("ace/mode/wizard");
    </script>
</body>
</html>
