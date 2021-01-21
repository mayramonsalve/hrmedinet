using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class UserOwner : AuthorizeAttribute
{
    // Add properties here.

    public UserOwner()
    {   }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var isAuthorized = base.AuthorizeCore(httpContext);
        if (isAuthorized)
        {
            var request = httpContext.Request;
            var objectId = request.RequestContext.RouteData.Values["objectId"]
                ?? request["objectId"];
            var currentUser = httpContext.User.Identity.Name;
            return HasPermissions(currentUser, objectId);
        }
        return isAuthorized;
    }

    private bool HasPermissions(string currentUser, object objectId)
    {
        return true;
    }

}
