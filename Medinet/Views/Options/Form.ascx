<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.OptionViewModel>" %>
            
    <div class="span-24 last"> 
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.option.Questionnaire_Id) %></h4></div>
        <div class="span-24 last"><%: Html.DropDownListFor(model => model.option.Questionnaire_Id, Model.questionnairesList, ViewRes.Scripts.Shared.Select, new { @class = "required, input-background short" })%></div> 
        <div><%: Html.ValidationMessageFor(model => model.option.Questionnaire_Id)%></div>
                
        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.option.Text)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.option.Text, new { @class = "input-background short" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.option.Text)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.option.Value)%></h4></div>
        <div class="span-24 last"><%: Html.TextBoxFor(model => model.option.Value, new { @class = "input-background tiny" })%></div>
        <div><%: Html.ValidationMessageFor(model => model.option.Value)%></div>

        <div class="span-24 last"><h4><%: Html.LabelFor(model => model.option.Image) %></h4></div>
        <div class="span-24 last">
            <%: Html.TextBoxFor(model => model.option.Image, new { @class = "input-background short", @type = "file", @name = "Image", @id = "File" })%> 
            <div><%: Html.ValidationMessageFor(model => model.option.Image)%></div>
            <% if (Model.option.Image != null && Model.option.Image != "")
                {%>
                <div>
                    <a href="/Content/Images/Options/<%=Model.option.Image %>" target="_blank"><%: ViewRes.Views.User.Edit.ViewImage %></a>
                </div>
            <%} %>
        </div>
    </div>