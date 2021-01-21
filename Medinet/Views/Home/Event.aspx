<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/mobile/MobileMini.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Evento
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box rounded">
        <h1>Evento - Encuentro Nacional Avances</h1>
        <% if (User.Identity.Name.Equals("acrip", StringComparison.InvariantCultureIgnoreCase) || User.Identity.Name.Equals("cigeh", StringComparison.InvariantCultureIgnoreCase))
            { %>
        <div data-role="collapsible-set" data-theme="f" data-content-theme="c">
            <div data-role="collapsible">
				<h2>DIA 5/9/13</h2>
                <ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
					<li><h2>7:00am-8:00am &nbsp &nbsp&nbsp| Inscripciones</h2></li>
                    <li><h2>8:00am-8:30am &nbsp &nbsp&nbsp| Apertura e Instalación del Evento</h2></li>
                    <li><a href="#eval1" data-rel="dialog"><h2>8:30am-9:30am &nbsp &nbsp&nbsp| Conferencia 1 - ¿Cómo se esta liderando la ciudad para ser sostenible?</h2></a></li>
                    <li><a href="#eval2" data-rel="dialog"><h2>9:30am-10:30am &nbsp&nbsp| Conferencia 2 - Perspectivas de inclusión laboral en población vulnerable, una propuesta a la paz</h2></a></li>
                    <li><h2>10:30am-11:00am | Registro - Visita Muestra Comercial</h2></li>
                    <li><a href="#eval3" data-rel="dialog"><h2>11:00am-12:30am | Conferencia 3 - Colombia analizada desde afuera</h2></a></li>
                    <li><a href="#eval4" data-rel="dialog"><h2>2:00pm-2:45pm &nbsp &nbsp&nbsp| Conferencia 4 - La sostenibilidad clave de la competitividad en los nuevos mercados globales</h2></a></li>
                    <li><a href="#eval5" data-rel="dialog"><h2>2:45pm-3:45pm &nbsp &nbsp&nbsp| Conferencia 5 - El sector privado frente a la Sostenibilidad</h2></a></li>
                    <li><h2>3:45pm-4:15pm &nbsp &nbsp&nbsp| Premio Orden al Mérito</h2></li>
                    <li><h2>4:15pm-4:45pm &nbsp &nbsp&nbsp| Refrigerio - Visita Muestra Comercial</h2></li>
					<li><a href="#eval6" data-rel="dialog"><h2>4:45pm-6:15pm &nbsp &nbsp&nbsp| Conferencia 6 - Viral Change and Human Resources</h2></a></li>
                    <li><a href="#eval7" data-rel="dialog"><h2>6:15pm-8:00pm &nbsp &nbsp&nbsp| Conferencia 7 - Cóctel - SANFORD</h2></a></li>
                </ul>
			</div>
            <div data-role="collapsible">
				<h2>DIA 6/9/13</h2>
				<ul data-role="listview" data-filter="true" data-filter-theme="c" data-divider-theme="d" data-icon="plus" data-split-theme="d">
                    <li><a href="#eval8" data-rel="dialog"><h2>8:00am-9:00am &nbsp &nbsp&nbsp| Conferencia 8 - Gestión Humana Sostenible y Globalización</h2></a></li>   
                    <li><a href="#eval9" data-rel="dialog"><h2>9:00am-9:45am &nbsp &nbsp&nbsp| Conferencia 9 - La Importancia de la Gestión Humana en la Sostenibilidad</h2></a></li>   
                    <li><h2>9:45am-10:15am &nbsp&nbsp| Registro - Visita Muestra Comercial</h2></li>
                    <li><a href="#eval10" data-rel="dialog"><h2>10:15am-11:00am | Conferencia 10 - Entorno estratégico que impacta la sostenibilidad</h2></a></li> 
                    <li><h2>11:00am-11:30am | Cementos Argos / Imbera Friomix El Cauca</h2></li>
                    <li><a href="#eval11" data-rel="dialog"><h2>11:30am-12:00pm | Conferencia 11 - Proteger el Medio Ambiente también es Negocio</h2></a></li> 
                    <li><a href="#eval12" data-rel="dialog"><h2>2:00pm-3:00pm &nbsp &nbsp&nbsp| Conferencia 12 - La nueva era : El talentismo</h2></a></li> 
                    <li><a href="#eval13" data-rel="dialog"><h2>3:00pm-3:40pm &nbsp &nbsp&nbsp| Conferencia 13 - El aporte de las mujeres de éxito al País</h2></a></li> 
                    <li><h2>3:40pm-4:30pm &nbsp &nbsp&nbsp| Transmisión en Vivo de partido de la Selección de Colombia</h2></li>
                    <li><h2>4:30pm-5:00pm &nbsp &nbsp&nbsp| Refrigerio - Visita Muestra Comercial</h2></li>
                    <li><h2>5:00pm-6:00pm &nbsp &nbsp&nbsp| Transmisión en Vivo de partido de la Selección de Colombia</h2></li>  
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
        <%} %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="PageId" runat="server">
