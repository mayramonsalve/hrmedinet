<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ContactUsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.ContactUs.Details.TitleDetails %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
       <h2 class="path"><%= ViewRes.Views.ContactUs.Details.PathDetails %></h2>
        <div class="linea-sistema-footer"></div>
	    <% using (Html.BeginForm()) { %>
            <div class="span-23 prepend-1 last"> 
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Date) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Date.ToString(ViewRes.Views.Shared.Shared.Date)%></div>
                
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Company) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Company.ToString()%></div>                  

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Country_Id)%></h4></div>
                <div class="span-24 last"><%: Model.cont.Country.Name.ToString()%></div>

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Name) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Name.ToString()%></div>  

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Email) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Email.ToString()%></div> 

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Phone) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Phone.ToString()%></div> 

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Address) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Address.ToString()%></div> 

                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Description) %></h4></div>
                <div class="span-24 last"><%: Model.cont.Description.ToString()%></div> 
            </div>
        <% } %>
    </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "ShowContacts") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>
