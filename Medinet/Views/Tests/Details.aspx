<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.TestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Test.Details.TitleDetail %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
    
        <h2 class="path"><%= ViewRes.Views.Test.Details.PathDetails %></h2>
        <div class="linea-sistema-footer"></div>
        <%: Html.Hidden("NoData", ViewRes.Views.Test.Create.NoData, new { id = "NoData" })%>
        <%: Html.Hidden("Test_Id", Model.test.Id, new { id = "Test_Id" })%>
        <%: Html.Hidden("Weighted", Model.test.Weighted, new { id = "Weighted" })%>
        
		<% using (Html.BeginForm()) { %>
            <div class="span-23 prepend-1 last"> 
	            <% Html.RenderPartial("FormDetails", Model); %>	
            </div>
        <% } %>
         <div class="span-23 prepend-1 last">
		    <input type="submit"  class="button"  value="<%: ViewRes.Views.Test.Print.PrintButton %>" onclick="Print()" />
	    </div>
    </div>
    <%if (Model.userRole == "HRAdministrator")
      {%>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index", new { @company_id = Model.test.Company_Id })%>
        </div>
     <% } else if(Model.userRole == "HRCompany"){%>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index") %>
        </div>
     <% }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script type="text/javascript">
        function Print() {
            window.location = "/Evaluations/GetMeAnotherPdf?id=" + $('#Test_Id').val()
        };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('table').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bInfo": false,
                "bJQueryUI": true,
                "bRetrieve": true,
                "bSort": false,
                "bAutoWidth": false,
                "oLanguage": {
                    "sEmptyTable": $('#NoData').val()
                }
            });
            if ($("#Weighted").val() == "True") {
                $.post("/Tests/GetWeighingsByTest", { test_id: $("#Test_Id").val() }, function (j) {
                    var weighingsHtml = '';
                    for (var i = 0; i < j.length; i++) {
                        weighingsHtml += '<div class=" span-24 last">';
                        weighingsHtml += '<div class="column span-5"> <h4>' + j[i].weighingCategory + '</h4></div>';
                        weighingsHtml += '<div class="column span-19 last">' + j[i].weighingValue + ' %</div>';
                        weighingsHtml += '</div>';
                    }
                    $("#showWeighings").html(weighingsHtml);
                });
            }
        });
    </script>
</asp:Content>
