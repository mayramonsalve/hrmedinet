<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.FunctionalOrganizationType>" %>

<div class="span-24 last"> 
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.FOTParent_Id)%></h4></div>
    <%if (Model.Parent != null)
      { %>
        <div class="span-24 last"><%: Model.Parent.Name.ToString()%></div>
    <%}
      else
      {%>
        <div class="span-24 last"><%: ViewRes.Views.FunctionalOrganizationType.Details.None %> </div>
    <%} %>
    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Name) %></h4></div>
    <div class="span-24 last"><%: Model.Name.ToString()%></div> 

    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.ShortName) %></h4></div>
    <div class="span-24 last"><%: Model.ShortName.ToString()%></div> 
</div>