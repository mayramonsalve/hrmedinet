<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
                <% if (!Request.IsAuthenticated)
                       Html.RenderPartial("MenuHome");
                   else
                   {
                       if (Roles.IsUserInRole("Administrator"))
                       {
                           Html.RenderPartial("AdminMenu");
                       }
                       else if (Roles.IsUserInRole("HRAdministrator"))
                       {
                           Html.RenderPartial("HrAdminMenu");
                       }
                       else if (Roles.IsUserInRole("CompanyAppManager"))
                       {
                           Html.RenderPartial("CompanyAppManagerMenu");
                       }
                       else if (Roles.IsUserInRole("CompanyManager") || Roles.IsUserInRole("FreeReports"))
                       {
                           Html.RenderPartial("CompanyManagerMenu");
                       }
                       else if (Roles.IsUserInRole("HRCompany"))
                       {
                           Html.RenderPartial("HrCompanyMenu");
                       }
                       else if (Roles.IsUserInRole("General"))
                       { %>
                           <div>&nbsp;</div>
                       <%}
                   }
                %>