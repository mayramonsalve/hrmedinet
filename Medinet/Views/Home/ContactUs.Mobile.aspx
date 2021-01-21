<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ContactUsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Models.ContactUs.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainContactUs" class="basic">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<%--        <script type="text/javascript">
            $(document).on('pageinit', '#mainContactUs', function (e) {
                $.validator.unobtrusive.parse($(this).find('form'));
            });
        </script>--%>
         <div class="box rounded">
            <h1><%= ViewRes.Models.ContactUs.Path%></h1>
            <div >
                <img src="/Content/Images/Flags/Venezuela.png" alt="Venezuela" width="5%" />
            </div>
            <div >
                Zona rental Univ. Metropolitana. Edif Uno (Otepi). Piso 1. Oficina 1.
                Urb. Terrazas del Ávila<br />
                Caracas - Venezuela<br />
                (+58) 212-241.07.18<br />
                (+58) 212-241.38.53
            </div>
            <div class="lineSeparator"></div>
            <div >
                <img src="/Content/Images/Flags/United_States_of_America.png" alt="USA" width="5%" />
            </div>
            <div >
                535 Valencia Ave. #1. <br />
                FL-33134 Coral Gables - USA<br />
                (+1) 305-791.91.97
            </div>
            <div class="lineSeparator"></div>
            <div >
                <img src="/Content/Images/Flags/Colombia.png" alt="Colombia" width="5%" />
            </div>
            <div>
                <strong>Inversiones Reltech Servicios SAS</strong><br />
                Calle 15A #79-366<br />
                Medellín - Colombia <br />
                (+57) 318 4188645
            </div>
		   <%-- <% using (Html.BeginForm()) { %>
                <% Html.EnableClientValidation(); %>  
            <div>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Company) %>
                    <%: Html.TextBoxFor(model => model.cont.Company)%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Company)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Country_Id) %>
                    <%: Html.DropDownListFor(model => model.cont.Country_Id, Model.countries, ViewRes.Scripts.Shared.Select)%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Country_Id)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Name) %>
                    <%: Html.TextBoxFor(model => model.cont.Name)%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Name)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Email) %>
                    <%: Html.TextBoxFor(model => model.cont.Email, new { PlaceHolder = "helloWorld@world.com" })%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Email)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Phone) %>
                    <%: Html.TextBoxFor(model => model.cont.Phone, new { PlaceHolder = "0212 2223344" })%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Phone)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Address) %>
                    <%: Html.TextBoxFor(model => model.cont.Address)%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Address)%></div>
                </fieldset>
                <fieldset data-role="fieldcontain">
                    <legend></legend>
                    <%: Html.LabelFor(model => model.cont.Description) %>
                    <%: Html.TextAreaFor(model => model.cont.Description)%>
                    <div class="errorMsg"><%: Html.ValidationMessageFor(model => model.cont.Description)%></div>
                </fieldset>
                <h1></h1>
                <input data-icon="check" data-iconpos="right" data-theme="f" value="<%: ViewRes.Views.Shared.Shared.SendButton %>" type="submit" />
            </div>
            <% } %>  --%> 
        </div> 
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CssContent" runat="server">
    <style>
        .lineSeparator {
            padding-bottom: 5px;
            color: #01A0E4;
            border-bottom: dotted 1px #01A0E4;
            margin-top: 10px;
            margin-bottom: 20px;
            height: 2px;
        }
    </style>
</asp:Content>
