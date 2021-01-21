<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

        
            <div id="welcome-user" class="column span-21">
                <% if (Request.IsAuthenticated)
                    { %>
                    <div class="column span-24" >
                          <span id="user-icon" class=" span-1 column">&nbsp;</span>
                          <%: HttpContext.Current.User.Identity.Name%> | 
                        <% if (!Roles.IsUserInRole("General"))
                           {%>
                        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.ChangePassword, "ChangePassword", "Account", null, null)%> | 
                        <%}
                           else
                           {%>
                           &nbsp;
                           <%} %>
                        <%: Html.ActionLink(ViewRes.Views.Shared.Shared.Logout, "LogOff", "Account", null, null)%>
                        </div>
                      
                    
                    <%}
                    else {%>&nbsp;<% }%>
            </div>
            <div id="change-culture" class="column span-3 last">
                <div class="clear"></div>
                <div class="f-right">
                    <% Html.RenderPartial("Flags"); %>
                </div>
            </div>
       