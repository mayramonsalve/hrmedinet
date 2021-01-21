<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Medinet.Models.ViewModels.ChartReportViewModel>" %>

<% string demographic = ViewData["option"].ToString();
   string source="", type ="";
   int id = 1, order = 1, country_id = 0 ;

   if (Model.chartType.CompareTo("EditUnivariate") == 0)
       type = "Univariate";
   else 
       type = Model.chartType;
   for (int i = 0; i < Model.graphics.Length; i++) {
       if (Model.graphics[i].Demographic.CompareTo(demographic) == 0 && Model.graphics[i].Type.CompareTo(type)==0) {
                id = Model.graphics[i].Id;
                order = Model.graphics[i].Order;
                source = Model.graphics[i].Source.ToString();            
       }
   }

       if (Model.chartType.CompareTo("Population") == 0)
       { %>

        <h2><%:Model.details[order].Title%></h2>
        <img src="<%:source%>" alt="Grafico" />
   <%}
       else if (Model.chartType.CompareTo("Univariate") == 0 || Model.chartType.CompareTo("EditUnivariate") == 0)
       { %> 
       <table>
            <tr>
                <td>
                    <h2><%:Model.details[order].Title%></h2>
                    <%: Html.DropDownList(demographic + "GroupByCategoriesDDL", Model.GroupByCategories, ViewRes.Scripts.Shared.ShowAll)%> 
                    
                </td>
            </tr>
             <tr>
                <td>
                   <img id="<%:demographic%>Chart" name="<%:demographic%>Chart" src="<%:source%>&graphic_id=<%:id%>&test_id=<%:Model.test.Id%>" alt="Grafico" />
                </td>
                <% if (Model.chartType.CompareTo("Univariate") == 0)
                   { %>
                     <td><%: ViewRes.Views.ChartReport.Graphics.Comments %> <%:Html.Label(demographic + "Comments", Model.details[order].Text)%></td>
                  <%}
                   else
                   { %>
                  <td>
                    <table>
                        <tr>     
                            <td><%: ViewRes.Views.ChartReport.Graphics.TableTitle %></td> <td><%: Html.TextBox(demographic + "Title", Model.details[order].Title)%></td>  
                        </tr>
                         <tr>     
                            <td><%: ViewRes.Views.ChartReport.Graphics.AxisX %></td> <td><%: Html.TextBox(demographic + "AxisX", Model.details[order].AxisXName)%></td> 
                        </tr>
                         <tr>     
                             <td><%: ViewRes.Views.ChartReport.Graphics.AxisY %></td> <td> <%: Html.TextBox(demographic + "AxisY", Model.details[order].AxisYName)%></td>  
                        </tr>
                         <tr>     
                            <td><%: ViewRes.Views.ChartReport.Graphics.Comments %></td> <td> <%:Html.TextArea(demographic + "Comments", Model.details[Model.graphics[id].Order].Text)%></td>
                        </tr>
                    </table>
                </td>
                 <%
                   } %>
            </tr>
        </table>
   <%}%>