<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="nav">
    <li><a href="<%: Url.Action("Index","Home") %>"><%: ViewRes.Views.Shared.Shared.Home %></a> </li>
    <li><a><span> <%: ViewRes.Views.Shared.Shared.Users%></span></a>
        <div class="subs">
            <ul>
                <li><a href="<%: Url.Action("Index","Users")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                <li><a href="<%: Url.Action("Create","Users")%>"><%: ViewRes.Views.Shared.Shared.Create%></a> </li>
            </ul>
        </div>
    </li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Companies%></span></a>
        <div class="subs">
            <ul>
                <li><a href="<%: Url.Action("Index","Companies")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                <li><a href="<%: Url.Action("Create","Companies")%>"><%: ViewRes.Views.Shared.Shared.Create%></a> </li>
            </ul>
        </div>
    </li>
    <% if (HttpContext.Current.User.Identity.Name.ToLower() == "administrator")
       { %>
        <%--<li><a><span><%: ViewRes.Views.Shared.Shared.Countries%></span></a>
            <div class="subs">
                <ul>
                    <li><a href="<%: Url.Action("Index","Countries")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                    <li><a href="<%: Url.Action("Create","Countries")%>"><%: ViewRes.Views.Shared.Shared.Create%></a> </li>
                </ul>
            </div>
        </li>
        <li><a><span><%: ViewRes.Views.Shared.Shared.States%></span></a>
            <div class="subs">
                <ul>
                    <li><a href="<%: Url.Action("Index","States")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                    <li><a href="<%: Url.Action("Create","States")%>"><%: ViewRes.Views.Shared.Shared.Create%></a> </li>
                </ul>
            </div>
        </li>--%>
        <%--<li><a><span><%: ViewRes.Views.ChartReport.Graphics.FeedbackTab %></span></a>
            <div class="subs">
                <ul>
                    <li><a href="<%: Url.Action("Index","Feedbacks")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                    <li><a href="<%: Url.Action("ShowFeedbacks","Feedbacks")%>"><%: ViewRes.Views.Feedback.Show.Title %></a> </li>
                </ul>
            </div>
        </li>--%>
        <li><a href="<%: Url.Action("ShowContacts","ContactUs") %>"><%:ViewRes.Views.ContactUs.Index.TitleIndex%></a></li>
        <li><a><span>Demos</span></a>
            <div class="subs">
                <ul>
                    <li><a href="<%: Url.Action("Index","Demos")%>"><%:ViewRes.Views.Shared.Shared.List%></a> </li>
                    <li><a href="<%: Url.Action("Create","Demos")%>"><%: ViewRes.Views.Shared.Shared.Create %></a> </li>
                </ul>
            </div>
        </li>
     <%} %>
<%--    <li><a><span><%: ViewRes.Views.Shared.Shared.Reports %></span></a>
        <div class="subs">
            <ul>
                <li><a href="<%: Url.Action("TestReport","Reports")%>" target="_blank"><%: ViewRes.Views.Shared.Shared.TestReport%></a> </li>
            </ul>
        </div>
    </li>--%>
<%--    <li><a href="<%: Url.Action("FAQ","Home") %>"><%:ViewRes.Views.Home.FAQ.Title%></a></li>--%>
</ul>