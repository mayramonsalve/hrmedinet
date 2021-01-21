<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
      <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Account.PasswordRecovery.TitlePasswordRecovery%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-small" class="span-24 last" style="margin-top: 100px;">
        <div class="span-22 prepend-1 append-1 column last">
            <h2 class="path"><%: ViewRes.Views.Account.PasswordRecovery.PathPasswordRecovery%></h2>
                <div class="linea-sistema-footer"></div>
            <h4><%: ViewRes.Views.Account.PasswordRecovery.TextPasswordRecovery%></h4>
            <% using (Html.BeginForm()) { %>
            <%: Html.ValidationSummary(ViewRes.Views.Account.PasswordRecovery.UserNameValidation)%>      
                    <div class="span-24 last button-padding-top">
                        <%: Html.TextBox("UserName","", new { @class = "input-background short" })%>
                    </div>
                    <div class="button-padding-top">
                        <input type="submit" class="button" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" />
                    </div>
            <% } %>
        </div>
        <div class="clear"></div>
        <div class="prepend-1 span-23 last button-padding-top">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>
