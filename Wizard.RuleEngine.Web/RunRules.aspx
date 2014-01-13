<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunRules.aspx.cs" Inherits="Wizard.RuleEngine.TestWeb.RunRules" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span>Quote Id</span><asp:TextBox ID="txtQuoteId" runat="server" Text="6a753b2d-39a0-4c55-818a-3120f8089fb0" />
        </div>
        <div>
            <asp:Button ID="btnRunRules" runat="server" Text="Run Rules" OnClick="btnRunRules_Click" />
        </div>
        <div>
            <asp:Label ID="lblOutput" runat="server" Text="Output"></asp:Label>
        </div>
    </form>
</body>
</html>
