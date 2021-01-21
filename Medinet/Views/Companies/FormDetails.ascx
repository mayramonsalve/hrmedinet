<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.CompanyViewModel>"%>

<div class="span-24 last"> 
    <%if (Model.companyType == "Owner")
    {%>
        <div class="span-24 last"><h4><%: ViewRes.Views.Company.Create.DropDownCompaniesTypes %></h4></div>
        <div class="span-24 last"><%: Model.company.CompaniesType.Name %></div>
    <% } %>      
    <%if (Model.company.CompanySector != null)
    {%>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.CompanySector_Id) %></h4></div>
        <div class="span-24 last"><%: Model.company.CompanySector.Name %></div>
        <div class="column span-24 last"><h4><%: Html.LabelFor(model => model.company.ShowClimate)%></h4></div>
        <%if(Model.company.ShowClimate)
            { %>
            <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.True%></div>
        <%}
            else
            { %>
            <div class="span-24 last"><%: ViewRes.Views.Shared.Shared.False%></div>
        <%} %>
    <% } %>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Name) %></h4></div>
        <div class="span-24 last"><%: Model.company.Name %></div> 
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Number) %></h4></div>
        <div class="span-24 last"><%: Model.company.Number %></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Url) %></h4></div>
        <div class="span-24 last"><% if (Model.company.Url != null){ %>
							        <%: Model.company.Url %>
                                <% }
                                    else{ %>
                                    <%: ViewRes.Views.Shared.Shared.NoUrl %>
                                <% } %></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Phone) %></h4></div>
        <div class="span-24 last"><%: Model.company.Phone %></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Contact) %></h4></div>
        <div class="span-24 last"><%: Model.company.Contact %></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Address) %></h4></div>
        <div class="span-24 last"><%: Model.company.Address %></div>
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Image) %></h4></div>
        <div class="span-24 last"><% if (Model.company.Image != null && Model.company.Image != "System.Web.HttpPostedFileWrapper")
                                        { %>
							        <img src="../../Content/Images/Companies/<%: Model.company.Image %>"
                                        alt="<%: ViewRes.Views.Shared.Shared.NoImage %>" width="100" height="100" />
                                <% } %>
                                <% else{
                                        %>
                                    <img src="../../Content/Images/Companies/<%: Model.company.CompaniesType.Name %>Image.png"
                                        alt="<%: ViewRes.Views.Shared.Shared.NoImage %>" width="100" height="100" />
                                <% } %>
        </div>
    </div>   
           