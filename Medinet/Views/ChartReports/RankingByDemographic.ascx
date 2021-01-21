<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>

<% string demographic = ViewData["option"].ToString();
   string FO_id = ViewData["FO_id"] != null ? ViewData["FO_id"].ToString() : ""; %>
    <div class="clear"><br /></div>  
    <div class="prepend-4 span-16 apend-4">
            <br />
            <div>
            <table id="Ranking<%:demographic%><%:FO_id %>" class="display tabla">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.Name%></th>
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
                            <td class="alignLeft"><%:key%></td>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", Model.ranking[key])%>%</td>
                        </tr>
                     <%}
                   } %>
                </tbody>
            </table>
            </div>
            <div id="NoCompanies<%:demographic%><%:FO_id %>" style="display:none"><p><h5><%: ViewRes.Views.ChartReport.Ranking.NoDemographics %></h5></p></div>
            <br />
    </div>
<%--    <div class="prepend-19 span-5">
        <div class="clear"><br /></div>
        <%: Html.ActionLink(ViewRes.Views.ChartReport.Ranking.PrintThisRanking, "PdfRanking", new
        {
            questionnaire_id = Model.questionnaireId,
            sector_id = 0,
            country_id = 0,
            company_id = Model.companyId,
            demographic = demographic,
            fot = Model.foId
        }, new { id = "PrintLink" + demographic + FO_id, name = "PrintLink" + demographic + FO_id, @class = "Print" })%>
    </div>--%>
<%--        <div class="prepend-22 span-2 last alignCenter"> 
            <fieldset id=<%:demographic %><%:FO_id %>>
                <legend><%: ViewRes.Views.Shared.Shared.Print %></legend>
                <div class="span-24 column block alignCenter last"><a id="PrintPdf<%:demographic %><%:FO_id %>" class="PrintPdf"><div id="PrintPdfImage" title=<%: ViewRes.Views.Shared.Shared.PrintPdf %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintWord"<%:demographic %> class="Print"><div id="PrintWordImage" title=<%: ViewRes.Views.Shared.Shared.PrintWord %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintExcel"<%:demographic %> class="Print"><div id="PrintExcelImage" title=<%: ViewRes.Views.Shared.Shared.PrintExcel %>> </div></a></div>
                <div class="span-6 column block alignCenter last"><a href="#" id="PrintPpt"<%:demographic %> class="Print"><div id="PrintPptImage" title=<%: ViewRes.Views.Shared.Shared.PrintPpt %>> </div></a></div>
            </fieldset>
        </div>--%>