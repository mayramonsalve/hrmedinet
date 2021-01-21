<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FeedbackViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Feedback.Show.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="contenido-sistema" class="column span-24 last"> 
       <h2 class="path"><%: ViewRes.Views.Feedback.Show.Path %></h2>
        <div class="linea-sistema-footer"></div>
                    <% string text;
                       if (Model.TypeName != "")
                           text = ViewRes.Views.Feedback.Show.TextAverage1 + Model.TypeName + ViewRes.Views.Feedback.Show.TextAverage2;
                       else
                           text = ViewRes.Views.Feedback.Show.SelectType;
                        %> 
            <%: Html.Hidden("Send", "False", new { id = "Send" })%> 
            <%: Html.Hidden("Split", "4", new { id = "Split" })%> 
            <%: Html.Hidden("Text", text, new { id = "Text" })%> 
            <%: Html.Hidden("NoComments", ViewRes.Views.Feedback.Show.NoComments, new { id = "NoComments" })%> 
            <%: Html.Hidden("NoFeedbacks", ViewRes.Views.Feedback.Show.NoFeedbacks, new { id = "NoFeedbacks" })%> 
            <%: Html.Hidden("SelectType", ViewRes.Views.Feedback.Show.SelectType, new { id = "SelectType" })%> 
            <%: Html.Hidden("TextAverage1", ViewRes.Views.Feedback.Show.TextAverage1, new { id = "TextAverage1" })%> 
            <%: Html.Hidden("TextAverage2", ViewRes.Views.Feedback.Show.TextAverage2, new { id = "TextAverage2" })%> 
            <%: Html.Hidden("OutOf", ViewRes.Views.Feedback.Show.OutOf, new { id = "OutOf" })%> 
            <%: Html.Hidden("Votes", ViewRes.Views.Feedback.Show.Votes, new { id = "Votes" })%> 
            <div class="span-23 column prepend-1 last"> 
                <div class="span-24 last">
                    <h4> <%: ViewRes.Views.Feature.Index.DropDownTypes%></h4>
	                <%: Html.DropDownList("Types", Model.TypesList, ViewRes.Scripts.Shared.Select, new { id = "Types", @class = "form-short" })%>
	            </div>
                <div>&nbsp;</div>
                <div class="append-1 span-9 column colborder">
                    <div class="span-24 last column"><h4 id="textFeedbacks"><%:text%></h4></div>
                    <div>&nbsp;</div>
                    <div id="StarsFeedbacks">
                     <% if(Model.TypeName != "") // recargar esta parte con javascript
                        {
                         foreach (KeyValuePair<int, string> feature in (IEnumerable)Model.FeaturesList)
                            {
                                SelectList aux = Model.GetAverageSelectListByFeature(Model.OptionsList, Model.ScoreAverageByFeature[feature.Key]);
                            %>
                            <div class="rating span-23 append-1 column">
                                <div class="span-24 last column"><h4><%: feature.Value %>: <span id="<%: feature.Key%>Span" class="rating-cap"><%: Model.ScoreAverageByFeature[feature.Key].Average() %>/5 <%: ViewRes.Views.Feedback.Show.OutOf %> <%: Model.ScoreAverageByFeature[feature.Key].Count %> <%: ViewRes.Views.Feedback.Show.Votes %></span></h4></div>
                                <div class="span-24 last column">
                                    <form id="<%: feature.Key%>Form" method="post" action="#">
                                            <div id="<%: feature.Key%>"><%: Html.DropDownList(feature.Key.ToString() + "Select", aux, null, new { @class = "input-background short" })%></div>
                                    </form>
                                </div>
                                <br/>
                            </div> 
                            <%}                             
                        }%>
                    </div>
                </div>
                <% string heigth = "";/* Model.GetStringHeigthForAccordion();*/ %>
                <div class="append-1 span-13 column last">
                    <div id="myaccordion" <%: heigth %>>
                        <h3><a href="#"><%: ViewRes.Views.Feedback.Show.ToAdd%></a></h3>
                            <div id="AddComments">
                            <%if (Model.AddCommentStrings.Count > 0)
                              { %>
                                    <ul class="listStyle">
                                    <% foreach (string feedback in Model.AddCommentStrings)
                                       { %>
                                        <li><p><%: feedback%></p></li>
                                    <%} %>
                                    </ul>
                            <%}
                              else
                              { // actualizar con javascript
                                   string addCommentText;
                                   if (Model.TypeName == "")
                                       addCommentText = ViewRes.Views.Feedback.Show.SelectType;
                                   else
                                       addCommentText = ViewRes.Views.Feedback.Show.NoComments;
                                  %>
                                    <%: addCommentText %>
                            <%} %>
                            </div>
                        <h3><a href="#"><%: ViewRes.Views.Feedback.Show.Comments %></a></h3>
                            <div id="Comments">
                            <%if (Model.CommentStrings.Count > 0)
                              { %>
                                    <ul class="listStyle">
                                    <% foreach (string feedback in Model.CommentStrings)
                                       { %>
                                        <li><p><%: feedback%></p></li>
                                    <%} %>
                                    </ul>
                            <%}
                              else
                              { // actualizar con javascript                              
                                  string commentText;
                                   if (Model.TypeName == "")
                                       commentText = ViewRes.Views.Feedback.Show.SelectType;
                                   else
                                       commentText = ViewRes.Views.Feedback.Show.NoComments;
                                  %>
                                    <%: commentText %>
                            <%} %>
                            </div>
                    </div>
                </div>
            </div>
    </div>
        <div class="span-24 last">
            <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index", null, new { @type_id = 0 }, new { id = "editlink" })%>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="/Content/Css/jquery.ui.stars.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/Feedbacks.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.stars.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myaccordion").accordion({
                collapsible: true
            });
        });
</script>
</asp:Content>
