<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FAQ
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 column last">
        <div class="span-24 last">
            <h2 class="path span-10">Frequently Asked Questions
            <a class="button expand path" href="#">Expand</a></h2>
        </div>
        <div class="linea-sistema-footer"></div>
        <div class="span-22 prepend-1 append-1 column last">
            <dl>
                <dt><span class="icon"></span>-- How does this FAQ section work?</dt>
                    <dd>With the help of jQuery and YQL, this script pulls the latest data ..</dd>
            </dl>
        </div>
    </div>
<br/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/faq.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/faq.js" type="text/javascript"></script>
</asp:Content>
