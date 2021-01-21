<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
    <div data-role="page" id="two" class="basic main">
        <div id="header" class="basic">
        </div>

        <div id="menu" class="rounded">
            <div id="lang">
                <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="en",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-usa.png" border="0" /></a>&nbsp;
                <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="es",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-spain.png" border="0" /></a>
            </div>
            <a href="http://localhost:32900/" class="items rounded-left"><img src="/Content/mobile/Images/icon-home.png" border="0" /></a>
            <a href="" class="items" id="sm"><img src="/Content/mobile/Images/icon-folder.png" border="0" /></a>
            <a href="" class="items"><img src="/Content/mobile/Images/icon-letter.png" border="0" /></a>
        </div>

        <div id="sub-menu">
            <a href="" class="rounded">Quienes somos</a>
            <a href="" class="rounded">Registrse</a>
            <a href="" class="rounded">Configuracion</a>
            <a href="" class="rounded">Contacto</a>
        </div>

        <div id="content" class="basic">
            <p><a href="#main" data-direction="reverse" data-role="button" data-theme="b">Back to page "main"</a></p>	
        </div>

        <div id="footer" class="basic">
            <a href="http://twitter.com/@hrmedinet" target="_blank">@hrmedinet <img src="/Content/mobile/Images/icon-tw.png" border="0" /></a><br />
            <a href="<%: Url.Action("SwitchToDesktopVersion","Mobile")%>" rel="external">www.hrmedinet.com</a>
        </div>
    </div>
</asp:Content>
