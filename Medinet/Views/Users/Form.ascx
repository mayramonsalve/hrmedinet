<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.UserViewModel>" %>

            <div class="span-24 column last">
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.FirstName) %></h4>
                    <%: Html.TextBoxFor(model => model.user.FirstName, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.FirstName)%></div>
                </div>
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.LastName) %></h4>
                    <%: Html.TextBoxFor(model => model.user.LastName, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.LastName)%></div>
                </div>
                <div class="column span-6 apend-6 last">
                    <h4><%: Html.LabelFor(model => model.user.Image) %></h4>
                    <%: Html.TextBoxFor(model => model.user.Image, new { @class = "input-background short", @type = "file", @name = "Image", @id = "File" })%> 
                    <div><%: Html.ValidationMessageFor(model => model.user.Image)%></div>
                    <% if (Model.user.Image != null && Model.user.Image != "")
                       {%>
                        <div>
                            <a href="/Content/Images/Users/<%=Model.user.Image %>" target="_blank"><%: ViewRes.Views.User.Edit.ViewImage %></a>
                        </div>
                    <%} %>
                </div>
            </div>

            <div class="span-24 column last">
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.IdNumber) %></h4>
                    <%: Html.TextBoxFor(model => model.user.IdNumber, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.IdNumber)%></div>
                </div>
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.Email) %></h4>
                    <%: Html.TextBoxFor(model => model.user.Email, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.Email)%></div>
                </div>
                <div class="column span-6 apend-6 last">
                    <h4><%: Html.LabelFor(model => model.user.Role_Id) %></h4>
                    <%: Html.DropDownListFor(model => model.user.Role_Id, Model.rolesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%> 
                    <div><%: Html.ValidationMessageFor(model => model.user.Role_Id)%></div>
                </div>
            </div>

            <div class="span-24 column last">
                 <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.ContactPhone) %></h4>
                    <%: Html.TextBoxFor(model => model.user.ContactPhone, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.ContactPhone)%></div>
                </div>
                <div class="column span-12 apend-6 last">
                    <h4><%: Html.LabelFor(model => model.user.Address) %></h4>
                    <%: Html.TextBoxFor(model => model.user.Address, new { @class = "input-background xlarge" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.Address)%></div>
                </div>
                <%--<div class="column span-6 apend-3 last"> &nbsp; </div>--%>
            </div>

            <div class="span-24 column last">
            <%if (Model.companyType == "Customer")
              {
                  Response.Write( Html.HiddenFor(model=>model.user.Company_Id));
                  if (Model.locationsList.Count() > 0)
                  {  %>
                    <div class="column span-6">
                        <h4><%: Html.LabelFor(model => model.user.Location_Id)%></h4>
                        <%: Html.DropDownListFor(model => model.user.Location_Id, Model.locationsList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                        <div><%: Html.ValidationMessageFor(model => model.user.Location_Id)%></div>
                    </div>
                    <div class="column span-6">&nbsp;</div>
                    <div class="column span-6 apend-6 last"> &nbsp; </div>
             <%  }
              }
              else
              { %>
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.Company_Id)%></h4>
                    <%: Html.DropDownListFor(model => model.user.Company_Id, Model.companiesList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.Company_Id)%></div>
                </div>
                <div class="column span-6">  &nbsp;  </div>
                <div class="column span-6 apend-6 last">  &nbsp;</div>
            <%} %>
            </div>

             <div id="customer" class="span-24 column last"  style="display:none;">
                <div class="column span-6">
                    <h4><%: Html.LabelFor(model => model.user.Location_Id)%></h4>
                    <%: Html.DropDownListFor(model => model.user.Location_Id, Model.locationsList, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" })%>
                    <div><%: Html.ValidationMessageFor(model => model.user.Location_Id)%></div>
                </div>
                <div class="column span-6">

                </div>
                <div class="column span-6 apend-6 last"></div>
            </div>