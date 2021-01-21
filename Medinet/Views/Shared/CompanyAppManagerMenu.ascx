<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>    

<ul id="nav">
	<li><a href="<%: Url.Action("Index","Home") %>"><%:ViewRes.Views.Shared.Shared.Home %></a></li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.Users%></span></a>
        <div class="subs">
    	    <ul>
	    	    <li><a href="<%: Url.Action("Index","Users") %>"><%:ViewRes.Views.Shared.Shared.List%></a></li>
		        <li><a href="<%: Url.Action("Create","Users") %>"><%: ViewRes.Views.Shared.Shared.Create%></a></li>
	        </ul>
        </div>
	</li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.Ubications%></span></a>
        <div class="subs">
    	    <ul>
        	    <li><a><span><%: ViewRes.Views.Shared.Shared.Regions%></span></a>
                    <div class="subs">
	    	            <ul>
		    	            <li><a href="<%: Url.Action("Index","Regions") %>"><%: ViewRes.Views.Shared.Shared.List%></a></li>
        	    	        <li><a href="<%: Url.Action("Create","Regions") %>"><%: ViewRes.Views.Shared.Shared.Create%></a></li>
    		            </ul>
                    </div>
                </li>
		        <li ><a><span><%: ViewRes.Views.Shared.Shared.Locations%></span></a>
                    <div class="subs">
    	    	        <ul>
        	    		    <li><a href="<%: Url.Action("Index","Locations") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
	        	            <li><a href="<%: Url.Action("Create","Locations") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
    	    	        </ul>
                    </div>
                </li>
            </ul>
        </div>
    </li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.FOrganizations%></span></a>
        <div class="subs">
        	<ul>
                <li><a><span><%: ViewRes.Views.Shared.Shared.FOTypes%></span></a>
                    <div class="subs">
                        <ul>
        			        <li><a href="<%: Url.Action("Index","FunctionalOrganizationTypes") %>" ><%:ViewRes.Views.Shared.Shared.List%></a></li>
                		    <li><a href="<%: Url.Action("Create","FunctionalOrganizationTypes") %>" ><%: ViewRes.Views.Shared.Shared.Create%></a></li>
                		</ul>
                    </div>
    		    </li>
        		<li><a><span><%: ViewRes.Views.Shared.Shared.Entities%></span></a>
                    <div class="subs">
                        <ul>
                            <li><a href="<%: Url.Action("Index","FunctionalOrganizations") %>" ><%:ViewRes.Views.Shared.Shared.List%></a></li>
        	    	        <li><a href="<%: Url.Action("Create","FunctionalOrganizations") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        		        </ul>
                    </div>
                </li>
    	    </ul>
        </div>
    </li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.Levels%></span></a>
        <div class="subs">
    	    <ul>
        	    <li><a><span><%: ViewRes.Views.Shared.Shared.InstructionLevels%></span></a>
                    <div class="subs">
	    	            <ul>
		    	            <li><a href="<%: Url.Action("Index","InstructionLevels") %>"><%: ViewRes.Views.Shared.Shared.List%></a></li>
        	    	        <li><a href="<%: Url.Action("Create","InstructionLevels") %>"><%: ViewRes.Views.Shared.Shared.Create%></a></li>
    		            </ul>
                    </div>
                </li>
		        <li ><a><span><%: ViewRes.Views.Shared.Shared.PositionLevels%></span></a>
                    <div class="subs">
    	    	        <ul>
        	    		    <li><a href="<%: Url.Action("Index","PositionLevels") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
	        	            <li><a href="<%: Url.Action("Create","PositionLevels") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
    	    	        </ul>
                    </div>
                </li>
            </ul>
        </div>
    </li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.Ranges%></span></a>
        <div class="subs">
    	    <ul>
                 <li><a><span><%: ViewRes.Views.Shared.Shared.Ages%></span></a>
                    <div class="subs">
        	            <ul>
                            <li><a href="<%: Url.Action("Index","Ages") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
    	    	            <li><a href="<%: Url.Action("Create","Ages") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        	            </ul>
                    </div>
	            </li>
                <li><a><span><%: ViewRes.Views.Shared.Shared.Seniorities%></span></a>
                    <div class="subs">
        	            <ul>
                            <li><a href="<%: Url.Action("Index","Seniorities") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
    	    	            <li><a href="<%: Url.Action("Create","Seniorities") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        	            </ul>
	                </div>
                </li>
            </ul>
        </div>
    </li>
	<li><a><span><%: ViewRes.Views.Shared.Shared.Scales%></span></a>
        <div class="subs">
    	    <ul>
                 <li><a><span><%: ViewRes.Views.Shared.Shared.ClimateScales%></span></a>
                    <div class="subs">
        	            <ul>
                            <li><a href="<%: Url.Action("Index","ClimateScales") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
    	    	            <li><a href="<%: Url.Action("Create","ClimateScales") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        	            </ul>
                    </div>
	            </li>
                <li><a><span><%: ViewRes.Views.Shared.Shared.ClimateRanges%></span></a>
                    <div class="subs">
        	            <ul>
                            <li><a href="<%: Url.Action("Index","ClimateRanges") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
    	    	            <li><a href="<%: Url.Action("Create","ClimateRanges") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        	            </ul>
	                </div>
                </li>
            </ul>
        </div>
    </li>
    <li><a><span><%: ViewRes.Views.Shared.Shared.Performances%></span></a>
        <div class="subs">
        	<ul>
                <li><a  href="<%: Url.Action("Index","PerformanceEvaluations") %>" ><%: ViewRes.Views.Shared.Shared.List%></a></li>
		        <li><a href="<%: Url.Action("Create","PerformanceEvaluations") %>" ><%:ViewRes.Views.Shared.Shared.Create%></a></li>
        	</ul>
        </div>
	</li>
<%--    <li><a href="<%: Url.Action("FAQ","Home") %>"><%:ViewRes.Views.Home.FAQ.Title%></a></li>--%>
</ul>

