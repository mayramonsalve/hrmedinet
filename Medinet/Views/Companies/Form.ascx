<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.CompanyViewModel>" %>
        
         <% bool editDDL;
            string companyType = "";
            string style = "";
             editDDL = Model.company.CompaniesType == null ? false : true;
             if (editDDL)
             {
                 companyType = Model.company.CompaniesType.Name;
             }
             if (Model.companyType == "Owner")
             {
                 if (companyType != "Customer")
                     style = "display:none;";
             }          
             %>

          <div class="span-24 last"> 

              <%if (Model.companyType == "Owner" && companyType!="Owner")
                {%>
                    <div class="span-24 last"><h4> <%: ViewRes.Views.Company.Create.DropDownCompaniesTypes %></h4></div>
                    <div class="span-24 last"><%: Html.DropDownListFor(model => model.company.CompanyType_Id, Model.companiesTypesList, ViewRes.Scripts.Shared.Select, new { @class = "required input-background short" })%></div>
              <% } %>
                <div id="sector" class="span-24 column last" style=<%:style%>>
                    <div class="column span-8">
                        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.CompanySector_Id) %></h4></div>
                        <div class="span-24 last"><%: Html.DropDownListFor(model => model.company.CompanySector_Id, Model.companySectorsList, ViewRes.Scripts.Shared.Select, new { @class="input-background short"})%></div> 
                        <%--<div><%: Html.ValidationMessageFor(model => model.company.CompanySector_Id)%></div>--%>
                    </div>
                    <div class="column span-4 append-12 last">
                        <h4><%: Html.LabelFor(model => model.company.ShowClimate)%></h4><%: Html.CheckBoxFor(model => model.company.ShowClimate, new { @checked = "checked" })%>
                        <div><%: Html.ValidationMessageFor(model => model.company.ShowClimate)%></div>
                    </div>
                </div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Name) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Name, new { @class="input-background short"})%></div> 
                <div><%: Html.ValidationMessageFor(model => model.company.Name)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Number) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Number, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Number)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Url) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Url, new { @class = "input-background short", })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Url)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Phone) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Phone, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Phone)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Contact) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Contact, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Contact)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Address) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Address, new { @class = "input-background short" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Address)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.company.Image) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.company.Image, new { @class = "input-background short", @type = "file", @name = "Image", @id = "File" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.company.Image)%></div>
                       <% if (Model.company.Image != null && Model.company.Image != "")
                       {%>
                        <div>
                            <a href="/Content/Images/Companies/<%=Model.company.Image %>" target="_blank"><%: ViewRes.Views.User.Edit.ViewImage %></a>
                        </div>
                    <%} %>
         </div>   