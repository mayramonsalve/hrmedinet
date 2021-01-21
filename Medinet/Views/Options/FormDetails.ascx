<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MedinetClassLibrary.Models.Option>" %>

        <div class="span-24 last"> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Questionnaire_Id) %></h4></div>
                <div class="span-24 last"><%: Model.Questionnaire.Name%></div> 
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Text)%></h4></div>
                <div class="span-24 last"><%: Model.Text%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Value)%></h4></div>
                <div class="span-24 last"><%: Model.Value%></div>

                <% if (Model.Image != null && Model.Image != "System.Web.HttpPostedFileWrapper")
                   { %>
                    <div class="span-24 last"><h4><%: Html.LabelFor(model => model.Image) %></h4></div>
                    <div class="span-24 last">
						    <img src="../../Content/Images/Options/<%: Model.Image %>"
                            alt="<%: ViewRes.Views.Shared.Shared.NoImage %>" width="50" height="50" />
                   </div>
               <% } %>
         </div>
