<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewRes.Views.Shared.Shared.GenericTitle %><%: ViewRes.Views.Home.Index.TitleIndex %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageId" runat="server">
    <div data-role="page" id="mainIndex" class="basic">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).on('pageinit', '#mainIndex', function (e) {
            $.validator.unobtrusive.parse($(this).find('form'));

            if ($.cookie('UserName') && $.cookie('Password')) {
                $("#UserName").val($.cookie('UserName'));
                $("#Password").val($.cookie('Password'));
            }
        });
    </script>

    <% if (!Request.IsAuthenticated)
       { %>
            <% Html.RenderPartial("/Views/Home/PartialLogOn.Mobile.ascx"); %>
    <% } else { %>

        <div class="box rounded">    
            <h1><%: User.Identity.Name %><br />
                <%: ViewRes.Views.Home.Index.Welcome %>
            </h1>
            <% if (User.Identity.Name.Equals("acrip", StringComparison.InvariantCultureIgnoreCase)){ %>
                <div style="text-align:center">
                    <div>Para continuar haga click en la imagen </div>
                    <a href="/Home/Event" data-ajax="false" title="acrip"><img src="../../Content/mobile/Images/evento/acrip.png" alt="acrip" /></a>
                </div>
            <% } %> 
            <% if (User.Identity.Name.Equals("todovision", StringComparison.InvariantCultureIgnoreCase)){ %>
                <div style="text-align:center">
                    <div>Para continuar haga click en la imagen </div>
                    <a href="#event" title="Todovision"><img src="../../Content/mobile/Images/evento/cmc.png" alt="CMC" /></a>
                </div>
            <% } %> 
            <% if (User.Identity.Name.Equals("cigeh", StringComparison.InvariantCultureIgnoreCase)){ %>
                <div style="text-align:center">
                    <div>Para continuar haga click en la imagen </div>
                    <a href="#event" title="Cigeh"><img src="../../Content/mobile/Images/evento/evento-logo.png" alt="cigeh" /></a>
                </div>
            <% } %> 
            <% if (User.Identity.Name.Equals("svd", StringComparison.InvariantCultureIgnoreCase)){ %>
                <div style="text-align:center">
                    <div>Para continuar haga click en la imagen </div>
                    <a href="#event" title="Sociedad Venezolana de Dermatología"><img src="../../Content/mobile/Images/evento/svd.png" alt="Sociedad Venezolana de Dermatología" /></a>
                </div>
            <% } %> 
            <% if (Roles.IsUserInRole("HRAdministrator") || Roles.IsUserInRole("HRCompany") || Roles.IsUserInRole("CompanyManager") || Roles.IsUserInRole("FreeReports")){ %>
                <div class="ui-grid-b">
	                <div class="ui-block-a alignCenter" style="width:48%">                        
                        <h2 class="sub"><%: ViewRes.Views.Home.Index.Reports%></h2>                             
                        <a href="/ChartReports/ReportsList" data-ajax="false">
                            <span class="imagen-reportes"></span>
                        </a>
                    </div>
                    <div class="ui-block-b alignCenter" style="width:4%"> 
                    </div>
<%--	                <div class="ui-block-c alignCenter" style="width:48%">                        
                        <h2 class="sub"><%: ViewRes.Views.Home.Index.GlobalClimate%></h2>                             
                        <a href="#globalClimate">
                            <span class="imagen-clima"></span>
                        </a>
                    </div>--%>
	                <%--<div class="ui-block-c alignCenter" style="width:48%"> 
                        <h2 class="sub"><%: ViewRes.Views.Home.Index.HelpMsg%></h2>     
                        <a href="#help"><img src=<%: ViewRes.Views.Home.Index.Help %> alt="" style="max-width:124px;max-height:128px;"/></a>
                    </div>--%>
                </div>
            <% }
            %>
<%--            <div class="ui-grid-solo alignCenter">
	            <div class="ui-block-a">
                    <a href="#help"><img src=<%: ViewRes.Views.Home.Index.Help %> alt=""/>
                    </a>
                </div>
            </div>--%>
        </div>
    <% } %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">

        <div data-role="page" id="globalClimate" class="basic">
            <% Html.RenderPartial("mobile/MobileHeaderMini"); %> 
            <% Html.RenderPartial("mobile/MobileMenu"); %> 
            <% Html.RenderPartial("mobile/MobileSubMenu"); %> 
            <div data-role="content" class="content basic">
                <h4><%: ViewRes.Views.Home.Index.Welcome %>&nbsp-&nbsp<%: Html.ActionLink(ViewRes.Views.Shared.Shared.Logout, "LogOff", "Account")%></h4>
                <div class="box rounded">
                    <h1><%: ViewRes.Views.Home.Index.GlobalClimate%></h1>
                    En construccion ...
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="help" class="basic">
            <% Html.RenderPartial("mobile/MobileHeaderMini"); %> 
            <% Html.RenderPartial("mobile/MobileMenu"); %> 
            <% Html.RenderPartial("mobile/MobileSubMenu"); %> 
            <div data-role="content" class="content basic">
                <h4><%: ViewRes.Views.Home.Index.Welcome %>&nbsp-&nbsp<%: Html.ActionLink(ViewRes.Views.Shared.Shared.Logout, "LogOff", "Account")%></h4>
                  <div class="box rounded">
                    <h1><%: ViewRes.Views.Home.Index.HelpMsg %></h1>
                    En construccion ...
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval1" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Dialog1">
                    <h1>Conferencia de apertura</h1>
                    <p>"Neurociencia en el mundo del Management; como nuestro cerebro puede 
                                 ayudarnos a ser mejores líderes" 
                    </p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=bc13584f-e73a-4c09-8f15-99fa0d60439f" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval2" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div2">
                    <h1>Conferencia 2</h1>
                    <p>Líderes Grandiosos: Cómo liberar el Talento del capital humano en el nuevo contexto global</p>
                    <p>Por José Gabriel "Pepe" Miralles -  CEO Franklin Covey Latam</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=3dc4d863-3b51-46bd-a45b-5b76cf55bd65" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval3" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div4">
                    <h1>Conferencia 3</h1>
                    <p>Recursos Humanos Comprometido o Involucrado en los Procesos de Cambio</p>
                    <p>Por Raquel Zambrano - Socio Director Henka Change Mangement Consulting</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=e92a28e0-2b36-44dd-88b6-73f2606d753c" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval4" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div6">
                    <h1>Panel 1</h1>
                    <p>Desafíos de CEOs de Latinoamérica Lo que todo Gestor de talento Humano debe saber de su CEO</p>
                    <p>Moderador: Eladio Uribe - Vicepresidente de Gestión Humana de Grupo Industrial Romana de República Dominicana</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=bff88d58-bfbd-4939-af8a-83d1a14b13cb" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval5" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div8">
                    <h1>Conferencia 4</h1>
                    <p>Talento Verde: Desarrollando las Competencias Ambientales </p>
                    <p>Por Ángela María Salazar  - Directora Green Citizen foundation</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=16cc836b-4e0c-4598-a792-ba1a1d1d9b12" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval6" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div10">
                    <h1>Pensamiento GLOBAL Y VISIONARIO para la solución de problemas</h1>
                    <p>Por María Socorro</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=51c0c5a7-0a76-41ca-99f1-8b694a6a88bf" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval7" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div12">
                    <h1>Las 12 mejores prácticas de los Administradores de Capacitación </h1>
                    <p>Por Jan Reis – Best Training</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=d09f1e2a-e177-4cac-8d8c-e78036b58fcb" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval8" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div14">
                    <h1>Conferencia 5</h1>
                    <p>Cómo crear una Cultura Emprendedora: apelando a las nuevas generaciones</p>
                    <p>Por Julio Zelaya  - Director Ejecutivo de The Learning Group</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=36cb1663-aadf-48ad-87bc-47bcbe019735" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval9" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div16">
                    <h1>Conferencia 6</h1>
                    <p>La Cultura y el Talento como ventaja Competitivas</p>
                    <p>Por Claudia Valverde – Great Place to Work</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=6dd8af12-7bbe-4f9e-84c2-6c0cb7d02026" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval10" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div18">
                    <h1>Conferencia 7</h1>
                    <p>Creando el Talento Humano </p>
                    <p>Por Alfredo Diez - ADEN Business School</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=614f3ae0-33f6-4cdf-9db8-2c8171d91123" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

        <div data-role="page" id="eval11" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div20">
                    <h1>Conferencia 8</h1>
                    <p>La Neurociencia del Liderazgo; Como crear Mejores Líderes en sus organizaciones</p>
                    <p>Por Ciro Alejandro Pérez - Socio Director Change Americas</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=9e1d42cf-892d-474c-97cc-296d0d352abc" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

            <div data-role="page" id="eval12" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div22">
                    <h1>Panel 2</h1>
                    <p>VP de Recursos Humanos: Tendencias de Recursos Humanos en la Región</p>
                    <p>Moderador: Jaime Bocanegra - Gerente de Consultoria de PWC</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=134bf156-d5ea-411f-9c45-bea066e78279" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

            <div data-role="page" id="eval13" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div24">
                    <h1>Conferencia 9</h1>
                    <p>TRASCENDIENDO PARADIGMAS: Del desarrollo a la retención de Talento, de la Mejora Continua a una Cultura de Coaching como una   estrategia Organizacional </p>
                    <p>Por Thomas Köttner  - Coach Ready</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=58d857aa-d927-4af3-9ede-360f649235f5" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>

            <div data-role="page" id="eval14" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div26">
                    <h1>Conferencia de Clausura</h1>
                    <p>"Trabajo y Felicidad, el Sentido de lo que Hacemos"</p>
                    <p>Por Ricardo Gómez</p>
                    <div class="ui-grid-a">
                        <div class="ui-block-a">
                            <a href="/Evaluations/MobileDemographicsAnswerTest?code=1d3cb876-186e-42be-af29-c8861791095a" data-role="button" data-theme="f">Evaluar</a>
                        </div>
                        <div class="ui-block-b">
                            <a href="#main" data-rel="back" data-role="button" data-icon="back" data-theme="f">Regresar</a>
                        </div>
                    </div>
                </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>


        <div data-role="page" id="event" class="basic">
            <% Html.RenderPartial("mobile/MobileHeaderMini"); %> 
            <% Html.RenderPartial("mobile/MobileMenu"); %> 
            <% Html.RenderPartial("mobile/MobileSubMenu"); %> 
            <div data-role="content" class="content basic">
                <div class="box rounded">
                    <h1>Evento</h1>
                    <% if (User.Identity.Name.Equals("todovision", StringComparison.InvariantCultureIgnoreCase) || User.Identity.Name.Equals("cigeh", StringComparison.InvariantCultureIgnoreCase))
                       { %>
                    <div data-role="collapsible-set" data-theme="f" data-content-theme="c">
					    <div data-role="collapsible">
						    <h2>DIA 17</h2>
                            <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
							    <li><h2>3:00-4:30 | Registro</h2></li>
                                <li><h2>4:30-5:00 | Ceremonia de apertura</h2></li>
                                <li><a href="#eval1" data-rel="dialog"><h2>5:00-6:15 | Conferencia de Apertura</h2></a></li>
                                <li><h2>6:15-8:00 | Coctel de Apertura / Inauguración de la Expoferia</h2></li>
						    </ul>
					    </div>
                        <div data-role="collapsible">
						    <h2>DIA 18</h2>
                            <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
							    <li><h2>7:00-8:00 &nbsp &nbsp&nbsp| Registro</h2></li>
                                <li><a href="#eval2" data-rel="dialog"><h2>8:00-9:00 &nbsp &nbsp&nbsp| Conferencia 2</h2></a></li>
                                <li><a href="#eval3" data-rel="dialog"><h2>9:00-10:00 &nbsp&nbsp| Conferencia 3</h2></a></li>
                                <li><h2>10:00-10:45 | Receso y Visita a la Expo Feria</h2></li>
                                <li><a href="#eval4" data-rel="dialog"><h2>10:45-12:00 | Panel 1</h2></a></li>
                                <li><h2>12:00-12:30 | Anuncio Especial</h2></li>
                                <li><h2>12:30-1:30 &nbsp&nbsp| Almuerzo</h2></li>
                                <li><h2>1:30-2:00 &nbsp &nbsp&nbsp| Visita a la Expo</h2></li>
                                <li><a href="#eval5" data-rel="dialog"><h2>2:00-3:00 &nbsp &nbsp&nbsp| Conferencia 4</h2></a></li>
                                <li><h2>2:00-5:00 &nbsp &nbsp&nbsp| Experiencias Virtuales de Aprendizaje Acelera Por Simi Benhamu – Ateneo</h2></li>
						        <li><a href="#eval6" data-rel="dialog"><h2>2:00-5:00 &nbsp &nbsp&nbsp| Pensamiento GLOBAL Y VISIONARIO para la solución de problemas</h2></a></li>
                                <li><a href="#eval7" data-rel="dialog"><h2>2:00-5:00 &nbsp &nbsp&nbsp| Las 12 mejores prácticas de los Administradores de Capacitación</h2></a></li>
                                <li><a href="#eval8" data-rel="dialog"><h2>3:00-4:00 &nbsp &nbsp&nbsp| Conferencia 5</h2></a></li>
                                <li><h2>4:00-4:30 &nbsp &nbsp&nbsp| Receso y Visita a la Expo</h2></li>
                                <li><a href="#eval9" data-rel="dialog"><h2>4:30-5:30 &nbsp &nbsp&nbsp| Conferencia 6</h2></a></li>
                                <li><h2>6:00-7:00 &nbsp &nbsp&nbsp| Traslado a la Cena de Gala</h2></li>
                                <li><h2>7:30-10:00 &nbsp&nbsp| Cena de Gala / Entrega del Premio Oscar Alvear Urrutia</h2></li>
                            </ul>
					    </div>
                        <div data-role="collapsible">
						    <h2>DIA 19</h2>
						    <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
						        <li><h2>7:00-8:00 &nbsp &nbsp&nbsp| Registro</h2></li>
                                <li><a href="#eval10" data-rel="dialog"><h2>8:00-9:00 &nbsp &nbsp&nbsp| Conferencia 7</h2></a></li>
                                <li><a href="#eval11" data-rel="dialog"><h2>9:00-10:00 &nbsp&nbsp| Conferencia 8</h2></a></li>
                                <li><a href="#eval12" data-rel="dialog"><h2>10:45-12:00 | Panel 2</h2></a></li>
                                <li><h2>12:00-1:30 &nbsp&nbsp| Almuerzo y Visita a la Expo</h2></li>
                                <li><h2>1:30-2:00 &nbsp &nbsp&nbsp| Anuncio Especial</h2></li>
                                <li><a href="#eval13" data-rel="dialog"><h2>2:00-3:00 &nbsp &nbsp&nbsp| Conferencia 9</h2></a></li>
                                <li><a href="#eval14" data-rel="dialog"><h2>3:00-4:30 &nbsp &nbsp&nbsp| Conferencia de Clausura</h2></a></li>
                                <li><h2>4:30-5:00 &nbsp &nbsp&nbsp| Palabras de Clausura</h2></li>
                                <li><h2>5:00-7:00 &nbsp &nbsp&nbsp| Tarde de Coctel, Quesos y Vinos</h2></li>
                            </ul>
					    </div>
                        <div data-role="collapsible">
						    <h2>Evaluación General</h2>
						    <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-split-theme="d">
                                <li><a href="/Evaluations/MobileDemographicsAnswerTest?code=ccbad0e8-804e-4f66-bd8d-0af6917d2e5a"><img src="../../Content/mobile/Images/evento/evento.png"/><h2>Evaluación general del evento</h2></a></li>
                                <%--<li><a href="/Evaluations/AllTickets"><img src="../../Content/mobile/Images/evento/logistica2.png"/><h2>Ver Tickets</h2></a></li>--%>
						    </ul>
					    </div>

                </div>
                <%}
                  else if (User.Identity.Name.Equals("svd", StringComparison.InvariantCultureIgnoreCase))
          { %>
          <div data-role="collapsible-set" data-theme="f" data-content-theme="c">
            <div data-role="collapsible">
				<h2>Miércoles 23/10/2013</h2>
                <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
					<li><h2>8:00am-9:40am &nbsp &nbsp&nbsp| Curso Pre-Congreso Dermatoscopia</h2></li>
                    <li><h2>9:40am-10:00am &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><h2>10:00am-12:10pm &nbsp &nbsp&nbsp| Curso Pre-Congreso Dermatoscopia</h2></li>
                    <li><h2>12:10pm-1:30pm &nbsp &nbsp&nbsp| Almuerzo</h2></li>
                    <li><h2>1:30pm-3:10pm &nbsp &nbsp&nbsp| Curso Pre-Congreso Dermatoscopia</h2></li>
                    <li><h2>3:10pm-3:40pm &nbsp &nbsp&nbsp| Almuerzo</h2></li>
                    <li><h2>1:30pm-3:10pm &nbsp &nbsp&nbsp| Curso Pre-Congreso Dermatoscopia</h2></li>
                </ul>
			</div>
            <div data-role="collapsible">
				<h2>Jueves 24/10/2013</h2>
				<ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
                    <li><a href="#eval8" data-rel="dialog"><h2>7:45am-9:00am &nbsp &nbsp&nbsp| Foro Di Prisco</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>9:00am-9:45am &nbsp &nbsp&nbsp| Simposio Dermatología Pediátrica</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>9:45am-10:30am &nbsp &nbsp&nbsp| Simposio Acné</h2></a></li>   
                    <li><h2>10:30am-10:55am &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><a href="#eval8" data-rel="dialog"><h2>10:55am-11:40am &nbsp &nbsp&nbsp| Simposio Erupciones Medicamentosas</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>11:40am-12:00m &nbsp &nbsp&nbsp| Conferencia Magistral</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>12:00m-12:45pm &nbsp &nbsp&nbsp| Simposio Toxina Botulínica</h2></a></li>   
                    <li><h2>12:45pm-2:15pm &nbsp &nbsp&nbsp| Almuerzo</h2></li>
                    <li><a href="#eval8" data-rel="dialog"><h2>2:15pm-5:30pm &nbsp &nbsp&nbsp| Curso Teórico-Práctico Cirugía Dermatológica Básica</h2></a></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>2:15pm-3:00pm &nbsp &nbsp&nbsp| Simposio Psoriasis</h2></a></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>3:00pm-4:15pm &nbsp &nbsp&nbsp| Simposio Micología</h2></a></li>   
                    <li><h2>4:15pm-4:40pm &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><a href="#eval9" data-rel="dialog"><h2>4:40pm-5:30pm &nbsp &nbsp&nbsp| Simposio Perfil del Dermatólogo en RRSS</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>5:30pm-6:30pm &nbsp &nbsp&nbsp| Minicasos</h2></a></li>   
                </ul>
			</div>
            <div data-role="collapsible">
				<h2>Viernes 25/10/2013</h2>
				<ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
                    <li><h2>7:30am-8:30am &nbsp &nbsp&nbsp| Desayuno Conferencia</h2></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>8:30am-9:30am &nbsp &nbsp&nbsp| Simposio Cirugía Dermatológica</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>9:30am-10:10am &nbsp &nbsp&nbsp| Conferencias Magistrales</h2></a></li>   
                    <li><h2>10:10am-10:35am &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><a href="#eval8" data-rel="dialog"><h2>10:35am-11:20am &nbsp &nbsp&nbsp| Simposio Antienvejecimiento</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>11:20am-12:00m &nbsp &nbsp&nbsp| Conferencia Magistral</h2></a></li>   
                    <li><h2>12:00pm-1:30pm &nbsp &nbsp&nbsp| Almuerzo</h2></li>
                    <li><a href="#eval8" data-rel="dialog"><h2>1:30pm-2:15pm &nbsp &nbsp&nbsp| Simposio Psoriasis</h2></a></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>2:15pm-3:00pm &nbsp &nbsp&nbsp| Simposio Melanoma</h2></a></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>3:00pm-3:45pm &nbsp &nbsp&nbsp| CMB</h2></a></li>   
                    <li><h2>3:45pm-4:10pm &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><a href="#eval9" data-rel="dialog"><h2>4:10pm-5:10pm &nbsp &nbsp&nbsp| Encuentro Colombo-Venezolano</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>5:10pm-6:40pm &nbsp &nbsp&nbsp| Trabajos Libres</h2></a></li>   
                </ul>
			</div>
            <div data-role="collapsible">
				<h2>Sábado 26/10/2013</h2>
				<ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
                    <li><a href="#eval9" data-rel="dialog"><h2>7:30am-8:30am &nbsp &nbsp&nbsp| Minicasos</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>8:30am-10:00am &nbsp &nbsp&nbsp| Sesión Anatomoclínica</h2></a></li>   
                    <li><h2>10:00am-10:20am &nbsp &nbsp&nbsp| Receso</h2></li>
                    <li><a href="#eval8" data-rel="dialog"><h2>10:20am-11:20am &nbsp &nbsp&nbsp| Simposio Acné</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>11:20am-12:20pm &nbsp &nbsp&nbsp| Simposio Cilad</h2></a></li>   
                    <li><h2>12:20pm-1:20pm &nbsp &nbsp&nbsp| Presentación de Posters Seleccionados</h2></li>   
                    <li><h2>1:20pm-2:20pm &nbsp &nbsp&nbsp| Lunch</h2></li>
                    <li><h2>2:20pm-3:20pm &nbsp &nbsp&nbsp| Presentación de Minicasos Seleccionados</h2></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>3:20pm-4:20pm &nbsp &nbsp&nbsp| ¿Qué hay de nuevo?</h2></a></li>   
                    <li><a href="#eval8" data-rel="dialog"><h2>4:20pm-5:20pm &nbsp &nbsp&nbsp| Reunión Administrativa</h2></a></li>   
                </ul>
			</div>
            <div data-role="collapsible">
				<h2>Evaluación General</h2>
				<ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-split-theme="d">
                    <li><a href="/Evaluations/MobileDemographicsAnswerTest?code=ccbad0e8-804e-4f66-bd8d-0af6917d2e5a"><img src="../../Content/mobile/Images/evento/evento.png"/><h2>Evaluación general del evento</h2></a></li>
                    <%--<li><a href="/Evaluations/AllTickets"><img src="../../Content/mobile/Images/evento/logistica2.png"/><h2>Ver Tickets</h2></a></li>--%>
				</ul>
			</div>

        </div>
          <%}
                   %>
            </div>
            </div>
            <% Html.RenderPartial("mobile/MobileFooter"); %> 
        </div>
</asp:Content>