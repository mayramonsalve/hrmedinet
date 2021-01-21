<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.TicketViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<html>
<head>
</head>
<body>
<%--<table border="1">
    <% foreach (Ticket t in Model.tickets)
        {%>
        <tr>
            <td>Id : <%: t.Evaluation_Id %><br />
            <%: ViewRes.Models.Shared.Name %> : <%: t.Name %><br />
            <%: ViewRes.Models.User.Email %>  : <%: t.Email %></td>
        </tr>
    <% } %>
</table>--%>
<table border="1" style="border-style: dotted;border-width:3px;width:100%;">
    <% for (int k = 0;k < Model.tickets.Count(); k++){%>
        <tr>
            <td style="padding:10px;border-style: dotted;border-width:3px;">Id : <%: Model.tickets[k].Evaluation_Id %><br />
            <%: ViewRes.Models.Shared.Name %> : <%: Model.tickets[k].Name %><br />
            <%: ViewRes.Models.User.Email %>  : <%: Model.tickets[k].Email %></td>
        
        <%if (k+1 < Model.tickets.Count()){ 
            k++; %>
            <td style="padding:10px;border-style: dotted;border-width:3px;">Id : <%: Model.tickets[k].Evaluation_Id %><br />
            <%: ViewRes.Models.Shared.Name %> : <%: Model.tickets[k].Name %><br />
            <%: ViewRes.Models.User.Email %>  : <%: Model.tickets[k].Email %></td>
        <% } %>
        </tr>
    <% } %>
</table>
</body>
</html>