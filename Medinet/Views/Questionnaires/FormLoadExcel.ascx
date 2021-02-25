<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.LoadExcelViewModel>" %>

        <div class="span-24 last"> 
            <div class="button-padding-top">
                <%--<div class="span-24 last"><h4><%: Html.LabelFor(model => model.postedFile) %></h4></div>--%>
                <div class="span-24 last"><%: Html.TextBoxFor(model => model.postedFile, new {  type="file",@class = "input-file input-background large" })%></div> 
                <div><%: Html.ValidationMessageFor(model => model.postedFile)%></div>
                <%--<input type="file" name="postedFile" />--%>     
            </div>
        </div>
