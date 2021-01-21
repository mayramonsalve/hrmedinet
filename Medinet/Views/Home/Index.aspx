<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %>
    <%: ViewRes.Views.Home.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (!Request.IsAuthenticated)
        { %>
    <div class="slider-wrapper theme-default">
        <% //Html.RenderPartial("Slider"); %>
        <img style="width: 100%" src="../../Content/Images/Slider_Home/main-new.jpg" alt="HRMedinet"/>
        
        <div id="login-box" class="padding_login-box">

            <% Html.RenderPartial("PartialLogOn"); %>
        </div>
    </div>
    <div class="page block moreSpace">
        <div class="column span-7 append-1">
            <div class="txt-index-home alignJustify column">
                <span class="txt-orange">HRMedinet</span>
                <%: Html.Raw(ViewRes.Views.Home.Brochure.MainText.Replace("&lt;","<").Replace("&gt;",">")) %>
            </div>
        </div>
        <div class="column span-7 append-1">
            <div class="txt-index-home alignJustify">
                <span class="txt-orange">HRMedinet</span>
                <%: ViewRes.Views.Home.Brochure.VarietyOfRportsText%>
            </div>
            <div class="f-right">
                <a href="<%:Url.Action("HRMedinet","Home") %>">
                    <span class="column more" title="<%: ViewRes.Views.Shared.Shared.See %>"></span>
                </a>
            </div>
        </div>
        <div class="column span-7 last">
            <strong><%: ViewRes.Views.Home.Brochure.FreeDemo%></strong>
            <%--<% Html.RenderPartial("TwitterWidget");%>--%>
        </div>
    </div>
    <div class="clear"><br /></div>
    <% Html.RenderPartial("Footer"); %>
   <%}else{ %>
        <div id="contenido-sistema" class="block">
            <%if (User.Identity.Name.ToLower() == "todovision")
              {%>
              <link href="/Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
              <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
            <div class=" column span-7  ">
            <%}
              else
              {%>
            <div class=" column span-12  ">
            <%} %>
                <h1><%: ViewRes.Views.Home.Index.Welcome %></h1>
                <p> <%: ViewRes.Views.Home.Index.WelcomeText1 %></p>
                <p> <%: ViewRes.Views.Home.Index.WelcomeText2 %></p>
	            <p> <%: ViewRes.Views.Home.Index.WelcomeText3 %></p>
	            <p> <%: ViewRes.Views.Home.Index.WelcomeText4 %></p>
            </div>
            
            <%if (User.Identity.Name.ToLower() == "todovision" || User.Identity.Name.ToLower() == "cigeh" || User.Identity.Name.ToLower() == "acrip")
              {%>
              <div class="span-15 last column sidebar-derecha-sistema"> 
                <% if (User.Identity.Name.ToLower() == "acrip")
                   {%>
                <%Html.RenderPartial("/Views/Home/ConferenceColombia.ascx"); %>
                <% }
                   else
                   { %>
                <%Html.RenderPartial("/Views/Home/Conference.ascx"); %>
                <% } %>
              </div>
            <%}
              else {%>
            <div class="span-11 last column sidebar-derecha-sistema"> 
                <%: Html.Hidden("Cancel", ViewRes.Views.Shared.Shared.CancelLabel, new { id = "Cancel" })%>
                <%: Html.Hidden("Validate", ViewRes.Views.Shared.Shared.SelectOptionRequired, new { id = "Validate" })%> 
                <%: Html.Hidden("Select", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%>
<%--                <% if (Roles.IsUserInRole("Administrator")) { %>
                    <a href="/Evaluations/AllTickets" style="text-align:center;"><h2>Tickets</h2></a>
                <% } %>  --%>
                <% if (Roles.IsUserInRole("HRAdministrator") || Roles.IsUserInRole("HRCompany") || Roles.IsUserInRole("CompanyManager") || Roles.IsUserInRole("FreeReports"))
                   {
                       string rol;
                       if (Roles.IsUserInRole("HRAdministrator"))
                           rol = "HRAdministrator";
                       else if (Roles.IsUserInRole("HRCompany"))
                           rol = "HRCompany";
                       else if (Roles.IsUserInRole("CompanyManager"))
                           rol = "CompanyManager";
                       else
                           rol = "FreeReports";
                       %>
                   <%: Html.Hidden("Rol", rol, new { id = "Rol" })%>
                    <div class="prepend-2 span-8 append-2 column iconos-sidebar">
                        <h2><%: ViewRes.Views.Home.Index.Reports%></h2>                             
                        <a href="<%: Url.Action("ReportsList","ChartReports")%>" class="imagen-reportes column span-16 alignCenter">
                        </a>
                    </div>
                    <div class="prepend-2 span-8 append-2 column last iconos-sidebar">
                        <% if (rol != "FreeReports")
                            { %>
                        <h2><%: ViewRes.Views.Home.Index.GlobalClimate%></h2>
                        <a href="#" id="Climate" class=" imagen-clima column span-16  alignCenter">
                        </a>
                        <%} %>
                    </div>
                <%}
                   else
                   {
                       if (Roles.IsUserInRole("Administrator"))
                       {
                           if (User.Identity.Name.ToLower() == "administrator")
                           {%>
                            <div class="prepend-2 span-8 append-2 column iconos-sidebar">
                                <h2><%: ViewRes.Views.Home.Index.Reports%></h2>
                                <a href="<%: Url.Action("TestReport","Reports")%>" class="imagen-reportes column span-18 alignCenter"></a>
                            </div>
                            <div class="prepend-2 span-8 append-2 column last iconos-sidebar">
                                <h2><%: ViewRes.Views.Home.Index.Feedbacks%></h2>
                                <a href="<%: Url.Action("ShowFeedbacks","Feedbacks")%>" class="imagen-caja-comentarios column span-18 alignCenter"></a>
                            </div>
                       <%}
                         else{%>
                                <div class="prepend-2 span-8 append-2 column iconos-sidebar">
                                    <h2><%: ViewRes.Views.Shared.Shared.Users%></h2>
                                    <a href="<%: Url.Action("Index","Users")%>" class="imagen-lista-usuarios column span-18 alignCenter"></a>
                                </div>
                                <div class="prepend-2 span-8 append-2 column last iconos-sidebar">
                                    <h2><%: ViewRes.Views.Shared.Shared.Companies%></h2>
                                    <a href="<%: Url.Action("Index","Companies")%>" class="imagen-companias-index column span-18 alignCenter"></a>
                                </div>
                        <%}
                       }
                       else
                       {%>
                            <div class="prepend-2 span-8 append-2 column iconos-sidebar">
                                <h2><%: ViewRes.Views.ChartReport.Graphics.UserTab%></h2>
                                <a href="<%: Url.Action("Index","Users")%>" class="imagen-lista-usuarios column span-18 alignCenter"></a>
                            </div>
                            <div class="prepend-2 span-8 append-2 column last iconos-sidebar">
                                <h2><%: ViewRes.Views.ChartReport.Graphics.LocationTab%></h2>
                                <a href="<%: Url.Action("Index","Locations")%>" class="imagen-lista-sucursales column span-18 alignCenter"></a>
                            </div>
                        <%}
                   }%>
<%--                <div class="prepend-7 span-10 append-7 column last iconos-sidebar">
                    <div class="imagen-ayuda column alignCenter">
                        <a href="<%: Url.Action("FAQ","Home")%>"></a>
                        <img src=<%: ViewRes.Views.Home.Index.Help %> alt=""/>
                    </div>
                </div>--%>
            </div>
            <%} %>
        </div>
        <%--<div class="clear"><h6><%: ViewRes.Views.Home.Index.DisplayProblems %></h6></div>--%>
   <% } %>
        <%--<div id="Dialog" title="<%= ViewRes.Views.Home.Index.ChooseCompany %>">
            <p class="validateTips"><%= ViewRes.Views.Home.Index.SelectCompany %></p>
		    <div id="DivDDL" class="editor-field">
			    
		    </div>
        </div>--%>
        <div id="Dialog" title="<%= ViewRes.Views.Report.ReportsList.ChooseTest %>">
            <p class="validateTips"><%= ViewRes.Views.Report.ReportsList.SelectTest %></p>
		    <div id="DivDDL" class="editor-field">
			    
		    </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.cookie.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.infieldlabel.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/home-view.js?mm=0609") %>" type="text/javascript"></script>
</asp:Content>