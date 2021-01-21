<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>

                    <% string style = "";
                        if (Model.UserLogged.Role.Name == "HRAdministrator")
                       {
                           style = "display:none;";%>
                        <div class="prepend-1 span-22 append-1 column last">
                            <div class="span-24 column last"><h4><%: ViewRes.Views.ChartReport.Graphics.Company%></h4></div>
                            <div class="editor-field">
			                    <%: Html.DropDownList("CompaniesInternal", Model.companiesList, ViewRes.Scripts.Shared.Select, new { @id = "CompaniesGeneral", @class = "Companies input-background short" })%>
		                    </div>
                        </div>
                        <div class="clear"><br /></div> 
                     <%} %>
                    <div class="prepend-1 span-22 append-1 column last">
                        <div id="DemographicsDiv">
                            <% if (Model.UserLogged.Role.Name != "HRAdministrator")
                               { %>
                                <%Html.RenderPartial("RankingTabs", Model); %>
                             <%}%>
                        </div>
                        <% if (Model.UserLogged.Role.Name == "HRAdministrator")
                               { %> 
                                    <div class="prepend-4 span-16 apend-4">
                                        <div id="DivSelectInternal" class="alignCenter">
                                            
                                            <h5><%: ViewRes.Views.ChartReport.Ranking.SelectCompany%></h5>
                                            
                                        </div>
                                    </div>
                             <%} %>
                    </div>
                    <div class="clear"><br /></div>
                    <%--<div class="append-18 span-6 column last">
                        <%: Html.ActionLink(ViewRes.Views.ChartReport.Ranking.PrintInternalRanking, "PdfRanking", new
                        {
                            questionnaire_id = Model.questionnaireId,
                            sector_id = 0,
                            country_id = 0,
                            company_id = 0,
                            demographic = "Internal",
                            fot = 0
                        }, new { id = "PrintLinkInternal", name = "PrintLinkInternal", @class = "Print" })%>
                    </div>--%>
<%--
                    <div class="column last alignCenter"> 
                        <fieldset id="FieldsetInternal" style=<%: style %> class="alignCenter">
                            <legend><%: ViewRes.Views.Shared.Shared.PrintAll %></legend>
                            <div class="span-20 prepend-4 column block alignCenter last"><a id="PrintPdfInternal" class="PrintPdf"><div id="PrintPdfImage" title=<%: ViewRes.Views.Shared.Shared.PrintPdf %>> </div></a></div>
                            <%--<div class="span-6 column block alignCenter"><a href="#" id="PrintWordInternal" class="Print"><div id="PrintWordImage" title=<%: ViewRes.Views.Shared.Shared.PrintWord %>> </div></a></div>
                            <div class="span-6 column block alignCenter"><a href="#" id="PrintExcelInternal" class="Print"><div id="PrintExcelImage" title=<%: ViewRes.Views.Shared.Shared.PrintExcel %>> </div></a></div>
                            <div class="span-6 column block alignCenter last"><a href="#" id="PrintPptInternal" class="Print"><div id="PrintPptImage" title=<%: ViewRes.Views.Shared.Shared.PrintPpt %>> </div></a></div>
                        </fieldset>
                    </div>--%>