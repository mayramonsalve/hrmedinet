<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.User>" %>

        <div class="span-24 column last"> 
            <div class="span-24 last">
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.UserName) %></h4>
                    <%: Model.UserName %>
                </div>
                <div class="column span-7">
                </div>
                <div class="column span-7 last">
                </div>
            </div>

            <div class="span-24 last">
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.FirstName) %></h4>
                    <%: Model.FirstName %>
                </div>
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.LastName) %></h4>
                    <%: Model.LastName %>
                </div>
                <div class="column span-7 last">
                    <h4><%: Html.LabelFor(model => model.Image) %></h4>
                    <% if (Model.Image != null && Model.Image != "System.Web.HttpPostedFileWrapper")
                       { %>
						<img src="../../Content/Images/Users/<%: Model.Image %>"
                        alt="<%: ViewRes.Views.Shared.Shared.NoImage %>" width="100" height="100" />
                    <% } %>
                    <% else{ %>
                        <img src="../../Content/Images/Users/userDefault.png"
                            alt="<%: ViewRes.Views.Shared.Shared.NoImage %>" width="100" height="100" />
                    <% } %>
                </div>
            </div>

            <div class="span-24 last">
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.IdNumber) %></h4>
                    <%: Model.IdNumber %>
                </div>
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.Email) %></h4>
                    <%: Model.Email %>
                </div>
                <div class="column span-7 last">
                    <h4><%: Html.LabelFor(model => model.Role_Id) %></h4>
                    <%: Model.Role.Name %>
                </div>
            </div>

            <div class="span-24 last">
                <div class="column span-7">
                    <h4><%: Html.LabelFor(model => model.ContactPhone) %></h4>
                    <%: Model.ContactPhone %>
                </div>
                <div class="column span-14 last">
                    <h4><%: Html.LabelFor(model => model.Address) %></h4>
                    <%: Model.Address %>
                </div>
            </div>

            <div class="span-24 last">
            <%if (Model.Company.CompaniesType.Name == "Customer")
              {%>
                <div class="column span-7">
                    <% if (Model.Location != null)
                               { %>
                                <h4><%: Html.LabelFor(model => model.Location_Id)%></h4>
							    <%: Model.Location.Name%>
                            <% } %>
                </div>
                <div class="column span-7">&nbsp;</div>
                <div class="column span-7 last"> &nbsp; </div>
             <%}
              else
              { %>
                <div class="column span-7">
                <h4><%: Html.LabelFor(model => model.Company_Id)%></h4>
                 <% if (Model.Company != null){ %>
							    <%: Model.Company.Name%>
                            <% } %>
                            <% else{ %>
                                <%: Html.Label("-") %>
                            <% } %>
                </div>
                <div class="column span-7"> &nbsp; </div>
                <div class="column span-7 last">  &nbsp;</div>
            <%} %>
            </div>
        </div>

