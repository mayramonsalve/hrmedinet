<%@ Page Title="" Language="C#"MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Shared.Errors.NotFoundErrorTitle %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainPageNotFounded" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div class="box rounded">
            <h1><%: ViewRes.Views.Shared.Errors.NotFoundErrorTitle %></h1>
            <h4><%: ViewRes.Views.Shared.Errors.NotFoundErrorText %></h4>
            <h1></h1>
            <a href="/" data-icon="home" data-role="button" data-theme="f" ><%: ViewRes.Views.Shared.Shared.GoHome %></a>
        </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
