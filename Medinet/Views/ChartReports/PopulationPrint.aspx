<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <title></title>
</head>
<body>
    <div>
    <%ViewData["print"] = "True"; %>
    <div id="tabGeneral">
        <%ViewData["option"] = "General"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <div id="tabCountry">
        <%ViewData["option"] = "Country"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
        <% foreach (var id in Model.countriesId)
        {%>
            <div id="tabState-<%:id%>">
                <%ViewData["option"] = "State"; %>
                <%ViewData["country_id"] = id; %>
                <%Html.RenderPartial("Population", Model); %>
            </div>
        <%}%>
    <% if (Model.demographicsCount["Region"] > 0)
        { %>
    <div id="tabRegion">
        <%ViewData["option"] = "Region"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <%} %>

    <div id="tabAgeRange">
        <%ViewData["option"] = "AgeRange"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <div id="tabInstructionLevel">
        <%ViewData["option"] = "InstructionLevel"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <% if (Model.demographicsCount["Location"] > 0)
        { %>
    <div id="tabLocation">
        <%ViewData["option"] = "Location"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <%} %>
    <div id="tabPositionLevel">
        <%ViewData["option"] = "PositionLevel"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <div id="tabSeniority">
        <%ViewData["option"] = "Seniority"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <div id="tabGender">
        <%ViewData["option"] = "Gender"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
    <% if (Model.demographicsCount["Performance"] > 0){ %>
    <div id="tabPerformance">
        <%ViewData["option"] = "Performance"; %>
        <%Html.RenderPartial("Population", Model); %>
    </div>
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
            <%}
        }%>  
    </div>
</body>
</html>
