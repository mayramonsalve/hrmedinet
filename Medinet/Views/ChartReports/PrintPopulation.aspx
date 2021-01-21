<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.PopulationTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="alignCenter"><%:ViewRes.Views.ChartReport.Graphics.PrintPopulation %></h2>
    <br/>
    <%ViewData["print"] = "True"; %>
    <div id="tabGeneral">
        <%ViewData["option"] = "General"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <div id="tabCountry">
        <%ViewData["option"] = "Country"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
        <% foreach (var id in Model.countriesId)
        {%>
            <div id="tabState-<%:id%>">
                <%ViewData["option"] = "State"; %>
                <%ViewData["country_id"] = id; %>
                <%Html.RenderPartial("Population", Model); %>
            </div>
            <br/>
        <%}%>
    <% if (Model.demographicsCount["Region"] > 0)
        { %>
    <div id="tabRegion">
        <%ViewData["option"] = "Region"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <%} %>

    <div id="tabAgeRange">
        <%ViewData["option"] = "AgeRange"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <div id="tabInstructionLevel">
        <%ViewData["option"] = "InstructionLevel"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <% if (Model.demographicsCount["Location"] > 0)
        { %>
    <div id="tabLocation">
        <%ViewData["option"] = "Location"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <%} %>
    <div id="tabPositionLevel">
        <%ViewData["option"] = "PositionLevel"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <div id="tabSeniority">
        <%ViewData["option"] = "Seniority"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <div id="tabGender">
        <%ViewData["option"] = "Gender"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <% if (Model.demographicsCount["Performance"] > 0){ %>
    <div id="tabPerformance">
        <%ViewData["option"] = "Performance"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <br/>
    <% } %>
        <% foreach (var v in Model.FO)
        {
            if (Model.GetFOCount(v.Key) > 0)
            {%> 
                <div id="tabFO-<%:v.Key%>">
                    <%ViewData["option"] = "FunctionalOrganizationType"; %>
                    <%ViewData["FO_id"] = v.Key; %>
                    <%Html.RenderPartial("Population", Model); %>
                </div>
                <br/>
            <%}
        }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<div id="header" class="block">  
    <a href="#" class="span-4 column"><span id="logo"></span></a>
    <div class="span-4 prepend-18 append-2"><% if (Model.test.Company.Image != null && Model.test.Company.Image != "System.Web.HttpPostedFileWrapper")
                                    { %>
							    <img src="../../Content/Images/Companies/<%: Model.test.Company.Image %>"
                                    alt="" width="100" height="100" />
                            <% } %>
                            <% else{
                                    %>
                                <img src="../../Content/Images/Companies/<%: Model.test.Company.CompaniesType.Name %>Image.png"
                                    alt="" width="100" height="100" />
                            <% } %></div>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
</asp:Content>
