<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.FunctionalOrganizationViewModel>" %>

    <div class="span-24 column last"> 
    <fieldset class="span-9 column">
        <legend><%: ViewRes.Models.FOrganization.Parent %></legend>
        <div class="span-24 last"><h4><%: ViewRes.Models.FOrganization.FOTParent %></h4></div>
        <div class="span-24 last"> <%: Model.functionalOrganization.Parent.FunctionalOrganizationType.Name %></div>
        <div class="span-24 last"><h4><%: ViewRes.Models.FOrganization.FOParent %></h4></div>
        <div class="span-24 last"> <%: Model.functionalOrganization.Parent.Name %></div>
    </fieldset>
    <div class="span-2 column">&nbsp;</div>
        <fieldset class="span-9 column">
        <legend><%: ViewRes.Models.FOrganization.Entity %></legend>
            <div class="span-24 column last"><h4><%: ViewRes.Models.FOrganization.FOType %></h4></div>
            <div class="span-24 last"><%: Model.functionalOrganization.FunctionalOrganizationType.Name.ToString() %></div>
            <div class="span-24 column last"><h4><%: ViewRes.Models.Shared.Name%></h4></div>
            <div class="span-24 last"><%: Model.functionalOrganization.Name.ToString() %></div> 
            <div class="span-24 column last"><h4><%: ViewRes.Models.Shared.ShortName%></h4></div>
            <div class="span-24 last"><%: Model.functionalOrganization.ShortName.ToString()%></div> 
        </fieldset>
    <div class="span-4 column last">&nbsp;</div>
    </div>  