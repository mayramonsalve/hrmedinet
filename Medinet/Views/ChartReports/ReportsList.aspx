<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.Report.ReportsList.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Hidden("TestCoockie", Session["TestCookie"], new { id = "TestCookie" })%>
    <% string roleClass = Roles.IsUserInRole("FreeReports") ? "rep_demo" : ""; %>
    <%: Html.Hidden("RepDemo", roleClass, new { id = "RepDemo" })%>
    <div id="contenido-small-list" style="height: 725px; margin-top: 100px;" class="span-24 last">
          <div class="span-24 column last">
                <h2 class="path" style="margin-bottom: 10px;"><%:ViewRes.Views.Report.ReportsList.Path %></h2>
                <% if (User.Identity.Name == "manager65")
                   { %>
                <div class="span-24 column last button-padding-top">
                    <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="PopulationReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.PopulationImage %>> </div></a></div>
                    <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="TextReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.TextImage %>> </div></a></div>
                    <div class="prepend-2 span-4 append-2 column block alignCenter last">
                        <a href="#" id="FrequencyReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.FrequencyImage %>> </div></a>
                    </div>
                </div>
                   <%}
                   else
                   {%>
                        <div class="span-24 column last button-padding-top">
                            <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="PopulationReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.PopulationImage %>> </div></a></div>
                            <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="UnivariateReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.UnivariateImage %>> </div></a></div>
                            <div class="prepend-2 span-4 append-2 column block alignCenter last"><a href="#" id="CategoryReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.CategoryImage %>> </div></a></div>
                        </div>
                        <div class="clear"></div>
                        <div class="span-24 column last button-padding-top">
                                <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="RegressionReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.RegressionImage %>> </div></a></div>
                                <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="FinalReport"><div id=<%: ViewRes.Views.ChartReport.ReportsList.ResultImage %>> </div></a></div>
                                <div class="prepend-2 span-4 append-2 column block alignCenter last"><a href="#" id="TextReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.TextImage %>> </div></a></div>
        
                            <div class="clear"></div>
                            <div class="span-24 column last button-padding-top">
                                <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="FrequencyReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.FrequencyImage %>> </div></a></div>
                                <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="BivariateReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.BivariateImage %>> </div></a></div>
                            <%if (Model.testsSeveralQuestionnaires.Count() == 0)
                              { %>
                                <div class="prepend-2 span-4 append-2 column block alignCenter last"><a href="#" id="Ranking"><div id=<%: ViewRes.Views.ChartReport.ReportsList.RankingImage %>> </div></a></div>
                            </div>
                            <%} %>
                            <%else
                                { %>
                           <div class="prepend-2 span-4 append-2 column block alignCenter last"></div>
                             <div class="span-24 column last button-padding-top">
                                <div class="prepend-2 span-4 append-2 column block last alignCenter"><a href="#" id="ComparativeReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.ComparativeImage %>> </div></a></div>
                                <div class="prepend-2 span-4 append-2 column block alignCenter"><a href="#" id="SatisfactionReports"><div id=<%: ViewRes.Views.ChartReport.ReportsList.SatisfactionImage %>> </div></a></div>
                            </div>
                            <%} 
                  
                  }%>
                        </div>
                <%--<div class="span-3 column last button-padding-top alignRight">
                    <a href="#" id="Feedbacks"><div id=<%: ViewRes.Views.ChartReport.ReportsList.FeedbacksImage %>></div></a>
                </div>--%>
            </div><%----%>
                    <%: Html.Hidden("Cancel", ViewRes.Views.Shared.Shared.CancelLabel, new { id = "Cancel" })%>
                    <%: Html.Hidden("Validate", ViewRes.Views.Shared.Shared.SelectOptionRequired, new { id = "Validate" })%> 
                    <%: Html.Hidden("ViewRes", ViewRes.Scripts.Shared.Select, new { id = "ViewRes" })%> 
            
                <div id="Dialog" title="<%= ViewRes.Views.Report.ReportsList.ChooseTest %>">
                    <p class="validateTips"><%= ViewRes.Views.Report.ReportsList.SelectTest %></p>
		            <div class="editor-field">
			            <%: Html.DropDownList("Tests", Model.tests, ViewRes.Scripts.Shared.Select, new { @class = "input-background short" , @style="width:100%;"})%>
		            </div>
                    <div id="DivCompare" style="display:none;">
                        <div class="span-24 last"><%: ViewRes.Views.ChartReport.Graphics.Compare %></div>
                        <div class="editor-field">
                            <%: Html.DropDownList("TestsToCompare", Model.testsToCompare, ViewRes.Scripts.Shared.Select, new { @class = "form-short", @style = "width:100%;" })%>
                        </div>
                    </div>
                </div>

                <div id="DialogSeveralQuestionnaires" title="<%= ViewRes.Views.Report.ReportsList.ChooseTest %>">
                    <p class="validateTips"><%= ViewRes.Views.Report.ReportsList.SelectTest %></p>
		            <div class="editor-field">
			            <%: Html.DropDownList("TestsSeveralQuestionnaires", Model.testsSeveralQuestionnaires, ViewRes.Scripts.Shared.Select, new { @class = "input-background short", @style = "width:100%;" })%>
		            </div>
                </div>

                <div id="DialogRanking" title="<%= ViewRes.Views.Report.ReportsList.ChooseQuestionnaire %>">
                    <p class="validateTips"><%= ViewRes.Views.Report.ReportsList.SelectQuestionnaire %></p>
		            <div class="editor-field">
			            <%: Html.DropDownList("Questionnaires", Model.questionnaires, ViewRes.Scripts.Shared.Select, new { @class = "input-background short", @style = "width:100%;" })%>
		            </div>
                </div>

     </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.cookie.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/reportsList.js?mm=0709") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
	<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>
