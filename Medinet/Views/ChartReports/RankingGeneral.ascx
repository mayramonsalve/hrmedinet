<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
        <div class="prepend-1 span-22 append-1 column last">
                <div class="span-24 column last"><h4> <%: ViewRes.Views.ChartReport.Graphics.Sector %> </h4></div>
                <div class="editor-field">
                    <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                        { %>
			            <%: Html.DropDownList("SectorsGeneral", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsGeneral", @class = "Sectors input-background short" })%>
                    <%}
                        else
                        {%>
                        <%:Model.UserLogged.Company.CompanySector.Name%>
                    <%} %>
		        </div>
        </div>
        <div class="clear"><br /></div>       
        <div class="prepend-4 span-16 apend-4">
            <br />
            <div>
            <div id="DivSelectGeneral" class="alignCenter">
                <% string style = "";
                    if (Model.UserLogged.Role.Name == "HRAdministrator")
                    { %>
                    <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSector %></h5>
                    <% style = "display:none;";%>
                    <%} %>
            </div>
            <table id="RankingGeneral" class="display tabla"">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.CompanyTab%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                 </thead>  
                <tbody>
                <% if (Model.ranking != null)
                   {
                       int pos = 0;
                       foreach (string key in Model.ranking.Keys)
                       {
                           pos++;%>
                        <tr>
                            <% if (key == Model.UserLogged.Company.Name)
                               {%>
                                    <td class="alignLeft"><%:key%></td>
                             <%}
                               else
                               { %>
                                    <td class="alignLeft">&nbsp;</td>
                             <%} %>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", Model.ranking[key])%>%</td>
                        </tr>
                     <%}
                   } %>
                </tbody>
            </table>
            <div id="NoCompaniesGeneral" style="display:none" class="alignCenter"><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5></div>
            </div>            
            <br />
        </div>
        <div class="clear">&nbsp;</div>
<%--        <div class="prepend-19 span-5 column last">
            <%: Html.ActionLink(ViewRes.Views.ChartReport.Ranking.PrintThisRanking, "PdfRanking", new
            {
                questionnaire_id = Model.questionnaireId,
                sector_id = 0,
                country_id = 0,
                company_id = 0,
                demographic = "General",
                fot = 0
            }, new { id = "PrintLinkGeneral", name = "PrintLinkGeneral", @class = "Print" })%>
        </div>--%>
<%--        <div class="prepend-22 span-2 column last alignCenter"> 
            <fieldset id="FieldsetGeneral" style=<%: style %>>
                <legend><%: ViewRes.Views.Shared.Shared.Print %></legend>
                <div class="span-24 column block alignCenter last"><a id="PrintPdfGeneral" class="PrintPdf"><div id="PrintPdfImage" title=<%: ViewRes.Views.Shared.Shared.PrintPdf %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintWordGeneral" class="Print"><div id="PrintWordImage" title=<%: ViewRes.Views.Shared.Shared.PrintWord %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintExcelGeneral" class="Print"><div id="PrintExcelImage" title=<%: ViewRes.Views.Shared.Shared.PrintExcel %>> </div></a></div>
                <div class="span-6 column block alignCenter last"><a href="#" id="PrintptGeneral" class="Print"><div id="PrintPptImage" title=<%: ViewRes.Views.Shared.Shared.PrintPpt %>> </div></a></div>
            </fieldset>
        </div>--%>