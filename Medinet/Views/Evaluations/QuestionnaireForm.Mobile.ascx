<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<% if (Model.test.GroupByCategories)
{ %>
    <% Html.RenderPartial("GroupByCategories.Mobile"); %>
<%}
else
{%>
    <% Html.RenderPartial("GroupBySortOrder.Mobile"); %>
<% } %>