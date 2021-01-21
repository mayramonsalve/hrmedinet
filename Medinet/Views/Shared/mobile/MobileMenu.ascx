<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="menu" class="menu rounded">
<%--    <div id="lang">
        <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="en",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-usa.png" border="0" /></a>&nbsp;
        <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="es",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-spain.png" border="0" /></a>
    </div>--%>
    <a href="/" data-ajax="false" class="items rounded-left"><img src="/Content/mobile/Images/icon-home.png" border="0" /></a>
    <a href=<%: @Url.Action("ContactUs", "Home")%> data-ajax="false" class="items"><img src="/Content/mobile/Images/icon-letter.png" border="0" /></a>
<%--    <% if (Request.IsAuthenticated)
       { %>
       <a href=<%: @Url.Action("LogOff", "Account")%> data-ajax="false" class="items" ><img src="/Content/mobile/Images/icon-logout.png" border="0" /></a>
    <% }
        %>--%>
    <% if (Request.IsAuthenticated)
       { 
        %>
           <a href=<%: @Url.Action("LogOff", "Account")%> data-ajax="false" class="items" ><img src="/Content/mobile/Images/icon-logout.png" border="0" /></a>
       <% } %>
        <div id="lang-b">
            <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="en",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-usa.png" border="0" /></a>&nbsp;
            <a href="<%: Url.Action("ChangeCulture","Account",new {@lang="es",@returnUrl = this.Request.RawUrl})%>" rel="external"><img src="/Content/mobile/Images/icon-spain.png" border="0" /></a>
        </div>
<%--        <div id="lang">
            <iframe allowtransparency="true" frameborder="0" scrolling="no"
              src="//platform.twitter.com/widgets/follow_button.html?screen_name=hrmedinet&lang=<%: Session["culture"] %>&show_count=false"
              style="width:60px; height:20px;"></iframe>
            <iframe src="//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.facebook.com%2Fhrmedinet%2Flike&amp;width=80&amp;height=21&amp;colorscheme=light&amp;layout=button_count&amp;action=like&amp;show_faces=false&amp;send=false&amp;appId=150209165177876" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:80px; height:21px;" allowTransparency="true"></iframe>
        </div>--%>
        <%--    <div class="addthis_toolbox addthis_32x32_style addthis_default_style" id="lang-c">
                <span style="float:left;padding-top:0px;"><strong>Follow Us</strong></span>
                <a class="addthis_button_facebook_follow" addthis:userid="hrmedinet"></a>
                <a class="addthis_button_twitter_follow" addthis:userid="hrmedinet"></a>
                <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5212484126bd4b8f"></script>
                </div>--%>
</div>