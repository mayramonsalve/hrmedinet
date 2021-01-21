<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<ul id="nav">
    <li><a href="<%: Url.Action("Index","Home") %>"><%: ViewRes.Views.Shared.Shared.Home %></a></li>
     <li><a><span><%: ViewRes.Views.Shared.Shared.Tests%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a href="<%: Url.Action("Index","Tests") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
                <li><a href="<%: Url.Action("Create","Tests") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>
                <li><a href="<%: Url.Action("CheckCode","Tests") %>" ><%: ViewRes.Views.Shared.Shared.Load%></a></li>
    	    </ul>
        </div>
	</li>
     <li><a><span><%: ViewRes.Views.Shared.Shared.Questionnaires%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a href="<%: Url.Action("Index","Questionnaires") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
                <li><a href="<%: Url.Action("Create","Questionnaires") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>
    	    </ul>
        </div>
	</li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Categories%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a href="<%: Url.Action("Index","Categories") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
                <li><a href="<%: Url.Action("Create","Categories") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>
    	    </ul>
        </div>
	</li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Questions%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a  href="<%: Url.Action("Index","Questions") %>"  ><%: ViewRes.Views.Shared.Shared.List%></a></li>
                <li><a href="<%: Url.Action("Create","Questions") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>	    
            </ul>
        </div>
	</li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Options%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a href="<%: Url.Action("Index","Options") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
                <li><a href="<%: Url.Action("Create","Options") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>
	        </ul>
        </div>
	</li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Reports%></span></a>
        <div class="subs">	    
            <ul>
	    	    <li><a href="<%: Url.Action("ReportsList","ChartReports") %>"><%: ViewRes.Views.Shared.Shared.ListReports%></a></li>
<%--                <li><a href="<%: Url.Action("QuestionnaireReport","Reports") %>" target="_blank"><%: ViewRes.Views.Shared.Shared.QuestionnaireReport%></a></li>
                <li><a href="<%: Url.Action("EvaluationReport","Reports") %>" target="_blank"><%: ViewRes.Views.Shared.Shared.EvaluationReport%></a></li>--%>
	        </ul>
        </div>
	</li>
<%--    <li><a href="<%: Url.Action("FAQ","Home") %>"><%:ViewRes.Views.Home.FAQ.Title%></a></li>--%>
 </ul>