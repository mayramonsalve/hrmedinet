<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.FeedbackViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Feedback.Details.TitleDetails %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="contenido-sistema" class="span-24 last"> 
       <h2 class="path"><%: ViewRes.Views.Feedback.Details.PathDetails %></h2>
        <div class="linea-sistema-footer"></div>
            <%: Html.Hidden("Send", "False", new { id = "Send" })%> 
            <%: Html.Hidden("FeedbackId", Model.Feedback.Id, new { id = "FeedbackId" })%> 
            <div class="span-23 prepend-1 last"> 
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Index.DropDownTypes %></h4></div>
                    <div class="span-24 last"><%: Model.Feedback.FeedbackType.Name%></div> 

                <% if (Model.Feedback.User != null)
                   {%>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.User %></h4></div>
                    <div class="span-24 last"><%: Model.Feedback.User.FirstName%> <%: Model.Feedback.User.LastName%></div> 
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.Company %></h4></div>
                    <div class="span-24 last"><%: Model.Feedback.User.Company.Name%></div> 
                <%}%>
                <% if (Model.Feedback.AddComments != "")
                   {%>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.Suggestion %></h4></div>
                    <div class="span-24 last"><%: Model.Feedback.AddComments%></div> 
                <%}%>
                <% if (Model.Feedback.Comments != "")
                   {%>
                    <div>&nbsp;</div>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.Comment %></h4></div>
                    <div class="span-24 last"><%: Model.Feedback.Comments%></div> 
                <%}%>
                <% if(Model.ScoreCount > 0)
                   {%>
                    <div>&nbsp;</div>
                    <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.Opinion %></h4></div>
                    <div>&nbsp;</div>
                    <% foreach (KeyValuePair<int, string> feature in (IEnumerable)Model.FeaturesList)
                       {
                           SelectList aux = Model.GetSelectListByFeature(Model.OptionsList, Model.ScoreByFeature[feature.Key]);
                       %>
                        <div class="rating span-24 last">
                            <div class="span-24 last"><h4><%: feature.Value %>: </h4></div>
                            <div>
                                <form id="<%: feature.Key%>Form" method="post" action="#">
                                     <div id="<%: feature.Key%>"><%: Html.DropDownList(feature.Key.ToString() + "Select", aux, null, new { @class = "input-background short" })%></div>
                                </form>
                            </div>
                        </div>
                        <div>&nbsp;</div>
                     <%}%>
                 <%}%>
                 <div>&nbsp;</div>
                 <div class="span-24 last"><h4><%: ViewRes.Views.Feedback.Details.Show %></h4></div>
                 <div class="span-24 last"><%: Model.Feedback.Show.ToString()%></div> 
<%--                 <div class="span-24 last">
                     <% string s; // Darle estilo para q parezca un link
                        if (!Model.Feedback.Show)
                        {
                            s = "Mostrar en Index";%>
                      <%}
                        else
                        {
                            s = "No mostrar más en Index";
                        }%>
                        <span id="Show" onclick="Show();"><%: s %></span> 
                  </div>--%>
            </div>
    </div>
    <div class="span-24 last">
        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.BackToList, "Index") %>
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

