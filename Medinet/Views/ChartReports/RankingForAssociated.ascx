<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.RankingViewModel>" %>
        <div class="prepend-1 span-22 append-1 column last">
                <div class="span-24 column last"><h4> <%: ViewRes.Views.ChartReport.Graphics.Sector %> </h4></div>
                <div class="editor-field">
                    <%if (Model.UserLogged.Role.Name.ToLower() == "hradministrator")
                        { %>
			            <%: Html.DropDownList("SectorsCustomer", Model.sectorsList, ViewRes.Scripts.Shared.Select, new { @id = "SectorsCustomer", @class = "Sectors input-background short" })%>
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
            <div id="DivSelectCustomer" class="alignCenter">
                <h5><%: ViewRes.Views.ChartReport.Ranking.SelectSector %></h5>
            </div>
            <table id="RankingCustomer" class="display tabla" style="display:none">
                 <thead>
                    <tr> 
                         <th><%: ViewRes.Views.ChartReport.Graphics.CompanyTab%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                         <th><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                 </thead>  
                <tbody>
<%--                <% if (Model.ranking != null)
                   {
                       int pos = 0;
                       foreach (string key in Model.ranking.Keys)
                       {
                           pos++;%>
                        <tr>
                            <td class="alignLeft"><%:key%></td>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", Model.ranking[key])%></td>
                        </tr>
                     <%}
                   } %>--%>
                </tbody>
            </table>
            <div id="NoCompaniesCustomer" style="display:none" class="alignCenter"><h5><%: ViewRes.Views.ChartReport.Ranking.NoCompanies %></h5></div>
            </div>
            <br />
        </div>
<%--        <div class="clear">&nbsp;</div>
        <div class="prepend-22 span-2 column last alignCenter"> 
            <fieldset id="FieldsetCustomer" style="display:none">
                <legend><%: ViewRes.Views.Shared.Shared.Print %></legend>
                <div class="span-24 column block alignCenter last"><a id="PrintPdfCustomer" class="PrintPdf"><div id="PrintPdfImage" title=<%: ViewRes.Views.Shared.Shared.PrintPdf %>> </div></a></div>
<%--                <div class="span-6 column block alignCenter"><a href="#" id="PrintWordCustomer" class="Print"><div id="PrintWordImage" title=<%: ViewRes.Views.Shared.Shared.PrintWord %>> </div></a></div>
                <div class="span-6 column block alignCenter"><a href="#" id="PrintExcelCustomer" class="Print"><div id="PrintExcelImage" title=<%: ViewRes.Views.Shared.Shared.PrintExcel %>> </div></a></div>
                <div class="span-6 column block alignCenter last"><a href="#" id="PrintPptCustomer" class="Print"><div id="PrintPptImage" title=<%: ViewRes.Views.Shared.Shared.PrintPpt %>> </div></a></div>
            </fieldset>
        </div>--%>