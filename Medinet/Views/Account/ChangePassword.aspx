<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Account.ChangePassword.TitleChangePassword%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="contenido-small" class="span-24 last" style="margin-top: 100px;">
        <div class="span-22 prepend-1 append-1 column last">
            <h2 class="path"><%: ViewRes.Views.Account.ChangePassword.PathChangePassword%></h2>
            <% using (Html.BeginForm()) { %>
                <%: Html.ValidationSummary()%>      
                <div class="span-24 last button-padding-top">
                    <h4><%: Html.Label(ViewRes.Views.Account.ChangePassword.TextLastPassword)%></h4>
                    <%: Html.TextBox("CurrentPassword","", new { @type = "password" , @class = "input-background short" })%>
                </div>	
                <div class="span-24 last button-padding-top">
                    <h4><%: Html.Label(ViewRes.Views.Account.ChangePassword.TextNewPassword)%></h4>
                    <%: Html.TextBox("NewPassword", "", new { @type = "password", @class = "input-background short" })%>
                </div>   
                <div class="span-24 last button-padding-top">
                    <h4><%: Html.Label(ViewRes.Views.Account.ChangePassword.TextRepeatNewPassword)%></h4>
                    <%: Html.TextBox("RepeatNewPassword", "", new { @type = "password", @class = "input-background short" })%>
                </div>
                    <div class="button-padding-top">
                        <input type="submit" class="button" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" />
                    </div>
            <% } %>
        </div>      
        <div class="clear"></div>
        <div class="prepend-1 span-24 last button-padding-top">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home")%>
        </div>
    </div>
    
    
 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
