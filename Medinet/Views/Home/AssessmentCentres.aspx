<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Home.Services.ENDTitle%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="page block">
    <div class="span-14 column">
        <h2 class="path">
            <%: ViewRes.Views.Home.Services.ENDTitle%>
        </h2>
        <div class="linea-sistema-footer"></div>
        <div class="span-22 append-2 column last">
            <div class="col1">
                <p>
                    <span class="txt-orange alignJustify">
                        <%: ViewRes.Views.Home.Services.COdescription %>
                    </span>
                </p>
                <p><%: ViewRes.Views.Home.Services.ENDmsj1%></p>
                <p><%= ViewRes.Views.Home.Services.ENDmsj2%></p>
            </div>
            <div class="linea-sistema-footer"></div>
            <div class="col1 alignJustify">
                <span class="txt-orange alignJustify">
                    <%: ViewRes.Views.Home.Services.Benefits %>
                </span>
                <ul class="list-separator">
                    <li><%: ViewRes.Views.Home.Services.ENDmsj3%></li>
                    <li><%: ViewRes.Views.Home.Services.ENDmsj4%></li>
                </ul>
            </div>
        </div>
        <div class="linea-sistema-footer"></div>
        <div class="alignRight span-24 column last">
            <%: Html.ActionLink(ViewRes.Views.Home.Services.BackToServices,"Services","Home") %>
        </div>
    </div>
    <div class="span-8 column last">
        <br />
            <!--Division de la derecha--> 
        <div id="login-box" class="column">
            <% Html.RenderPartial("PartialLogOn"); %>
        </div><!--Division de la derecha--> 
    </div> 
    <% Html.RenderPartial("Footer"); %>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.infieldlabel.min.js") %>" type="text/javascript"></script> 
    <script src="<%: Url.Content("~/Scripts/labelInfield.js") %>" type="text/javascript"></script>
</asp:Content>




