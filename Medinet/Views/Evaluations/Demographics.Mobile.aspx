<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<Medinet.Models.ViewModels.EvaluationViewModel>" %>
<%@ Import Namespace="MedinetClassLibrary.Models" %>
<%@ Import Namespace="Medinet.Models.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %> <%: ViewRes.Views.Evaluation.AnswerTest.TitleAnswerTest%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainDemographic" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<script src="<%: Url.Content("~/Scripts/mobile/jquery.validate.mobile.js") %>" type="text/javascript"></script>--%>
    <script type="text/javascript">

        $.validator.messages.required = "<%: ViewRes.Views.Evaluation.AnswerTest.Required %>";

        $(document).on('pageinit', '#mainDemographic', function () {
            if ($("#errorEmail").val() == "El email ya ha sido utilizado") {
                alert("El email ya ha sido utilizado");
            }
            $("#demographic").validate({
                errorPlacement: function (error, element) {
                    if (element.is(":radio")) {
                        error.insertAfter($("#sexerror"));
                    }
                    else {
                        error.insertAfter($(element).parent());
                    }
                }

            });
            //            $('input:radio').change(function (event, ui) {
            //                $('#demographic').validate().form();
            //            });
        });

    function loadFO(item,select) { 
        removeFO(parseInt(select.id));    
        if (item.value != "") {
            $.ajax({
                type: "POST",
                url: "/Evaluations/MobileGetFOTChildren",
                data: { foParent_id: parseInt(item.value) },
                async: false,
                success: function Success(data) {
                    printFO(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
    }

    function removeFO(id) {
        $.ajax({
            type: "POST",
            url: "/Evaluations/MobileRemoveFOTChildren",
            data: { fotId: id },
            async: false,
            success: function Success2(data) {
                eraseFO(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });            
    }

    function eraseFO(data) {
        for (var i = 0; i < data.length; i++) {
            $("select[id*=" + data[i].Id + "]>option:not(:first)").remove();
        }
    }

    function printFO(data) {
        for (var i = 0; i < data.length; i++) {
            //$("select[id*=hijo]").append($("<option></option>").val(data[i].Id).html(data[i].Name));
            $("select[id*=" + data[i].Type_Id + "]").append($("<option></option>").val(data[i].Id).html(data[i].Name));
        }
    }
    </script>

    <h1 class = "testname"><%= Model.test.Name%></h1> 
    <div class="box rounded txt">  
       
        <div>
            <%int ev_id;
              int demoNumber = Model.test.DemographicsInTests.Count;
                if (Model.previousEvaluation != null)
                {
                    ev_id = Model.previousEvaluation.Id; %>
                <%}
                else
                {
                    ev_id = 0; %>
                &nbsp;
            <%} %>
        </div>
        <% using (Html.BeginForm("MobileDemographicsAnswerTest", "Evaluations", FormMethod.Post, new {  @id = "demographic", data_ajax="false"}))
           { %>
            <%: Html.HiddenFor(model => model.test.Id)%>
            <%: Html.HiddenFor(model => model.evaluation.Test_Id)%>
            <%: Html.Hidden("evaluation.Id", ev_id)%>
            <% if (Model.conferencia){ %>
                <div>
                    <h1>Datos Personales</h1>
                    <%: Html.Hidden("conferencia","true") %>
                    <fieldset data-role="fieldcontain">
                        <label for="nombre">Nombre:</label>
                        <input type="text" name="nombre" id="nombre" class="required"/>
                    </fieldset>
                    <fieldset data-role="fieldcontain">
                        <label for="email">Correo:</label>
                        <input type="email" name="email" id="email" placeholder="helloWorld@world.com" class="required" />
                        <%: Html.Hidden("errorEmail", ViewData["email"]) %>
                    </fieldset>
                </div>
            <% } %>
            <div> 
                <% Html.RenderPartial("DemographicsForm.Mobile"); %>
            </div>
            <h1></h1>
            <fieldset class="ui-grid-a">
	            <div class="ui-block-a"><a href="#" data-icon="arrow-l" data-rel="back" data-role="button" data-theme="f"><%: ViewRes.Views.Evaluation.AnswerTest.ButtonBack%></a>		</div>
	            <div class="ui-block-b"><input data-icon="arrow-r" data-iconpos="right" data-role="button" name="button" data-theme="f" value="<%: ViewRes.Views.Evaluation.AnswerTest.ButtonNext %>" type="submit" /></div>	   
            </fieldset>
        <% } %>
    </div>
</asp:Content>
