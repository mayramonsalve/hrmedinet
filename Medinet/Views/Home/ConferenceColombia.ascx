<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Medinet.Models.Helpers" %>

<div class="prepend-1 span-22 append-1">
    <h3 style="font-size:medium">Cronograma de participación.</h3>
    <br />
    <div id="accordion" class="myaccordion">
        <h3><a href="#">Día 1: 06/09/2013</a></h3>
	    <div style="height: 230px">
            <table id="table1" class="display tabla">
                 <thead>
                    <tr> 
                         <th>Hora</th>
                         <th>Evento</th>
                         <th></th>
                    </tr>
                 </thead>  
                <tbody>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">7:00am-8:00am</td>
                    <td style="vertical-align:middle">Inscripciones.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">8:00am-8:30am</td>
                    <td style="vertical-align:middle">Apertura e Instalación del Evento.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">8:30am-9:30am</td>
                    <td style="vertical-align:middle">Conferencia 1.<br/>
                        <strong>RODRIGO GUERRERO</strong> - Alcalde de Cali<br/>
                        <strong>ALFREDO SARMIETO GOMEZ</strong> - Dividendo por Colombia<br/>
                        <strong>MAURICIO OLIVERA</strong> - Viceministro de Empleo y Pensiones<br/>
				        Tema: ¿Cómo se esta liderando la ciudad para ser sostenible?.</td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "bc13584f-e73a-4c09-8f15-99fa0d60439f" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">9:30am-10:30am</td>
                    <td style="vertical-align:middle">Conferencia 2.<br />
                        <strong>ALEJANDRO EDER</strong> - Director General de la Agencia Colombiana de la Reinserción ACR<br />
                        <strong>JUAN FELIPE MONTOYA</strong> - Vicepresidente de RRHH - Grupo éxito Comité de Cafeteros<br />
                        Foro "Perspectivas de inclusión laboral en población vulnerable, una propuesta a la paz"
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "3dc4d863-3b51-46bd-a45b-5b76cf55bd65" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">10:30am-11:30am</td>
                    <td style="vertical-align:middle">Refrigerio - Visita Muestra Comercial.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">11:00am-12:30am</td>
                    <td style="vertical-align:middle">
                        Conferencia 3<br />
                        <strong>OCTAVIO AGUILAR - MÉXICO</strong> - Accountability Sustainability<br />
                        Colombia analizada desde afuera
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "e92a28e0-2b36-44dd-88b6-73f2606d753c" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">2:00pm-2:45pm</td>
                    <td style="vertical-align:middle">
                        Conferencia 4<br />
                        <strong>MANUEL RAMIRO MUÑOZ</strong> - Director Centro de Estudios Interculturales y Presidente de Gremios Azucareros<br />
                        La sostenibilidad clave de la competitividad en los nuevos mercados globales
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "bff88d58-bfbd-4939-af8a-83d1a14b13cb" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">2:45pm-3:45pm</td>
                    <td style="vertical-align:middle">
                        Conferencia 5<br />
                        <strong>MANUEL RAMIRO MUÑOZ</strong> - Director Centro de Estudios Interculturales y Presidente de Gremios Azucareros<br />
                        <strong>TYRONE ZIACHOQUE</strong> - Gerente Gestión humana del sector Agro Industial del Grupo Empresarial Ardila Lule<br />
                        <strong>DIEGO SALAZAR</strong> - Gerente Corporativo de Gestión Humana de Manuelita SA<br />
                        El sector privado frente a la Sostenibilidad    
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "16cc836b-4e0c-4598-a792-ba1a1d1d9b12" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">3:45pm-4:15pm</td>
                    <td style="vertical-align:middle">Premio Orden al Mérito.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">4:15pm-4:45pm</td>
                    <td style="vertical-align:middle">Refrigerio - Visita Muestra Comercial.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">4:45pm-6:15pm</td>
                    <td style="vertical-align:middle">Conferencia 6<br />
                        <strong>LEANDRO HERRERO</strong> - Conferencista Inglés<br />
                        Viral Change and Human Resources    
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "51c0c5a7-0a76-41ca-99f1-8b694a6a88bf" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">6:15pm-8:00pm</td>
                    <td style="vertical-align:middle">Conferencia 7<br />
                        <strong>Fenix "La Magia de lo Etéreo"</strong><br />
                        Cóctel - SANFORD    
                    </td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "d09f1e2a-e177-4cac-8d8c-e78036b58fcb" }, "Evaluar", "Evaluar", "50px")%>
                    </td>
                </tr>
                </tbody>
            </table>
	    </div>
        <h3><a href="#">Día 2: 06/09/2013</a></h3>
	    <div style="height: 230px">
            <table id="table3" class="display tabla">
                 <thead>
                    <tr> 
                         <th>Hora</th>
                         <th>Evento</th>
                         <th></th>
                    </tr>
                 </thead>  
                <tbody>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">8:00am-9:00am</td>
                        <td style="vertical-align:middle">Conferencia 8<br />
                            <strong>MIGUEL DÍAZ</strong><br />   
                            Gestión Humana Sostenible y Globalización   
                        </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "36cb1663-aadf-48ad-87bc-47bcbe019735" }, "Evaluar", "Evaluar", "50px")%>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">9:00am-9:45am</td>
                        <td style="vertical-align:middle">Conferencia 9<br />
                        <strong>ANDRÉS LÓPEZ</strong> - Director Postrado Gerencia del Medio ambiente - Universidad Icesi<br />
                        La importancia de la Gestión Humana en la Sostenibilidad
                        </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "6dd8af12-7bbe-4f9e-84c2-6c0cb7d02026" }, "Evaluar", "Evaluar", "50px")%>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">9:45am-10:15am</td>
                        <td style="vertical-align:middle">Refrigerio - Visita Muestra Comercial.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td>
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">10:15am-11:00am</td>
                        <td style="vertical-align:middle">Conferencia 10
                            <strong>PEDDRO FELIPE CARVAJAL</strong><br />
                            Reto GH: "Aportar a la sostenibilidad con talento humano calfocado y motivado"
                        </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "614f3ae0-33f6-4cdf-9db8-2c8171d91123" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">11:00am-11:30am/td>
                        <td style="vertical-align:middle">Cementos Argos / Imbera Friomix El Cauca.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">11:30am-12:00pm</td>
                        <td style="vertical-align:middle">Conferencia 11<br />
                        <strong>ALFREDO MARÍN</strong> - Director del Departamento Ténico y Ambiental<br />
                        "Proteger al Medio Ambiente también es Negocio" Caso Carton de Colombia  
                    </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "134bf156-d5ea-411f-9c45-bea066e78279" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>                    
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00pm-3:00pm</td>
                        <td style="vertical-align:middle">Conferencia 12<br />
                            <strong>NANCY VALERO</strong><br />
                            Fundacíon Mujeres de Éxito. El aporte de las mujeres de éxio al País<br />
                        </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "58d857aa-d927-4af3-9ede-360f649235f5" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>                    
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">3:00pm-3:40pm</td>
                        <td style="vertical-align:middle">Conferencia 13<br />
                            <strong>JORGE CAGIGAS</strong> - España<br />
                            La nueva era: El talentismo, clave de la nueva competitividad eje principal en la sostenibilidad    
                        </td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "9e1d42cf-892d-474c-97cc-296d0d352abc" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr> 
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">3:40pm-4:30pm</td>
                        <td style="vertical-align:middle">Transmisión en Vivo de partido de la Selección de Colombia.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td>
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">4:30pm-5:00pm</td>
                        <td style="vertical-align:middle">Refrigerio - Visita Muestra Comercial.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td>
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">5:00pm-6:00pm</td>
                        <td style="vertical-align:middle">Transmisión en Vivo de partido de la Selección de Colombia.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td>
                    </tr>                  
                </tbody>
            </table>
	    </div>
        <h3><a href="#">Final</a></h3>
	    <div>
            <table id="table4" class="display tabla">
                 <thead>
                    <tr> 
                         <th></th>
                         <th>Evento</th>
                         <th></th>
                    </tr>
                 </thead>  
                <tbody>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                    <td style="vertical-align:middle">Evaluación general del evento.</td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "ccbad0e8-804e-4f66-bd8d-0af6917d2e5a" }, "Evaluar", "Evaluar", "50px")%>
                    </td> 
                </tr>
                </tbody>
            </table>
	    </div>
    </div>
    <br />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('.tabla').dataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "bJQueryUI": true,
            "bRetrieve": true,
            "bSort": false,
            "oLanguage": {
                "sZeroRecords": " "
            }
        });
        $(".myaccordion").accordion({
            collapsible: true,
            heightStyle: "content",
            autoHeight: false
        });
    });  
</script>