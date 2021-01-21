<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Home.Index.HRMedinetPath %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="page block">
    <div class="span-14 column">
        <h2 class="path">
            <%: ViewRes.Views.Home.Index.HRMedinetPath %>
        </h2>
        <div class="linea-sistema-footer"></div>
        <div class="span-22 append-2 column last">
            <div class="col1">
                <p> <%: ViewRes.Views.Home.Index.WelcomeText1 %></p> <br/>
                <p><%: ViewRes.Views.Home.AboutUs.WhoAreWeAnswer %></p>
                <br/>
                <p> <%: ViewRes.Views.Home.Index.WelcomeText2 %></p> <br/>
	            <p> <%: ViewRes.Views.Home.Index.WelcomeText3 %></p> <br/>
	            <p> <%: ViewRes.Views.Home.Index.WelcomeText4 %></p>
            </div>
            <div class="linea-sistema-footer"></div>
            <div class="col1 alignJustify">
                <h3 class="path txt-orange">
                    <%: ViewRes.Views.Home.Brochure.GlobalScopeTitle %>
                </h3>
                <div class="linea-sistema-footer"></div>
                <p><%: ViewRes.Views.Home.Brochure.GlobalScopeText %></p>
            </div>
            <div class="linea-sistema-footer"></div>
            <div class="col1 alignJustify">
                <h3 class="path txt-orange">
                    <%: ViewRes.Views.Home.Brochure.AutomatedDataCollectionTitle%>
                </h3>
                <div class="linea-sistema-footer"></div>
                <p><%: ViewRes.Views.Home.Brochure.AutomatedDataCollectionText%></p>
            </div>
            <div class="linea-sistema-footer"></div>
            <div class="col1 alignJustify">
                <h3 class="path txt-orange">
                    <%: ViewRes.Views.Home.Brochure.AutomatedAnalysisTitle%>
                </h3>
                <div class="linea-sistema-footer"></div>
                <p><%: ViewRes.Views.Home.Brochure.AutomatedAnalysisText%></p>
            </div>
        </div>
        <div class="linea-sistema-footer"></div>
        <div class="alignRight span-24 column last">
            <%: Html.ActionLink(ViewRes.Views.Home.Services.BackToServices,"Services","Home") %>
        </div>
    </div>
    <div class="span-8 column last">
            <!--Division de la derecha--> 
        <div id="login-box" class="column">
            <% Html.RenderPartial("PartialLogOn"); %>
        </div><!--Division de la derecha--> 
        <div id="twitter" class="column">
            <% Html.RenderPartial("TwitterWidget");%>  
        </div>
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
