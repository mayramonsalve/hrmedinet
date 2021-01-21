<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini2.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.AnswerTest.TitleAnswerTest%>
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainQuestionnaire" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%: Url.Content("~/Scripts/mobile/jquery.validate.mobile.js") %>" type="text/javascript"></script>
<script type="text/javascript">
    $.validator.messages.required = "<%: ViewRes.Views.Evaluation.AnswerTest.Required %>";
    $.validator.messages.email = "<%: ViewRes.Models.User.EmailInvalid %>";


    $(document).on('pageinit', '#mainQuestionnaire', function () {
        $(document).on("submit", "form.msform", handleMSForm)
        $.mobile.changePage("#step1");
    });

    $(document).on("pagebeforechange", function (e, ob) {
        if (ob.toPage && (typeof ob.toPage === "string") && ob.toPage.indexOf('#') < 0 && ob.toPage.indexOf('MobileDemographicsAnswerTest') >= 0) {
            console.log("blocking the back");
            e.preventDefault();
        }
        if (ob.toPage && (typeof ob.toPage === "string") && ob.toPage.indexOf('#') < 0 && ob.toPage.indexOf('MobileWithDemographicAnswerTest') >= 0) {
            console.log("blocking the back");
            e.preventDefault();
        }
    });

    var formData = {};
    var cambiar = "";

    $(document).on('submit', '#formStep1', function (event) {

        var correo = $("#10033739").val();
        //alert(correo);
        if (correo) {
            $.ajax({
                type: "POST",
                url: "/Evaluations/CheckEmail",
                data: { email: correo },
                async: false,
                success: function Success(data) {
                    if (data) {
                    }
                    else {
                        alert("El email ya ha sido utilizado");
                        cambiar = "no";
                        event.preventDefault();
                    }
                }
            });
        }
    });

    function handleMSForm(e) {
        var next = "";

        //gather the fields
        var data = $(this).serializeArray();
        //store them - assumes unique names
        for (var i = 0; i < data.length; i++) {
            //If nextStep, it's our metadata, don't store it in formdata
            if (data[i].name == "nextStep") {
                next = data[i].value;
                continue;
            }
            //if we have it, add it to a list. This is not "comma" safe.
            if (formData.hasOwnProperty(data[i].name))
                formData[data[i].name] = data[i].value;
            else
                formData[data[i].name] = data[i].value;

        }
        //now - we need to go the next page...
        //if next step isn't a full url, we assume internal link
        //logic will be, if something.something, do a post
        if (next.indexOf(".") == -1) {
            var nextPage = "#" + next;
            if (cambiar == "no") {
                cambiar = "";
            }
            else {
                $.mobile.changePage(nextPage);
            }
        } else {
            $.mobile.changePage("/Evaluations/MobileAnswerTest", { type: "post", data: formData });
        }
        e.preventDefault();
    };

</script>

<h1 class="testname"><%= Model.test.Name%></h1> 
<div class="box rounded">
<%--    <h1></h1>
    <h4><%: ViewRes.Models.Questionnaire.Instructions %>: <%: Model.QuestionnaireToUse.Instructions.ToString() %></h4>
    <p><a href="#step1" data-role="button" data-icon="arrow-r" data-theme="f"><%: ViewRes.Views.Evaluation.AnswerTest.Page %> 1</a></p>
    <h1>    </h1>	--%>
</div>
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
        <% Html.RenderPartial("QuestionnaireForm.Mobile"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>