<div data-role="page" id="mainEventIndex" class="basic">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="PageContent" runat="server">
        <div data-role="page" id="eval1" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Dialog1">
                    <h1>Conferencia 1</h1>
                    <p><strong>RODRIGO GUERRERO</strong> - Alcalde de Cali</p>
                    <p><strong>ALFREDO SARMIETO GOMEZ</strong> - Dividendo por Colombia</p>
                    <p><strong>MAURICIO OLIVERA</strong> - Viceministro de Empleo y Pensiones</p>
                    <p>Tema: ¿Cómo se esta liderando la ciudad para ser sostenible?</p>
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
                    <p><strong>ALEJANDRO EDER</strong> - Director General de la Agencia Colombiana de la Reinserción ACR</p>
                    <p><strong>JUAN FELIPE MONTOYA</strong> - Vicepresidente de RRHH - Grupo éxito Comité de Cafeteros</p>
                    <p>Foro "Perspectivas de inclusión laboral en población vulnerable, una propuesta a la paz"</p>
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
                    <p><strong>OCTAVIO AGUILAR - MÉXICO</strong> - Accountability Sustainability</p>
                    <p>Colombia analizada desde afuera</p>
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
                    <h1>Conferencia 4</h1>
                    <p><strong>MANUEL RAMIRO MUÑOZ</strong> - Director Centro de Estudios Interculturales y Presidente de Gremios Azucareros</p>
                    <p>La sostenibilidad clave de la competitividad en los nuevos mercados globales</p>
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
                    <h1>Conferencia 5</h1>
                    <p><strong>MANUEL RAMIRO MUÑOZ</strong> - Director Centro de Estudios Interculturales y Presidente de Gremios Azucareros</p>
                    <p><strong>TYRONE ZIACHOQUE</strong> - Gerente Gestión humana del sector Agro Industial del Grupo Empresarial Ardila Lule</p>
                    <p><strong>DIEGO SALAZAR</strong> - Gerente Corporativo de Gestión Humana de Manuelita SA</p>
                    <p>El sector privado frente a la Sostenibilidad</p>
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
                    <h1>Conferencia 6</h1>
                    <p><strong>LEANDRO HERRERO</strong> - Conferencista Inglés</p>
                    <p>Viral Change and Human Resources</p>
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
                    <h1>Conferencia 7</h1>
                    <p><strong>Fenix "La Magia de lo Etéreo"</strong></p>
                    <p>Cóctel - SANFORD</p>
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
                    <h1>Conferencia 8</h1>
                    <p><strong>MIGUEL DÍAZ</strong></p>
                    <p>Gestión Humana Sostenible y Globalización</p>
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
                    <h1>Conferencia 9</h1>
                    <p><strong>ANDRÉS LÓPEZ</strong> - Director Postrado Gerencia del Medio ambiente - Universidad Icesi</p>
                    <p>La importancia de la Gestión Humana en la Sostenibilidad</p>
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
                    <h1>Conferencia 10</h1>
                    <p><strong>PEDDRO FELIPE CARVAJAL</strong></p>
                    <p>Reto GH: "Aportar a la sostenibilidad con talento humano calfocado y motivado"</p>
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

        <div data-role="page" id="eval13" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div20">
                    <h1>Conferencia 13</h1>
                    <p><strong>JORGE CAGIGAS</strong> - España</p>
                    <p>La nueva era: El talentismo, clave de la nueva competitividad eje principal en la sostenibilidad</p>
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

        <div data-role="page" id="eval11" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div22">
                    <h1>Conferencia 11</h1>
                    <p><strong>ALFREDO MARÍN</strong> - Director del Departamento Ténico y Ambiental</p>
                    <p>"Proteger al Medio Ambiente también es Negocio" Caso Carton de Colombia</p>
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

        <div data-role="page" id="eval12" class="basic"  data-close-btn="right">
            <div data-role="header" data-theme="f">
                <h1>HRMEDINET</h1>
	        </div>
            <div data-role="content" class="content basic">
                <div class="box rounded" id="Div24">
                    <h1>Conferencia 12</h1>
                    <p><strong>NANCY VALERO</strong></p>
                    <p>Fundacíon Mujeres de Éxito. El aporte de las mujeres de éxio al País</p>
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
</asp:Content>
