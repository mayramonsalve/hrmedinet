<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime>" %>
   <%string name = ViewData.TemplateInfo.HtmlFieldPrefix;%>  
   <%string id = name.Replace(".", "_");%>  
   <div class="editor-label">  
      <h4><%: Html.LabelFor(model => model) %>  </h4>
  </div>  
  <div class="editor-field">  
      <%: Html.TextBox("", Model.ToString(ViewRes.Views.Shared.Shared.Date), new { @class = "input-background tinydate" })%>  
      <%: Html.ValidationMessageFor(model => model) %>  
  </div>  
    
  <script type="text/javascript">
      $(function () {
          var dates = $("#<%=id%>").datepicker({
              dateFormat: '<%: ViewRes.Views.Shared.Shared.DateScript %>',
              changeMonth: true,
              changeYear: true,
              dayNamesMin: ['<%: ViewRes.Views.Shared.Shared.Sun %>', '<%: ViewRes.Views.Shared.Shared.Mon %>', '<%: ViewRes.Views.Shared.Shared.Tue %>', '<%: ViewRes.Views.Shared.Shared.Wed %>', '<%: ViewRes.Views.Shared.Shared.Thu %>', '<%: ViewRes.Views.Shared.Shared.Fri %>', '<%: ViewRes.Views.Shared.Shared.Sat %>'],
              monthNamesShort: ['<%: ViewRes.Views.Shared.Shared.Jan %>', '<%: ViewRes.Views.Shared.Shared.Feb %>', '<%: ViewRes.Views.Shared.Shared.Mar %>', '<%: ViewRes.Views.Shared.Shared.Apr %>', '<%: ViewRes.Views.Shared.Shared.May %>', '<%: ViewRes.Views.Shared.Shared.Jun %>', '<%: ViewRes.Views.Shared.Shared.Jul %>', '<%: ViewRes.Views.Shared.Shared.Aug %>', '<%: ViewRes.Views.Shared.Shared.Sep %>', '<%: ViewRes.Views.Shared.Shared.Oct %>', '<%: ViewRes.Views.Shared.Shared.Nov %>', '<%: ViewRes.Views.Shared.Shared.Dec %>']
          });
      });
  </script>  



