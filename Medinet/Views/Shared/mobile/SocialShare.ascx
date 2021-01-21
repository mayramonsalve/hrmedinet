<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<!-- AddThis Button BEGIN -->
<div class="addthis_toolbox addthis_default_style addthis_32x32_style" addthis:url="http://www.hrmedinet.com/">
    <span style="float:left"><strong><%: ViewRes.Views.Shared.Shared.Share %>:</strong></span>
    <a class="addthis_button_facebook" addthis:url="http://www.facebook.com/hrmedinet"></a>
    <a class="addthis_button_twitter"></a>
    <a class="addthis_button_linkedin"></a>
    <a class="addthis_button_google_plusone_share"></a>
    <a class="addthis_button_yammer"></a>
    <a class="addthis_button_gmail"></a>
</div>
<h1></h1>
<script type="text/javascript">
    var addthis_config = { "data_track_addressbar": true };
    var addthis_share = 
    { 
        templates: {
                       twitter: '<%: ViewRes.Views.Shared.Shared.Message %>',
                   }
    };
</script>
<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5212484126bd4b8f"></script>

<!-- AddThis Button END -->