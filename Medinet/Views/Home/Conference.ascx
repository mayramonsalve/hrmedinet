<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Medinet.Models.Helpers" %>

<div class="prepend-1 span-22 append-1">
    <h3 style="font-size:medium">Cronograma de participación.</h3>
    <br />
    <div id="accordion" class="myaccordion">
        <h3><a href="#">Día 1: 17/07/2013</a></h3>
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
                    <td class="alignCenter" style="vertical-align:middle">3:00 - 4:30</td>
                    <td style="vertical-align:middle">Registro.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">4:30 - 5:00</td>
                    <td style="vertical-align:middle">Ceremonia de apertura.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">5:00 - 6:15</td>
                    <td style="vertical-align:middle">Conferencia de apertura.<br/>
				        Neurociencia en el mundo del Management; como nuestro cerebro puede ayudarnos a ser mejores líderes.</td>
                    <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "bc13584f-e73a-4c09-8f15-99fa0d60439f" }, "Evaluar", "Evaluar", "50px")%>
                </tr>
                <tr>
                    <td class="alignCenter" style="vertical-align:middle">6:15 - 8:00</td>
                    <td style="vertical-align:middle">Coctel de Apertura / Inauguración de la Expo feria.</td>
                    <td class="alignCenter" style="vertical-align:middle"></td>
                </tr>
                </tbody>
            </table>
	    </div>
        <h3><a href="#">Día 2: 18/07/2013</a></h3>
	    <div style="height: 230px">
            <table id="table2" class="display tabla">
                 <thead>
                    <tr> 
                         <th>Hora</th>
                         <th>Evento</th>
                         <th></th>
                    </tr>
                 </thead>  
                <tbody>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">7:00 - 8:00</td>
                        <td style="vertical-align:middle">Registro.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">8:00 - 9:00</td>
                        <td style="vertical-align:middle">Conferencia 2.<br/>
	    			        Líderes Grandiosos: Cómo liberar el Talento del capital humano en el nuevo contexto global.<br/>
				            Por José Gabriel "Pepe" Miralles -  CEO Franklin Covey Latam.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "3dc4d863-3b51-46bd-a45b-5b76cf55bd65" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">9:00 - 10:00</td>
                        <td style="vertical-align:middle">Conferencia 3.<br/>
	    			        Recursos Humanos Comprometido o Involucrado en los Procesos de Cambio.<br/>
				            Por Raquel Zambrano - Socio Director Henka Change Mangement Consulting.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                        <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "e92a28e0-2b36-44dd-88b6-73f2606d753c" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">10:00 - 10:45</td>
                        <td style="vertical-align:middle">Receso y Visita a la Expo Feria.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">10:45 - 12:00</td>
                        <td style="vertical-align:middle">Panel 1.<br/>
				            Desafíos de CEOs de Latinoamérica Lo que todo Gestor de Talento Humano debe saber de su CEO.<br/>                                 
                            Moderador: Eladio Uribe - Vicepresidente de Gestión Humana de Grupo Industrial Romana de República Dominicana.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "bff88d58-bfbd-4939-af8a-83d1a14b13cb" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">12:00 - 12:30</td>
                        <td style="vertical-align:middle">Anuncio Especial.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">12:30 - 1:30</td>
                        <td style="vertical-align:middle">Almuerzo.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">1:30 - 2:00</td>
                        <td style="vertical-align:middle">Visita a la Expo.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00 - 3:00</td>
                        <td style="vertical-align:middle">Conferencia 4.<br/>
				            Talento Verde: Desarrollando las Competencias Ambientales.<br/>
                            Por Ángela María Salazar - Directora Green Citizen foundation.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "16cc836b-4e0c-4598-a792-ba1a1d1d9b12" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00 - 5:00</td>
                        <td style="vertical-align:middle">Experiencias Virtuales de Aprendizaje Acelera.<br/>
                            Por Simi Benhamu – Ateneo.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00 - 5:00</td>
                        <td style="vertical-align:middle">Pensamiento Global y Visionario para la solución de 	problemas.<br/>
                            Por María Socorro.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "51c0c5a7-0a76-41ca-99f1-8b694a6a88bf" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00 - 5:00</td>
                        <td style="vertical-align:middle">Las 12 mejores prácticas de los Administradores de Capacitación.<br/>
                            Por Jan Reis – Best Training.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "d09f1e2a-e177-4cac-8d8c-e78036b58fcb" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">3:00 - 4:00</td>
                        <td style="vertical-align:middle">Conferencia 5.<br/>
				            Cómo crear una Cultura Emprendedora: apelando a las nuevas generaciones.<br/>
                            Por Julio Zelaya - Director Ejecutivo de The Learning Group.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "36cb1663-aadf-48ad-87bc-47bcbe019735" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">4:00 - 4:30</td>
                        <td style="vertical-align:middle">Receso y Visita a la Expo.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">4:30 - 5:30</td>
                        <td style="vertical-align:middle">Conferencia 6.<br/>
				            La Cultura y el Talento como ventaja Competitivas.<br/>
                            Por Claudia Valverde – Great Place to Work.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "6dd8af12-7bbe-4f9e-84c2-6c0cb7d02026" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">6:00 - 7:00</td>
                        <td style="vertical-align:middle">Traslado a la Cena de Gala.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">7:30 - 10:00</td>
                        <td style="vertical-align:middle">Cena de Gala / Entrega del Premio Oscar Alvear Urrutia.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                </tbody>
            </table>
	    </div>
        <h3><a href="#">Día 3: 19/07/2013</a></h3>
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
                        <td class="alignCenter" style="vertical-align:middle">7:00 - 8:00</td>
                        <td style="vertical-align:middle">Registro.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">8:00 - 9:00</td>
                        <td style="vertical-align:middle">Conferencia 7.<br/>
				            Creando el Talento Humano.<br/>
				            Por Alfredo Diez - ADEN Business School.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "614f3ae0-33f6-4cdf-9db8-2c8171d91123" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">9:00 - 10:00</td>
                        <td style="vertical-align:middle">Conferencia 8.<br/>
				            La Neurociencia del Liderazgo: Como crear Mejores Líderes en sus organizaciones.<br/>
                            Por Ciro Alejandro Pérez - Socio Director Change Americas.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "9e1d42cf-892d-474c-97cc-296d0d352abc" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">10:45 - 12:00</td>
                        <td style="vertical-align:middle">Panel 2.<br/>
				            VP de Recursos Humanos: Tendencias de Recursos Humanos en la Región.<br/>
                            Moderador: Jaime Bocanegra - Gerente de Consultoria de PWC.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "134bf156-d5ea-411f-9c45-bea066e78279" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">12:00 - 1:30</td>
                        <td style="vertical-align:middle">Almuerzo y Visita a la Expo.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">1:30 - 2:00</td>
                        <td style="vertical-align:middle">Anuncio Especial.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">2:00 - 3:00</td>
                        <td style="vertical-align:middle">Conferencia 9.<br/>TRASCENDIENDO PARADIGMAS.<br/>
				            Del desarrollo a la retención de Talento, de la Mejora Continua a una Cultura de Coaching como una estrategia Organizacional.<br/>
                            Por Thomas Köttner - Coach Ready.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "58d857aa-d927-4af3-9ede-360f649235f5" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">3:00 - 4:30</td>
                        <td style="vertical-align:middle">Conferencia de Clausura.<br/>
                            Trabajo y Felicidad, el Sentido de lo que Hacemos.<br/>
                            Ricardo Gómez.</td>
                        <td class="alignCenter" style="vertical-align:middle">
                            <%= Html.ImageActionLink("Content/Images/icon-system/survey.png", "TestInstructions", "Evaluations", new { code = "1d3cb876-186e-42be-af29-c8861791095a" }, "Evaluar", "Evaluar", "50px")%>
                        </td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">4:30 - 5:00</td>
                        <td style="vertical-align:middle">Palabras de Clausura.</td>
                        <td class="alignCenter" style="vertical-align:middle"></td> 
                    </tr>
                    <tr>
                        <td class="alignCenter" style="vertical-align:middle">5:00 - 7:00</td>
                        <td style="vertical-align:middle">Tarde de Coctel, Quesos y Vinos.</td>
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