<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FeedbackViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Feedback.Send.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
        <h2 class="path"><%: ViewRes.Views.Feedback.Send.Path %></h2>
        <div class="linea-sistema-footer"></div>
            <% using (Html.BeginForm()) { 
                   %>
            <div class="span-23 prepend-1 last"> 
                <%: Html.Hidden("Send", "True", new { id = "Send" })%> 
                <%: Html.Hidden("Add", "False", new { id = "Add" })%>  
                <%: Html.Hidden("ShowDialog", Model.showDialog, new { id = "ShowDialog" })%> 
                <div class="span-24 last">
                    <div class="span-11 append-1 column">
                        <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Send.Suggestion %></h4></div>
                        <div class="span-24 last"><%: Html.TextAreaFor(model => model.Feedback.AddComments, new { @class = "input-background textArea" })%></div>
                    </div>
                    <div class="span-11 prepend-1 column last">
                        <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Send.Comment %></h4></div>
                        <div class="span-24 last"><%: Html.TextAreaFor(model => model.Feedback.Comments, new { @class = "input-background textArea" })%></div>
                    </div>
                </div>
<%--                <% if (Model.FeaturesList.Count > 0)
                   { %>
                    <div class="clear"><br /></div>
                    <div class="span-24 last"><h3><%: ViewRes.Views.Feedback.Send.Opinion %></h3></div>
                    <% foreach (KeyValuePair<int, string> feature in (IEnumerable)Model.FeaturesList)
                       {
                         %>
                         <%: Html.Hidden(feature.Key.ToString(), "", new { id = feature.Key.ToString() })%>                
                         <div class="span-24 last">
				            <div class="rating">
                                <div><h4><%: feature.Value%>:   </h4>
                                <span id="<%: feature.Key%>Span" class="rating-cap"></span></div>
                                <div id="<%: feature.Key%>Div"><%: Html.DropDownList(feature.Key.ToString(), Model.OptionsList, null, new { @id = feature.Key.ToString(), @class = "input-background short" })%></div>
				            </div>
                            <div>&nbsp;</div>
                        </div>
                    <%}%>
                <%} %>--%>
                <div class="clear"><br /></div>
			    <div class="button-padding-top">
				    <input type="submit" class="button" value="<%: ViewRes.Views.Account.LogOn.LogOnButton %>" id="Send" />
			    </div>
            </div>
            <% } %>  
    </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.GoHome, "Index", "Home") %>
    </div>
    <%: Html.Hidden("GoHome", ViewRes.Views.Shared.Shared.GoHome, new { id = "GoHome" })%> 
        <div id="Dialog" title="<%: ViewRes.Views.Feedback.Send.Thanks %>">
		    <div class="span-22 prepend-1 append-1">
            <br />
			    <%: ViewRes.Views.Feedback.Send.ThanksText %>
		    </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
    <link href="../../Content/Css/jquery.ui.stars.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
    <script src="../../Scripts/jquery.ui.stars.js" type="text/javascript"></script>
    <script src="../../Scripts/Feedbacks.js" type="text/javascript"></script>
</asp:Content>
