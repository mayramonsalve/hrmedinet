<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ContactUsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Models.ContactUs.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/contactUs-views.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.infieldlabel.min.js") %>" type="text/javascript"></script> 
    <script src="<%: Url.Content("~/Scripts/labelInfield.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div class="page block">
     <div id="contenido-sistema-2"> 
            <h2 class="path"><%= ViewRes.Models.ContactUs.Path%></h2>
         <%--<div class="span-13 column colborder">
            <div class="linea-sistema-footer"></div>
		    <% using (Html.BeginForm()) { %>
                <% Html.EnableClientValidation(); %>
			    <%: Html.ValidationSummary(ViewRes.Views.Shared.Errors.ErrorInApplication, new { @class = "errorAplication" })%>
           <div class="span-22 append-2 column last">
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Company) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.cont.Company, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.cont.Company)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Country_Id) %></h4></div>
                <div class="span-24 last"> <%: Html.DropDownListFor(model => model.cont.Country_Id, Model.countries, ViewRes.Scripts.Shared.Select, new { @class = "input-background large" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.cont.Country_Id)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Name) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.cont.Name, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.cont.Name)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Email) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.cont.Email, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.cont.Email)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Phone) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.cont.Phone, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.cont.Phone)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Address) %></h4></div>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.cont.Address, new { @class = "input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.cont.Address)%></div>
                <div class="span-24 last"><h4><%: Html.LabelFor(model => model.cont.Description) %></h4></div>
                <div class="span-24 last"><%: Html.TextAreaFor(model => model.cont.Description, new { @class = "input-background textArea" })%></div>
                <div><%: Html.ValidationMessageFor(model => model.cont.Description)%></div>
			    <div class="button-padding-top">
                    <input type="submit"  class="button" value="<%: ViewRes.Views.Shared.Shared.SendButton %>" />
		        </div>
            </div>
            <% } %>   
        </div>--%>
        <!--Division de la derecha--> 
        <div class="span-23 prepend-1 column last">
            <div class="span-2 column">
                <img src="/Content/Images/Flags/Venezuela.png" alt="Venezuela" width="100%" />
            </div>
            <div class="span-21 prepend-1 column last">
                Zona rental Univ. Metropolitana. Edif Uno (Otepi). Piso 1. Oficina 1.
                Urb. Terrazas del Ávila<br />
                Caracas - Venezuela<br />
                (+58) 212-241.07.18<br />
                (+58) 212-241.38.53
            </div>
            <div class="lineSeparator column span-24"></div>
            <div class="span-2 column">
                <img src="/Content/Images/Flags/United_States_of_America.png" alt="USA" width="100%" />
            </div>
            <div class="span-21 prepend-1 column last">
                535 Valencia Ave. #1. <br />
                FL-33134 Coral Gables - USA<br />
                (+1) 305-791.91.97
            </div>
            <div class="lineSeparator column span-24"></div>
            <div class="span-2 column">
                <img src="/Content/Images/Flags/Colombia.png" alt="Colombia" width="100%" />
            </div>
            <div class="span-21 prepend-1 column last">
                <strong>Inversiones Reltech Servicios SAS</strong><br />
                Calle 15A #79-366<br />
                Medellín - Colombia <br />
                (+57) 318 4188645
            </div>
        </div>
      
    </div>

<%--         <div class="tit-logos alignCenter">
            <label>
                <span class="txt-orange">
                    <%: ViewRes.Views.Home.Index.Ourclients%>
                </span>
            </label>
        </div>
        <div id="carrusel" class="logos">
            <img src="/Content/Images/Icustomers/f-logo-1.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-2.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-3.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-4.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-5.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-6.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-7.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-8.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-9.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-10.jpg"></img>
            <img src="/Content/Images/Icustomers/f-logo-11.jpg"></img>
        </div>--%>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>
