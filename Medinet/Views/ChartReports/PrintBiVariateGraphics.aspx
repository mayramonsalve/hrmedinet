<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%:ViewRes.Views.ChartReport.Graphics.BivariateTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <div id="contenido-sistema" class="span-24 last column">
        <h2 class="path"><%: ViewRes.Views.ChartReport.Graphics.BivariateTitle%></h2>
        <div class="linea-sistema-footer"></div>
         <div class="editor-label">
            <h4><%:ViewRes.Views.ChartReport.Graphics.Demographics %>: 
		    <%: Model.demographicName1 %></h4>
	    </div>
        <div class="editor-label">
           <h4><%:ViewRes.Views.ChartReport.Graphics.Demographics %>: 
	        <%: Model.demographicName2 %></h4>
	    </div>
        <br/> 
        <div class="span-23 append-1 last">
            <% if (Model.isTable)
               { %>
                    <fieldset id="FieldsetTable" class="alignCenter">
                        <div class="alignCenter"><h4><%: Html.Label(Model.BivariateTitle) %></h4></div>
                        <table id="Table" class="display span-24 column last" >
                            <tbody>
                                <tr>
                                    <th rowspan='2' colspan='2' padding='10px'></th>
                                    <th padding='10px' colspan='<%: Model.demographicCount2 %>' style='border-style:solid; border-width:2px'><%: Model.demographicName2 %></th>
                                </tr>
                                <tr>
                                     <% foreach (string name2 in ((Dictionary<string, double>)Model.stringObject.FirstOrDefault().Value).Keys)
                                        { %>
                                            <th padding='10px' style='border-style:solid; border-width:1px; text-align:right'><%: name2 %></th>
                                      <%} %>
                                </tr>
                                <tr>
                                    <% foreach (KeyValuePair<string, object> pair in Model.stringObject)
                                       {
                                           if (pair.Key == Model.stringObject.First().Key)
                                           {%>
                                                <th padding='10px' rowspan='<%: Model.demographicCount1 %>' style='border-style:solid; border-width:2px; vertical-align:middle;'><%: Model.demographicName1%></th>
                                         <%} %>
                                        <th padding='10px' style='border-style:solid; border-width:1px'><%: pair.Key %></th>
                                        <%foreach (KeyValuePair<string, double> stringDouble in (Dictionary<string, double>)pair.Value)
                                          {
                                                string color;
                                                double pct = stringDouble.Value * 100 / Model.optionsCount;
                                                if (pct > 80) color = "green";
                                                else if (pct > 60) color = "#FF9900";
                                                else if (pct > 0) color = "red";
                                                else color = "black";
                                           %>
                                                <td padding='10px' style='text-align:right; border-style:solid; border-width:1px;'><span style='color:"<%: color %>";'><%: stringDouble.Value %></span></td>
                                        <%} %>
                                     <%} %>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
            <%}
               else
               {%>
                <div id="DivChart">
                    <img id="Chart" src="<%: Model.BivariateTitle %>" alt=" " />
                </div>
            <%} %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/BivariateCharts.js" type="text/javascript"></script>
</asp:Content>
