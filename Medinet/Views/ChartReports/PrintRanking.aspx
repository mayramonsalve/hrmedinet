<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.RankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.ChartReport.Graphics.Ranking %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="contenido-sistema">
    <h2 class="alignCenter"><%:ViewRes.Views.ChartReport.Ranking.RankingTitle %></h2>
    <div id="title" class="span-24 last"><h3><%: Model.title%></h3></div>
    <div id="options" class="span-24 last">
        <h4><%:ViewRes.Views.ChartReport.Graphics.Questionnaire%>: </h4><%:Model.questionnaire%><br/>
        <% if (Model.sector != "")
           {%>
            <h4><%:ViewRes.Views.ChartReport.Graphics.Sector%>: </h4><%:Model.sector%><br/>
        <% }%>
        <% if (Model.country != "")
           {%>
            <h4><%:ViewRes.Views.ChartReport.Graphics.Country%>: </h4><%:Model.country%><br/>
        <% }%>
    </div>
    <% foreach(KeyValuePair<string, object> objectPair in Model.rankingPrint)
       {
           int pos = 0;
           Dictionary<string, double> table = (Dictionary<string, double>)objectPair.Value;
           if (Model.rankingPrint.Count > 1)
           {
           %>               
               <div id="subTitle" class="span-24 column last"><br /><h4><%: objectPair.Key %></h4></div>
         <%} %>
         <div class="span-22 prepend-1 append-1">
         <br />
         <fieldset>
            <table class="display">
                <thead>
                    <tr> 
                        <th align="center"><%: Model.nameTH %></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Ranking%></th>
                        <th align="center"><%: ViewRes.Views.ChartReport.Graphics.Climate%></th>
                    </tr>
                </thead>  
                <tbody>
                    <%foreach (string key in table.Keys)
                      { 
                        pos++;%>
                        <tr>
                            <td class="alignLeft"><%:key%></td>
                            <td class="alignCenter"><%:pos%></td>
                            <td class="alignCenter"><%:String.Format("{0:0.##}", table[key])%>%</td>
                        </tr>
                    <%} %>
                </tbody>
            </table>
         </fieldset>
         </div>
    <% }%>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
        <div id="header" class="block">  
            <div id="logo" class="span-4 column" ></div>
            <div class="span-4 prepend-18 append-2"><% if (Model.UserLogged.Company.Image != null && Model.UserLogged.Company.Image != "System.Web.HttpPostedFileWrapper")
                                            { %>
							            <img src="../../Content/Images/Companies/<%: Model.UserLogged.Company.Image %>"
                                            alt="" width="100" />
                                    <% } %>
                                    <% else{
                                            %>
                                        <img src="../../Content/Images/Companies/<%: Model.UserLogged.Company.CompaniesType.Name %>Image.png"
                                            alt="" width="100" />
                                    <% } %></div>
        </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/demo_page.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table_jui.css" rel="stylesheet" type="text/css" media="screen, projection" />
    <link href="../../Content/Css/demo_table.css" rel="stylesheet" type="text/css" media="screen, projection" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server"> 
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tools.min.js" type="text/javascript"></script>
</asp:Content>
