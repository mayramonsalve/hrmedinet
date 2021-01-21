<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Home.AboutUs.Title%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page block">
        <div class="span-14 column append-1">
            <h2 class="path"><%: ViewRes.Views.Home.AboutUs.Path %></h2>
            <div class="linea-sistema-footer"></div>
            <div class="span-22 append-2 column last">
                <h4><%: ViewRes.Views.Home.AboutUs.WhoAreWeQuestion %></h4>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%: ViewRes.Views.Home.AboutUs.WhoAreWeAnswer %></p>
                <br/>
                <h4><%: ViewRes.Views.Home.AboutUs.VisionQuestion %></h4>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%: ViewRes.Views.Home.AboutUs.VisionAnswer %></p>
                 <br/>
                <h4><%: ViewRes.Views.Home.AboutUs.MissionQuestion %></h4>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%: ViewRes.Views.Home.AboutUs.MissionAnswer %></p>
                 <br/>
                <h4><%: ViewRes.Views.Home.AboutUs.ValuesQuestion %></h4>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%: ViewRes.Views.Home.AboutUs.ValuesAnswer %></p>
            </div>
        </div>
        <div class="span-8 column last">
            <br />
              <!--Division de la derecha--> 
            <div id="login-box" class="column">
                <% Html.RenderPartial("PartialLogOn"); %>
            </div>
            <%--<div id="twitter" class="column">
                <% Html.RenderPartial("TwitterWidget");%>  
            </div>--%>
        </div>    
    </div>
    <% Html.RenderPartial("Footer"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.infieldlabel.min.js") %>" type="text/javascript"></script> 
    <script src="<%: Url.Content("~/Scripts/labelInfield.js") %>" type="text/javascript"></script>
</asp:Content>
