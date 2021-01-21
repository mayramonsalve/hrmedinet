<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
    
        <div class="prepend-1 span-22 append-1 column last">
            <div class="span-12 column alignLeft">
                <div class="span-24 column last"><h4> <%: ViewRes.Views.ChartReport.Graphics.Sector %> </h4></div>
                <div class="editor-field">
                    <%if (Model.UserLogged.Role.Name == "HRAdministrator")
                        { %>
			            <%: Html.DropDownList("SectorsGeneralCountry", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsGeneralCountry", @class = "Sectors input-background short" })%>
                    <%}
                        else
                        {%>
                        <%:Model.UserLogged.Company.CompanySector.Name%>
                    <%} %>
		        </div>
            </div>
            <div class="span-12 column last alignRight">
                <div class="span-24 column last"><h4> <%: ViewRes.Views.ChartReport.Graphics.Country %> </h4></div>
                <div class="editor-field">
			        <%: Html.DropDownList("CountriesGeneralCountry", Model.countriesList, ViewRes.Scripts.Shared.Select, new { @id = "CountriesGeneralCountry", @class = "Countries input-background short" })%>
		        </div>
            </div>
        </div>
        <div class="clear"><br /></div>  
        <div class="prepend-4 span-16 apend-4">
            <br />
            <div>
            <div id="DivSelectGeneralCountry" class="alignCenter">
                <%if (Model.UserLogged.Role.Name == "HRAdministrator")
                    { %>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSectorAndCountry%></h5>
                    <%}
                    else
                    {%>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectCountry%></h5>
                <%} %>
            </div>
            
            <table id="RankingGeneralCountry" class="display tabla" style = "display:none;">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.CompanyTab%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                 </thead>  
                <tbody>
                </tbody>
            </table>
            
            <div id="NoCompaniesGeneralCountry" style="display:none" class="alignCenter"><p><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5></p></div>
            </div>
            <br />
        </div>
        <div class="clear">&nbsp;</div>
<%--        <div class="prepend-18 span-6 column last">
            <%: Html.ActionLink(ViewRes.Views.ChartReport.Ranking.PrintThisRanking, "PdfRanking", new
            {
                questionnaire_id = Model.questionnaireId,
                sector_id = 0,
                country_id = 0,
                company_id = 0,
                demographic = "GeneralCountry",
                fot = 0
            }, new { id = "PrintLinkGeneralCountry", name = "PrintLinkGeneralCountry", @class = "Print" })%>
        </div>--%>
<%--        <div class="prepend-22 span-2 column last alignCenter"> 
            <fieldset id="FieldsetGeneralCountry" style="display:none">
                <legend><%: ViewRes.Views.Shared.Shared.Print %></legend>
                <div class="span-24 column block alignCenter last"><a id="PrintPdfGeneralCountry" class="PrintPdf"><div id="PrintPdfImage" title=<%: ViewRes.Views.Shared.Shared.PrintPdf %>> </div></a></div>
<%--                <div class="span-6 column block alignCenter"><a href="#" id="PrintWordGeneralCountry" class="Print"><div id="PrintWordImage" title=<%: ViewRes.Views.Shared.Shared.PrintWord %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintExcelGeneralCountry" class="Print"><div id="PrintExcelImage" title=<%: ViewRes.Views.Shared.Shared.PrintExcel %>> </div></a></div>
                <div class="span-6 column block alignCenter last"><a href="#" id="PrintPptGeneralCountry" class="Print"><div id="PrintPptImage" title=<%: ViewRes.Views.Shared.Shared.PrintPpt %>> </div></a></div>
            </fieldset>
        </div>--%>