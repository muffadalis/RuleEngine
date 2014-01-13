<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WizardRuleEngineTestWeb._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <%--<section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
        </div>
    </section>--%>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <pre id="code-editor">
    </pre>
</asp:Content>
<asp:Content runat="server" ID="ScriptsContent" ContentPlaceHolderID="FooterContent">
    <script src="Scripts/ace/src/ace.js" type="text/javascript" charset="utf-8"></script>
    <script>
        var editor = ace.edit("code-editor");
        //editor.setTheme("ace/theme/twilight");
        //editor.getSession().setMode("ace/mode/javascript");
    </script>
</asp:Content>