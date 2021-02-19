using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.CustomClasses;
using System.Web.UI.DataVisualization.Charting;
using Medinet.Models.ViewModels;
using System.IO;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.AuthorizationModels;
using System.Reflection;
using MedinetClassLibrary.Classes;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Collections;
using System.Data;
using evointernal;
using Rotativa;

namespace Medinet.Controllers
{
    //    [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
    public class ChartReportsController : Controller
    {
        private int series;
        private bool tresD;
        private ChartsServices _chartService;
        private ChartReportViewModel _chartViewModel;
        private BivariateCharts _bivariateCharts;
        private string[] google_colors;
        private string[] google_colors_gender;

        public ChartReportsController()
        {
            _chartService = new ChartsServices();
            _bivariateCharts = new BivariateCharts();
            this.series = 1;
            this.tresD = true;
            this.google_colors = new string[] { "#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
                                                "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
                                                "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
                                                "#fd151b", "#FF69B4", "#00BFFF" };
            this.google_colors_gender = new string[] { "#FF69B4", "#00BFFF" };
        }

        public ChartReportsController(ChartsServices _chartService, ChartReportViewModel _chartViewModel)
        {
            this._chartService = _chartService;
            this._chartViewModel = _chartViewModel;
            this.tresD = true;
            this.google_colors = new string[] { "#00a0e3", "#ffce00", "#00b386", "#ff044d", "#664147", "#084c61", "#3c1642",
                                                "#1a181b", "#b4656f", "#f9b9f2", "#102542", "#2e0219", "#f8333c", "#a62639",
                                                "#065143", "#d9dbf1", "#af2bbf", "#841c26", "#aaaaaa", "#d30c7b", "#01295f",
                                                "#fd151b", "#FF69B4", "#00BFFF" };
            this.google_colors_gender = new string[] { "#FF69B4", "#00BFFF" };
        }

        private bool GetAuthorization(int test_id)
        {
            return new SharedHrAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                new CompaniesServices().GetById(new TestsServices().GetById(test_id).Company_Id), false).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        private string[] SetUnivariateTitle(string title, GraphicDetail detail)
        {
            string[] Title = new string[4];
            Title[0] = title;
            Title[1] = detail.AxisXName;
            Title[2] = detail.AxisYName;
            Title[3] = "";
            return Title;
        }

        #region Generar archivos

        public void ImprimirWord(FormCollection collection)
        {
            string html = "";// ObtenerHtml(collection);
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }

        public void ImprimirExcel(string report, int test_id)
        {
            string html = GetHtml(report, test_id);
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=Reporte.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }

        public void ImprimirPdf(string report, int test_id)//FormCollection collection)
        {
            //string html = GetHtml(report, test_id);
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 10f, 10f);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //StringReader sr = new StringReader(html);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            AddHeader(pdfDoc, test_id);
            Paragraph para = new Paragraph("Texto", new Font(Font.FontFamily.HELVETICA, 8f));
            para.SpacingAfter = 9f;
            para.Alignment = Element.ALIGN_JUSTIFIED;
            Image chart = Image.GetInstance(GeneralPercentageChartByte("Tooltip", "Pie", test_id, 1, true));
            PdfPTable table = new PdfPTable(2);
            PdfPCell cellChart = new PdfPCell(chart);
            cellChart.Border = 0;
            PdfPCell cellText = new PdfPCell();
            cellText.AddElement(para);
            cellText.Border = 0;
            float[] widths = new float[] { 2f, 1f };
            table.SetWidths(widths);
            table.AddCell(cellChart);
            table.AddCell(cellText);
            pdfDoc.Add(table);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        private void AddHeader(Document pdfDoc, int test_id)
        {
            Test test = new TestsServices().GetById(test_id);
            string imagepath = Server.MapPath("");
            imagepath = imagepath.Replace("ChartReports", "Content\\Images\\Companies\\");
            string imageCompany = (test.Company.Image != "" && test.Company.Image != null) ? test.Company.Image :
                                    test.Company.CompaniesType.Name + "Image.png";
            string routeCompany = imagepath + imageCompany;
            Image pngCompany = Image.GetInstance(routeCompany);
            string imageAssociated = (test.Company.CompanyAssociated.Image != "" && test.Company.CompanyAssociated.Image != null) ?
                                        test.Company.Image : test.Company.CompanyAssociated.CompaniesType.Name + "Image.png";
            string routeAssociated = imagepath + imageCompany;
            Image pngAssociated = Image.GetInstance(routeCompany);
            pngCompany.Alignment = Element.ALIGN_LEFT | Image.UNDERLYING;
            pngAssociated.Alignment = Element.ALIGN_RIGHT;// | Image.TEXTWRAP;
            pdfDoc.Add(pngCompany);
            pdfDoc.Add(pngAssociated);
            Font font = new Font(Font.FontFamily.COURIER, 15f, Font.NORMAL);
            Chunk title = new Chunk(ViewRes.Views.ChartReport.Graphics.PrintPopulation, font);
            Paragraph paragraph = new Paragraph();
            paragraph.Add(title);
            paragraph.SetAlignment("Center");
            paragraph.SpacingAfter = 30f;
            pdfDoc.Add(paragraph);

        }

        private string GetHtml(string report, int test_id)
        {
            //IniciarVistaReporte(Int32.Parse(collection["Reporte"].ToString()), collection);
            GetViewModelByReport(report, test_id);
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, "PopulationPrint", "");
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        private void GetViewModelByReport(string report, int test_id)
        {
            switch (report)
            {
                case "Population":
                    this.InitializeViews("Population", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Univariate":
                    this.InitializeViews("Univariate", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Bivariate":
                    this.InitializeViews("Bivariate", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Category":
                    this.InitializeViews("Category", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Frequency":
                    this.InitializeViews("Frequency", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "TextAnswers":
                    this.InitializeViews("TextAnswers", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Result":
                    this.InitializeViews("Result", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Analytical":
                    this.InitializeViews("Analytical", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
                case "Ranking":
                    this.InitializeViews("Ranking", "", null, test_id, null);
                    ViewData.Model = _chartViewModel;
                    break;
            }
        }

        public Byte[] GeneralPercentageChartByte(string chartSize, string chartType, int test_id, int graphic_id, bool? print)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "General");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("General", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "General",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, print.HasValue ? print.Value : false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return ms.GetBuffer();
        }

        private void AddData(Document doc, string html)
        {
            StringReader sr = new StringReader(html);
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);
            var arrayList = HTMLWorker.ParseToList(reader, null);
            List<IElement> htmlarraylist = new List<IElement>();
            foreach (object array in arrayList)
            {
                doc.Add(new Paragraph(" "));
                Type type = array.GetType();
                if (type.Name == "Paragraph")
                {
                    Paragraph p = (Paragraph)array;
                    Font font = FontFactory.GetFont("Helvetica", 16, Font.BOLDITALIC, new BaseColor(125, 88, 15));
                    doc.Add((Paragraph)array);
                }
                else
                    doc.Add((IElement)array);
            }
        }

        #endregion

        #region Views
        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager, FreeReports")]
        public int GetTestWithNoEvaluations(int test_id, bool final, int? compare_id)
        {
            TestsServices testService = new TestsServices();
            Test test = testService.GetById(test_id);
            if (test.Evaluations.Count > 0)
            {
                if (final)
                {
                    return (test.CurrentEvaluations >= test.MinimumPeople ? 0 : test_id);
                }
                else
                {
                    if (compare_id.HasValue)
                    {
                        if (testService.GetById(compare_id.Value).Evaluations.Count > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return compare_id.Value;
                        }
                    }
                    else
                        return 0;
                }
            }
            else
                return test_id;
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager, FreeReports")]
        public ActionResult NoEvaluations(int id_test)
        {
            _chartViewModel = new ChartReportViewModel(new TestsServices().GetById(id_test).Name);
            return View(_chartViewModel);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult BiVariateGraphics(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Bivariate", "", null, test_id, null);
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult UniVariateGraphics(int test_id, int? compare_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Univariate", "", null, test_id, compare_id);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult FrequencyGraphics(int test_id)
        {
            if (GetAuthorization(test_id))//ve los reportes de su compañia si es HRCompany o de sus compañias clientes si es HRAdministrator
            {
                this.InitializeViews("Frequency", "", null, test_id, null);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult CategoryGraphics(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Category", "", null, test_id, null);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult TextAnswerReports(int test_id, int? compare_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("TextAnswers", "", null, test_id, compare_id);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult EditResults(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("EditUnivariate", "", null, test_id, null);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager, FreeReports")]
        public ActionResult PopulationGraphics(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Population", "", null, test_id, null);
                this.series = 1;
                this.tresD = true;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult ComparativeGraphics(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Comparative", "", null, test_id, null);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult SatisfactionTables(int test_id)
        {
            if (GetAuthorization(test_id))
            {
                this.InitializeViews("Satisfaction", "Category", null, test_id, null);
                this.series = 2;
                this.tresD = false;
                return View(_chartViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager, FreeReports")]
        public ActionResult ReportsList()
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            SelectList testsList = new SelectList(new TestsServices().GetTestsByUserForDropDownList(user, true), "Key", "Value");
            SelectList testsSeveralQuestionnairesList = new SelectList(new TestsServices().GetTestsByUserForDropDownList(user, false), "Key", "Value");
            SelectList questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesByUCompanyForRanking(user.Company, user.Role.Name == "HRAdministrator"), "Key", "Value");
            return View(new ChartReportViewModel(testsList, testsSeveralQuestionnairesList, questionnairesList));
        }
        #endregion Views

        #region BiVariateCharts

        [HttpPost] // Actualiza el 2do DropdownList para seleccionar los demográficos bivariables
        public JsonResult UpdateDropDownListBivariateChart(string demographic1_value, int test_id, string demographic1_key)
        {
            List<string> ubicationList = _chartService.GetUbicationKeys();
            Test test = new TestsServices().GetById(test_id);
            Dictionary<string, string> dictionary = _chartService.DemographicsDropDownList(test);
            if (ubicationList.Contains(demographic1_key))
            {
                foreach (string ubication in ubicationList)
                {
                    dictionary.Remove(ubication);
                }
            }
            else if (demographic1_key.Contains("FunctionalOrganizationType-"))
            {
                foreach (int key in new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id).Keys)
                {
                    dictionary.Remove("FunctionalOrganizationType-" + key);
                }
            }
            else
            {
                dictionary.Remove(demographic1_key);
            }
            List<object> demographics = new List<object>();
            foreach (var demographic in dictionary)
            {
                demographics.Add(
                   new
                   {
                       optionValue = demographic.Key,
                       optionDisplay = demographic.Value
                   });
            }
            return Json(demographics);
        }

        public FileResult SelectBivariateGraphic(int test_id, string demographic_1, string demographic_2)
        {
            BivariateCharts _bivariateCharts = new BivariateCharts();//es donde estan todos los llamados a las funciones
            MemoryStream ms = (MemoryStream)_bivariateCharts.SelectBivariateGraphic(test_id, demographic_1, demographic_2, true);
            return File(ms.GetBuffer(), @"image/png");
            //return (FileResult)_bivariateCharts.SelectBivariateGraphic(test_id, demographic_1, demographic_2, true);//devuelve un FileResult que es una imagen
        }

        public JsonResult SelectBivariateTable(int test_id, string demographic_1, string demographic_2)//busca la tabla
        {
            object[] table = GetDataForTable(test_id, demographic_1, demographic_2);
            return Json(table);
        }

        private object[] GetDataForTable(int test_id, string demographic_1, string demographic_2)
        {
            _chartViewModel = new ChartReportViewModel(test_id);
            Test test = new TestsServices().GetById(test_id);
            BivariateCharts _bivariateCharts = new BivariateCharts();
            Dictionary<string, string> demoNames = _chartViewModel.demographicNames;
            int? fot = demographic_1.Contains('-') ? Int32.Parse(demographic_1.Split('-')[1]) : (demographic_2.Contains('-') ? Int32.Parse(demographic_2.Split('-')[1]) : 0);
            if (fot == 0) fot = null;
            string[] demographics = _chartViewModel.GetOrderedDemographic(demographic_1, demographic_2, fot);//obtener los demográficos ordenados,como sabemos que es tabla sabemos q uno de los dos demográficos tiene muchos registros.GetOrderedDemographic esta función me ordena los valores dependiendo de cual de los demográficos es el que tiene mayor número de elementos, es decir el que tenga más elementos va de manera vertical, y me dice cual demográfico va en horizontal y cual vertical
            List<object> aux = (List<object>)_bivariateCharts.SelectBivariateGraphic(test_id, demographics[0], demographics[1], false);
            Dictionary<string, object> stringObject = (Dictionary<string, object>)aux.First();//
            List<string> demos2 = new List<string>();
            foreach (KeyValuePair<string, object> strObj in stringObject)
            {
                demos2.AddRange(((Dictionary<string, double>)strObj.Value).Keys);
            }
            demos2 = demos2.Distinct().OrderBy(d => d).ToList();

            object[] table = new object[10];
            table[0] = demoNames[demographics[0]];//nombre de mi primer demográfico dependiendo si es en español o en inglés, ejemplo:country se guarda paises
            table[1] = stringObject.Count;//cuantos demográficos hay de ese primer demográfico. ejemplo:si es paises mi primer demográfico, se cuentan cuantos paises hay
            table[2] = demoNames[demographics[1]];//nombre del segundo demográfico
            table[3] = demos2.Count;//cantidad del segundo demográfico
            table[4] = stringObject;
            table[5] = test.GetOptionsByTest().Count();//valores de las opciones, hasta donde llega
            table[6] = aux.Last().ToString();//toma el titulo y lo envia
            table[7] = test.Name;//nombre de la medición
            table[8] = (test.ResultBasedOn100) ? "1" : "0";//indica si el test es en base 100 para cambiar los valores
            table[9] = demos2;
            return table;
        }

        private object[] GetDataForGoogleChart(int test_id, string demographic_1, string demographic_2)
        {
            _chartViewModel = new ChartReportViewModel(test_id);
            Test test = new TestsServices().GetById(test_id);
            BivariateCharts _bivariateCharts = new BivariateCharts();
            Dictionary<string, string> demoNames = _chartViewModel.demographicNames;
            int? fot = demographic_1.Contains('-') ? Int32.Parse(demographic_1.Split('-')[1]) : (demographic_2.Contains('-') ? Int32.Parse(demographic_2.Split('-')[1]) : 0);
            if (fot == 0) fot = null;
            string[] demographics = _chartViewModel.GetOrderedDemographic(demographic_1, demographic_2, fot);//obtener los demográficos ordenados,como sabemos que es tabla sabemos q uno de los dos demográficos tiene muchos registros.GetOrderedDemographic esta función me ordena los valores dependiendo de cual de los demográficos es el que tiene mayor número de elementos, es decir el que tenga más elementos va de manera vertical, y me dice cual demográfico va en horizontal y cual vertical
            List<object> aux = (List<object>)_bivariateCharts.SelectBivariateGraphic(test_id, demographics[0], demographics[1], false);
            Dictionary<string, object> stringObject = (Dictionary<string, object>)aux.First();
            List<string> demos2 = new List<string>();
            foreach (KeyValuePair<string, object> strObj in stringObject)
            {
                demos2.AddRange(((Dictionary<string, double>)strObj.Value).Keys);
            }
            int optionsTest = test.GetOptionsByTest().Count();
            demos2 = demos2.Distinct().OrderBy(d => d).ToList();
            aux.Add(demos2);
            aux.Add(demoNames[demographics[0]]);
            aux.Add(demoNames[demographics[1]]);
            aux.Add(test.ResultBasedOn100 ? 100 : optionsTest);
            aux.Add(test.ResultBasedOn100);
            aux.Add(optionsTest);
            return aux.ToArray();
        }

        public void GetDemographicTable(string demographic, int test_id)
        {
            DemographicsTables demographicTable = new DemographicsTables(test_id);
            demographicTable.SelectDemographicTable(demographic);
        }

        [HttpPost]
        public bool GetIsTable(int test_id, string demo1, string demo2)//esta función nos dice si es una tabla o no, si devuelve true es porque en efecto es una tabla
        {//si el segundo dropdown tiene mas demográficos que el primero,lo ordena y lo pone de primero para que la tabla no salga alargada horizontalmente
            _chartViewModel = new ChartReportViewModel(test_id);
            int? fot = demo1.Contains('-') ? Int32.Parse(demo1.Split('-')[1]) : (demo2.Contains('-') ? Int32.Parse(demo2.Split('-')[1]) : 0);
            if (fot == 0) fot = null;
            return _chartViewModel.IsTable(demo1, fot) || _chartViewModel.IsTable(demo2, fot);//si alguno de los dos es tabla manda true
        }

        #endregion

        #region PrintPDF
        [HttpGet]
        public PdfResult GetMyGraphics(int id)
        {
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintResultsReport?id=" + id, "ResultReport", false);
        }

        [HttpGet]
        public PdfResult GetMeOtherGraphics(int id)
        {
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintResultsReport?id=" + id, "Test", true);
        }

        public ActionResult PrintResultsReport(int id)
        {
            _chartViewModel.chartType = "Population";
            _chartViewModel.test = new TestsServices().GetById(id);
            this.series = 1;
            return View(_chartViewModel);
        }
        #endregion PrintPDF

        #region AddDetails
        [HttpPost]
        [ValidateInput(false)]
        public int SaveComments(int test_id, int graphic_id, string title, string xaxis, string yaxis, string comments)
        {
            GraphicsDetailsServices _graphicDetailService = new GraphicsDetailsServices();
            if (_graphicDetailService.GetDetailsByGraphic(graphic_id, test_id) != null)
            {
                return EditGraphicDetails(test_id, graphic_id, title, xaxis, yaxis, comments, _graphicDetailService);
            }
            else
            {
                return CreateGraphicDetail(test_id, graphic_id, title, xaxis, yaxis, comments, _graphicDetailService);
            }
        }

        private int CreateGraphicDetail(int test_id, int graphic_id, string title, string xaxis, string yaxis, string comments, GraphicsDetailsServices _graphicDetailService)
        {
            GraphicDetail details = new GraphicDetail();
            details.CreationDate = DateTime.Now;
            details.Graphic_Id = graphic_id;
            details.Test_Id = test_id;
            details.Text = comments;
            details.AxisXName = xaxis;
            details.AxisYName = yaxis;
            details.Title = title;
            details.User_Id = new UsersServices().GetByUserName(User.Identity.Name.ToString()).Id;
            if (_graphicDetailService.Add(details))
                return 1;
            else
                return 0;
        }

        private int EditGraphicDetails(int test_id, int graphic_id, string title, string xaxis, string yaxis, string comments, GraphicsDetailsServices _graphicDetailService)
        {
            GraphicDetail details = new GraphicDetail();
            try
            {
                details = _graphicDetailService.GetDetailsByGraphic(graphic_id, test_id);
                details.Text = comments;
                details.AxisXName = xaxis;
                details.AxisYName = yaxis;
                details.Title = title;
                UpdateModel(details, "GraphicDetail");
                _graphicDetailService.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        //public ActionResult SaveComments(FormCollection collection)
        //{
        //    int test_id = Int32.Parse(collection["test_id"]);
        //    AddDetails(collection, "AgeRange", 2, test_id);
        //    AddDetails(collection, "Department", 4, test_id);
        //    AddDetails(collection, "Gender", 6, test_id);
        //    AddDetails(collection, "InstructionLevel", 8, test_id);
        //    AddDetails(collection, "Location", 10, test_id);
        //    AddDetails(collection, "PositionLevel", 12, test_id);
        //    AddDetails(collection, "Position", 14, test_id);
        //    AddDetails(collection, "Seniority", 16, test_id);
        //    AddDetails(collection, "Country", 18, test_id);
        //    AddDetails(collection, "Region", 20, test_id);
        //    AddDetails(collection, "Performance", 23, test_id);
        //    return RedirectToAction("/UnivariateGraphics?test_id=" + test_id);
        //}

        //public void AddDetails(FormCollection collection, string demographic, int graphic_id, int test_id)
        //{
        //    GraphicDetail details;
        //    var v = collection[demographic + "Comments"];
        //    if (collection[demographic + "Comments"] != null)
        //    {
        //        GraphicsDetailsServices _graphicDetailService = new GraphicsDetailsServices();
        //        if (_graphicDetailService.GetDetailsByGraphic(graphic_id, test_id) != null)
        //        {
        //            details = _graphicDetailService.GetDetailsByGraphic(graphic_id, test_id);
        //            details.Text = collection[demographic + "Comments"];
        //            details.AxisXName = collection[demographic + "AxisX"];
        //            details.AxisYName = collection[demographic + "AxisY"];
        //            details.Title = collection[demographic + "Title"];
        //            UpdateModel(details, "GraphicDetail");
        //            _graphicDetailService.SaveChanges();
        //        }
        //        else
        //        {
        //            details = new GraphicDetail();
        //            details.CreationDate = DateTime.Now;
        //            details.Graphic_Id = graphic_id;
        //            details.Test_Id = test_id;
        //            details.Text = collection[demographic + "Comments"];
        //            details.AxisXName = collection[demographic + "AxisX"];
        //            details.AxisYName = collection[demographic + "AxisY"];
        //            details.Title = collection[demographic + "Title"];
        //            details.User_Id = new UsersServices().GetByUserName(User.Identity.Name.ToString()).Id;
        //            _graphicDetailService.Add(details);
        //        }
        //    }
        //}

        public string[] GetChartDetails(int graphic_id, int test_id)
        {
            string[] details = new string[4];
            for (int i = 0; i < 4; i++)
            {
                details[i] = "";
            }
            GraphicDetail detailGraphic = new GraphicsDetailsServices().GetDetailsByGraphic(graphic_id, test_id);
            if (detailGraphic != null)
            {
                if (detailGraphic.Title != null)
                {
                    details[0] = detailGraphic.Title;
                }
                if (detailGraphic.AxisXName != null)
                {
                    details[1] = detailGraphic.AxisXName;
                }
                if (detailGraphic.AxisYName != null)
                {
                    details[2] = detailGraphic.AxisYName;
                }
            }
            return details;
        }
        #endregion AddDetails

        #region GoogleCharts

        public JsonResult GetPercentageChart(int test_id, string demographic, int? FO_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string FO_name = "";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            if (demographic == "FunctionalOrganizationType")
            {
                parameters.Add("fot", FO_id.Value);
                FO_name = new FunctionalOrganizationTypesServices().GetById(FO_id.Value).Name;
            }
            string title = GetTitle(demographic, "Population", FO_name, "");
            Dictionary<string, string> columns = new Dictionary<string, string>()
            {
                { "Label", "string" },
                { "Population", "number" }
            };
            Dictionary<string, double> data = new ChartsServices().GetGraphicDataForPopulation("Population", parameters);
            if (demographic == "General" || demographic == "Gender")
            {
                data = UpdatePopulationLegends(demographic, data);
            }
            object[] info = new object[] { columns, data, title };
            return Json(info);
        }

        public JsonResult GetUnivariateChart(int test_id, string demographic, int? FO_id,
                                            int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            Dictionary<string, object> dictionary;
            int fot = FO_id.HasValue ? (FO_id.Value == -1 ? test.Company.FunctionalOrganizationTypes.FirstOrDefault().Id : FO_id.Value) : 0;
            switch (demographic)
            {
                case "General":
                    dictionary = test.GetGeneralAvgAndMed(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Category":
                    dictionary = new Dictionary<string, object>();
                    break;
                case "AgeRange":
                    dictionary = test.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "PositionLevel":
                    dictionary = test.GetAvgAndMedByPositionLevels(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Seniority":
                    dictionary = test.GetAvgAndMedBySeniorities(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Country":
                    dictionary = test.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Gender":
                    dictionary = test.GetAvgAndMedByGender(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "InstructionLevel":
                    dictionary = test.GetAvgAndMedByInstructionLevels(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Performance":
                    dictionary = test.GetAvgAndMedByPerformanceEvaluations(questionnaire_id, category_id, question_id, compare.HasValue);
                    break;
                case "Region":
                    dictionary = new Dictionary<string, object>();
                    break;
                case "Location":
                    dictionary = test.GetAvgAndMedByLocations(questionnaire_id, category_id, question_id, compare.HasValue, null, null, null);
                    break;
                case "State":
                    dictionary = new Dictionary<string, object>();
                    break;
                case "FunctionalOrganizationType":
                    dictionary = test.GetAvgAndMedByFOTypes(questionnaire_id, category_id, question_id, fot, compare.HasValue);
                    break;
                case "AllTests":
                    dictionary = new Dictionary<string, object>();
                    break;
                default:
                    dictionary = new Dictionary<string, object>();
                    break;
            }

            //Dictionary<string, double> data = (Dictionary<string, double>)dictionary["Average"];
            dictionary.Remove("Median");
            string FO_name = "";
            if (demographic == "FunctionalOrganizationType")
            {
                FO_name = new FunctionalOrganizationTypesServices().GetById(fot).Name;
            }
            string title = GetTitle(demographic, "Univariate", FO_name, "");
            ArrayList columns = new ArrayList()
            {
                new string[] {"string", "Label" },
                new string[] { "number", ViewRes.Views.ChartReport.Graphics.Average }
            };
            int optionsTest = test.GetOptionsByTest().Count();
            int vAxisVal = test.ResultBasedOn100 ? 100 : optionsTest;
            Dictionary<decimal, string> scales = GetScales(test);
            DataTable table = dictionary.Count == 0 ? new DataTable() :
                            GetDataTableFromDictionary(dictionary, null, false, vAxisVal, optionsTest, test.ResultBasedOn100, scales);
            object[] info = new object[] { columns, table.AsEnumerable().Select(r => r.ItemArray), title, vAxisVal };
            return Json(info);
        }

        //Category
        public JsonResult GetCategoryChart(int test_id, string demographic, int? FO_id, int? questionnaire_id, int? id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string FO_name = "";
            demographic = id.HasValue ? demographic : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                if (demographic != "Gender")
                    parameters.Add("id", id.Value);
                else
                    parameters.Add("id", id.Value == 0 ? ViewRes.Views.Shared.Shared.Female : ViewRes.Views.Shared.Shared.Male);
            if (demographic == "FunctionalOrganizationType")
            {
                parameters.Add("fot", FO_id.Value);
                FO_name = new FunctionalOrganizationTypesServices().GetById(FO_id.Value).Name;
            }
            string title = GetTitle(demographic, "Category", FO_name, "");
            ArrayList columns = new ArrayList()
            {
                new string[] {"string", "Label" },
                new string[] { "number", ViewRes.Views.ChartReport.Graphics.Average }
            };
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            object value = dictionary[""]; dictionary.Remove(""); dictionary["Average"] = value;
            int optionsTest = test.GetOptionsByTest().Count();
            int vAxisVal = test.ResultBasedOn100 ? 100 : optionsTest;
            Dictionary<decimal, string> scales = GetScales(test);
            DataTable table = dictionary.Count == 0 ? new DataTable() :
                            GetDataTableFromDictionary(dictionary, null, false, vAxisVal, optionsTest, test.ResultBasedOn100, scales);
            object[] info = new object[] { columns, table.AsEnumerable().Select(r => r.ItemArray), title, vAxisVal };
            return Json(info);
        }

        //Comparative
        public JsonResult GetComparativeChart(int test_id, string demographic)
        {
            Test test = new TestsServices().GetById(test_id);
            DataTable table = new DataTable();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("company", test.Company_Id);
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string c_fo = ""; string auxdemo = "";
            ArrayList columns = new ArrayList()
            { new string[] {"string", "Label" } };
            int vAxisVal = 0;
            Dictionary<decimal, string> scales = null;
            if (demographic == "Population")
            {
                int options = test.Company.Tests.Select(v => v.EvaluationNumber).Max();
                parameters.Add("selector", "General");
                dictionary = new ChartsServices().GetGraphicDataForComparativeGraph("Comparative", parameters);
                columns.Add(new string[] { "number", demographic });
                table = dictionary.Count == 0 ? new DataTable() :
                            GetDataTableFromDictionary(dictionary, null, false, 0, 0, false, null);
            }
            else
            {
                DemographicsInTest dit = test.DemographicsInTests.Where(s => s.Selector).FirstOrDefault();
                int options = test.GetOptionsByTest().Select(v => v.Value).Max();
                parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "Climate");
                parameters.Add("selector", dit.Demographic.Name);
                if (dit.FOT_Id.HasValue)
                    parameters.Add("fot", dit.FOT_Id);
                parameters.Add("company", test.Company_Id);
                dictionary = new ChartsServices().GetGraphicDataForComparative("Comparative", parameters);
                foreach (string key in dictionary.Keys)
                    columns.Add(new string[] { "number", key });
                int optionsTest = test.GetOptionsByTest().Count();
                vAxisVal = test.ResultBasedOn100 ? 100 : optionsTest;
                scales = GetScales(test);
                table = dictionary.Count == 0 ? new DataTable() :
                            GetDataTableFromDictionary(dictionary, null, true, vAxisVal, optionsTest, test.ResultBasedOn100, scales);
                c_fo = dit.FOT_Id.HasValue ? dit.FunctionalOrganizationType.Name : "";
                auxdemo = dit.Demographic.Name;
            }
            Dictionary<string, double> data = demographic == "Population" ? (Dictionary<string, double>)dictionary["General"]
                                                                            : new Dictionary<string, double>();
            string title = GetTitle(demographic, "Comparative", c_fo, auxdemo);
            object[] info = new object[] { columns, table.AsEnumerable().Select(r => r.ItemArray), title };
            return Json(info);
        }

        //Bivariate
        public JsonResult GetBivariateChart(int test_id, string demographic_1, string demographic_2)
        {
            object[] all_info = GetDataForGoogleChart(test_id, demographic_1, demographic_2);
            Dictionary<string, object> dictionary = (Dictionary<string, object>)all_info[0];
            string title = all_info[1].ToString();
            ArrayList columns = new ArrayList()
            { new string[] {"string", "Label" } };
            foreach (string key in dictionary.Keys)
                columns.Add(new string[] { "number", key });
            DataTable table = dictionary.Count == 0 ? new DataTable() :
                            GetDataTableFromDictionary(dictionary, (List<string>)all_info[2], false, 0, Convert.ToInt32(all_info[7]), Convert.ToBoolean(all_info[6]), null);
            object[] info = new object[] { columns, table.AsEnumerable().Select(r => r.ItemArray), title, all_info[3], all_info[4], all_info[5] };
            return Json(info);
        }


        #endregion

        private DataTable GetDataTableFromDictionary(Dictionary<string, object> dictionary, List<string> first_col_keys, bool compare, int optMax, int optionsTest, bool ResultBasedOn100, Dictionary<decimal, string> scales)
        {
            bool certainty = true;
            DataTable table = new DataTable();
            table.Columns.Add("Label", typeof(string));
            foreach (string key in dictionary.Keys)
            {
                table.Columns.Add(key, typeof(float));
                table.Columns.Add("Color-" + key, typeof(string));
                table.Columns.Add("Annotation-" + key, typeof(string));
                table.Columns.Add("Certainty-" + key, typeof(bool));
            }
            List<string> internal_keys = first_col_keys != null ? first_col_keys
                                            : ((Dictionary<string, double>)dictionary.Values.FirstOrDefault()).Keys.ToList();
            foreach (string first_col in internal_keys)
            {
                DataRow row = table.NewRow();
                row["Label"] = first_col;
                foreach (KeyValuePair<string, object> dict in dictionary)
                {//num * 100 / optMax
                    string color = "";
                    Dictionary<string, double> internal_dict = (Dictionary<string, double>)dict.Value;
                    double value = internal_dict.Keys.Contains(first_col) ? (ResultBasedOn100 ? (internal_dict[first_col] * 100 / optionsTest) : internal_dict[first_col]) : 0;
                    row[dict.Key] = value;
                    if (scales != null)
                        color = GetValueColor(scales, color, value, optMax);
                    row["Color-" + dict.Key] = color;
                    row["Annotation-" + dict.Key] = value == 0 ? null : string.Format("{0:f2}", row[dict.Key]);
                    row["Certainty-" + dict.Key] = certainty;
                    if (compare)
                        certainty = !certainty;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private static string GetValueColor(Dictionary<decimal, string> scales, string color, double value, int optMax)
        {
            double pct = value * 100 / optMax;
            foreach (KeyValuePair<decimal, string> scale in scales)
            {
                if (pct <= (double)scale.Key)
                {
                    color = scale.Value;
                    break;
                }
            }

            return color;
        }

        private Dictionary<decimal, string> GetScales(Test test)
        {
            Dictionary<decimal, string> scales;
            if (test.ClimateScale_Id.HasValue)
            {
                scales = test.ClimateScale.ClimateRanges.OrderBy(v => v.MaxValue).ToDictionary(cr => cr.MaxValue, cr => cr.Color);
            }
            else
            {
                scales = new Dictionary<decimal, string>()
                {
                    { 60, "#FF004C" },
                    { 80, "#FECE00" },
                    { 100, "#00B386" }
                };
            }
            return scales;
        }

        private string GetTitle(string demographic, string type, string c_fo, string auxdemo)//obtiene el titulo del gráfico
        {
            switch (type)
            {
                case "Population":
                    switch (demographic)
                    {
                        case "General":
                            return ViewRes.Views.ChartReport.Graphics.SampleReceived;//dependiendo del demográfico,estructura funcional,etc, devuelve el viewres
                        case "AgeRange":
                            return ViewRes.Controllers.ChartReports.AgePercentage;
                        case "PositionLevel":
                            return ViewRes.Controllers.ChartReports.PositionLevelPercentage;
                        case "Seniority":
                            return ViewRes.Controllers.ChartReports.SeniorityPercentage;
                        case "Country":
                            return ViewRes.Controllers.ChartReports.CountryPercentage;
                        case "Gender":
                            return ViewRes.Controllers.ChartReports.GenderPercentage;
                        case "InstructionLevel":
                            return ViewRes.Controllers.ChartReports.InstructionLevelPercentage;
                        case "Performance":
                            return ViewRes.Controllers.ChartReports.PerformancePercentage;
                        case "Region":
                            return ViewRes.Controllers.ChartReports.RegionPercentage;
                        case "Location":
                            return ViewRes.Controllers.ChartReports.LocationPercentage;
                        case "State":
                            return ViewRes.Controllers.ChartReports.StatePercentage + " " + c_fo;
                        case "FunctionalOrganizationType":
                            return ViewRes.Controllers.ChartReports.PercentageBy + " " + c_fo;
                        default:
                            return "";
                    }
                case "Univariate":
                    switch (demographic)
                    {
                        case "General":
                            return ViewRes.Controllers.ChartReports.GeneralClimate;
                        case "Category":
                            return ViewRes.Controllers.ChartReports.CategoryClimate;
                        case "AgeRange":
                            return ViewRes.Controllers.ChartReports.AgeClimate;
                        case "PositionLevel":
                            return ViewRes.Controllers.ChartReports.PositionLevelClimate;
                        case "Seniority":
                            return ViewRes.Controllers.ChartReports.SeniorityClimate;
                        case "Country":
                            return ViewRes.Controllers.ChartReports.CountryClimate;
                        case "Gender":
                            return ViewRes.Controllers.ChartReports.GenderClimate;
                        case "InstructionLevel":
                            return ViewRes.Controllers.ChartReports.InstructionLevelClimate;
                        case "Performance":
                            return ViewRes.Controllers.ChartReports.PerformanceClimate;
                        case "Region":
                            return ViewRes.Controllers.ChartReports.RegionClimate;
                        case "Location":
                            return ViewRes.Controllers.ChartReports.LocationClimate;
                        case "State":
                            return ViewRes.Controllers.ChartReports.StateClimate + c_fo;
                        case "FunctionalOrganizationType":
                            return ViewRes.Controllers.ChartReports.ClimateBy + c_fo;
                        case "AllTests":
                            return ViewRes.Controllers.ChartReports.MeasurementHistory;
                        default:
                            return "";
                    }
                case "Frequency":
                    switch (demographic)
                    {
                        case "General":
                            return ViewRes.Controllers.ChartReports.GeneralFrequency;
                        case "AgeRange":
                            return ViewRes.Controllers.ChartReports.AgeFrequency;
                        case "PositionLevel":
                            return ViewRes.Controllers.ChartReports.PositionLevelFrequency;
                        case "Seniority":
                            return ViewRes.Controllers.ChartReports.SeniorityFrequency;
                        case "Country":
                            return ViewRes.Controllers.ChartReports.CountryFrequency;
                        case "Gender":
                            return ViewRes.Controllers.ChartReports.GenderFrequency;
                        case "InstructionLevel":
                            return ViewRes.Controllers.ChartReports.InstructionLevelFrequency;
                        case "Performance":
                            return ViewRes.Controllers.ChartReports.PerformanceFrequency;
                        case "Region":
                            return ViewRes.Controllers.ChartReports.RegionFrequency;
                        case "Location":
                            return ViewRes.Controllers.ChartReports.LocationFrequency;
                        case "State":
                            return ViewRes.Controllers.ChartReports.StateFrequency + c_fo;
                        case "FunctionalOrganizationType":
                            return ViewRes.Controllers.ChartReports.FrequencyBy + c_fo;
                        default:
                            return "";
                    }
                case "Category":
                    switch (demographic)
                    {
                        case "General":
                            return ViewRes.Controllers.ChartReports.GeneralCategory;
                        case "AgeRange":
                            return ViewRes.Controllers.ChartReports.AgeCategory + " (" + auxdemo + ")";//estos aux es solo para los reportes de categorias,categoria x Rango de Edad(18), aux sería 18
                        case "PositionLevel":
                            return ViewRes.Controllers.ChartReports.PositionLevelCategory + " (" + auxdemo + ")";
                        case "Seniority":
                            return ViewRes.Controllers.ChartReports.SeniorityCategory + " (" + auxdemo + ")";
                        case "Country":
                            return ViewRes.Controllers.ChartReports.CountryCategory + " (" + auxdemo + ")";
                        case "Gender":
                            return ViewRes.Controllers.ChartReports.GenderCategory + " (" + auxdemo + ")";
                        case "InstructionLevel":
                            return ViewRes.Controllers.ChartReports.InstructionLevelCategory + " (" + auxdemo + ")";
                        case "Performance":
                            return ViewRes.Controllers.ChartReports.PerformanceCategory + " (" + auxdemo + ")";
                        case "Region":
                            return ViewRes.Controllers.ChartReports.RegionCategory + " (" + auxdemo + ")";
                        case "Location":
                            return ViewRes.Controllers.ChartReports.LocationCategory + " (" + auxdemo + ")";
                        case "State":
                            return ViewRes.Controllers.ChartReports.StateCategory + c_fo + " (" + auxdemo + ")";
                        case "FunctionalOrganizationType":
                            return ViewRes.Controllers.ChartReports.CategoryBy + c_fo + " (" + auxdemo + ")";
                        default:
                            return "";
                    }
                case "Comparative":
                    switch (demographic)
                    {
                        case "Population":
                            return ViewRes.Controllers.ChartReports.PopulationComparative;
                        case "Climate":
                            return ViewRes.Controllers.ChartReports.ClimateComparative +
                                (c_fo == "" ? new DemographicsServices().GetNameInSelectedLanguage(auxdemo) : c_fo);
                        default:
                            return "";
                    }
                default:
                    return "";
            }
        }

        private static Dictionary<string, double> UpdatePopulationLegends(string demographic, Dictionary<string, double> data)
        {
            Dictionary<string, double> new_dict = new Dictionary<string, double>();
            string key = "";
            foreach (KeyValuePair<string, double> d in data)
            {
                if (demographic == "General")
                {
                    if (d.Key == "Received")
                        key = ViewRes.Views.ChartReport.Graphics.Received;
                    else
                        key = ViewRes.Views.ChartReport.Graphics.NotReceived;
                }
                else if (demographic == "Gender")
                {
                    if (d.Key == "Female")
                        key = ViewRes.Views.Shared.Shared.Female;
                    else
                        key = ViewRes.Views.Shared.Shared.Male;
                }
                new_dict.Add(key, d.Value);
            }
            return new_dict;
        }


        #region PercentagesCharts

        public FileResult GeneralPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();//se crea un diccionario para un procedimiento,tiene los parametros para consultar a la BD junto con el nombre del demográfico y el tipo de consulta o reporte
            parameters.Add("demographic", "General");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);//porsi hay detalles o comentarios guardados en gráhic details
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("General", "Population", "", "");//si el gráfico que obtuve no tiene titulo o es vacio,se va a colocar lo que sea que tenga el gráfico sino lo manda a buscar con  GetTitle ya sea en ingles o español
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "General",//se hacen 2 cosas,1: obntener la informacion que le voy a mandar al gráfico(es un diccionario). ChartDetails:hace el gráfico
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);//ChartsServices:da la información del gráfico
            MemoryStream ms = new MemoryStream();//pasa el chart a MemoryStream
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");//el MemoryStream dio la imágen
        }
        public ActionResult CountryPercentageChartMap(string chartSize, string chartType, int test_id, int graphic_id, string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Country");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Country", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Country",
                                new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            Session["Chart"] = ms.ToArray();
            return Content(cd.chart.GetHtmlImageMap(name));
        }
        public ActionResult CountryPercentageChart(string chartSize, string chartType)
        {
            byte[] data = Session["Chart"] as byte[];
            return File(data, @"image/png");
        }
        public FileResult RegionPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Region");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Region", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Region",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult AgeRangePercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "AgeRange");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("AgeRange", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "AgeRange",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult GenderPercentageChart(string chartSize, string chartType, int test_id, int graphic_id,
                                                int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Gender");
            parameters.Add("test", test_id);
            if (country_id.HasValue)
                parameters.Add("Country", country_id.Value);
            if (state_id.HasValue)
                parameters.Add("State", state_id.Value);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Gender", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Gender",
                               new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, country_id, state_id, region_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult SeniorityPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Seniority");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Seniority", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Seniority",
                               new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult PositionLevelPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "PositionLevel");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("PositionLevel", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "PositionLevel",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult LocationPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Location");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Location", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Location",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult InstructionLevelPercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "InstructionLevel");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("InstructionLevel", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "InstructionLevel",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult StatePercentageChart(string chartSize, string chartType, int country_id, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "State");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("State", "Population", new CountriesServices().GetById(country_id).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "State",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult PerformancePercentageChart(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Performance");
            parameters.Add("test", test_id);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("Performance", "Population", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "Performance",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FunctionalOrganizationTypePercentageChart(string chartSize, string chartType, int test_id, int graphic_id, int id_FO)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "FunctionalOrganizationType");
            parameters.Add("test", test_id);
            parameters.Add("fot", id_FO);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = graphDetail.Title != "" ? graphDetail.Title : GetTitle("FunctionalOrganizationType", "Population", new FunctionalOrganizationTypesServices().GetById(id_FO).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, tresD, title, series, "FunctionalOrganizationType",
                              new ChartsServices().GetGraphicDataForPopulation("Population", parameters).ToArray(), test_id, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png"); //return null;
        }
        #endregion PercentagesCharts

        #region UnivariateChart
        public FileResult GeneralUniVariateChart(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare,
                                                    int? country_id, int? state_id, int? region_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary;
            if (country_id.HasValue || region_id.HasValue)
                dictionary = test.GetGeneralAvgAndMedByUbication(questionnaire_id, category_id, question_id,
                            country_id, state_id, region_id);
            else
                dictionary = test.GetGeneralAvgAndMed(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetGeneralAvgAndMed(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("General", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "General", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        public FileResult AllTestsUniVariateChart(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Dictionary<string, double> dictionaryAllTests = new Dictionary<string, double>();
            foreach (Test allTest in test.Questionnaire.Tests.Where(t => t.Company_Id == test.Company_Id))
            {
                dictionaryAllTests.Add(allTest.Name, allTest.GetAvg(questionnaire_id, category_id, question_id));
            }
            dictionary.Add("AllTests", dictionaryAllTests);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("AllTests", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, null, "AllTests", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        public FileResult CategoryUniVariateChart(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? compare,
                                                    int? country_id, int? state_id, int? region_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetCategoryAvgAndMed(compare.HasValue, questionnaire_id, country_id,
                                                                                state_id, region_id);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetCategoryAvgAndMed(true, questionnaire_id, country_id,
                                                                                                state_id, region_id);
                Dictionary<string, double> aux = (Dictionary<string, double>)dictionary[test.Name];
                dictionary.Add(compareTest.Name, GetOrderedCategories(aux.Keys.ToList(), (Dictionary<string, double>)dictionaryCompare[compareTest.Name]));
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Category", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        private Dictionary<string, double> GetOrderedCategories(List<string> keys, Dictionary<string, double> dictionaryCompare)
        {
            Dictionary<string, double> auxi = new Dictionary<string, double>();
            foreach (string key in keys)
            {
                auxi.Add(key, dictionaryCompare[key]);
            }
            return auxi;
        }

        public FileResult UniVariateChartByAgeRange(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("AgeRange", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByPerformance(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByPerformanceEvaluations(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByPerformanceEvaluations(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Performance", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByLocation(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare,
                                                    int? country_id, int? state_id, int? region_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByLocations(questionnaire_id, category_id, question_id, compare.HasValue,
                                                                                country_id, state_id, region_id);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByLocations(questionnaire_id, category_id, question_id, true,
                                                                                country_id, state_id, region_id);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Location", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByPositionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByPositionLevels(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByPositionLevels(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("PositionLevel", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartBySeniority(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedBySeniorities(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedBySeniorities(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Seniority", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByGender(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByGender(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByGender(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Gender", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByInstructionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByInstructionLevels(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByInstructionLevels(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("InstructionLevel", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByRegion(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByRegions(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByRegions(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Region", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByState(string chartSize, string chartType, int graphic_id, int country_id, int? questionnaire_id, int? category_id, int? question_id, int test_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("State", "Univariate", new CountriesServices().GetById(country_id).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              test.GetAvgAndMedByStates(category_id, question_id, country_id), test_id, null, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public ActionResult UniVariateChartMapByCountry(string chartSize, string chartType, int graphic_id, int test_id, int? compare, int? questionnaire_id, int? category_id, int? question_id, string name)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Country", "Univariate", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            Session["Chart2"] = ms.ToArray();
            return Content(cd.chart.GetHtmlImageMap(name));
        }
        public ActionResult UniVariateChartByCountry(string chartSize, string chartType, int? questionnaire_id, int? category_id, int? question_id)
        {
            byte[] data = Session["Chart2"] as byte[];
            return File(data, @"image/png");
        }
        public ActionResult UniVariateChartMapByCountryPrueba(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, string name, double? pValue)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, false), test_id, null, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByFunctionalOrganizationType(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int type_id, int? compare)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = test.GetAvgAndMedByFOTypes(questionnaire_id, category_id, question_id, type_id, compare.HasValue);
            Test compareTest;
            if (compare.HasValue)
            {
                compareTest = new TestsServices().GetById(compare.Value);
                Dictionary<string, object> dictionaryCompare = compareTest.GetAvgAndMedByFOTypes(questionnaire_id, category_id, question_id, type_id, true);
                dictionary.Add(compareTest.Name, dictionaryCompare[compareTest.Name]);
            }
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("FunctionalOrganizationType", "Univariate", new FunctionalOrganizationTypesServices().GetById(type_id).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        #endregion UnivariablesCharts

        #region FrequencyChart
        public FileResult GeneralFrequencyChart(string chartSize, string chartType, int graphic_id, int test_id,
                                                 int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "General", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("General", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        private Dictionary<string, object> GetParametersForFrequency(Test test, int? questionnaire_id, int? category_id, int? question_id, string demographic, int? c_fo)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", test.Id);
            if (demographic == "FunctionalOrganizationType")
                parameters.Add("fot", c_fo.Value);
            else if (demographic == "State")
                parameters.Add("country", c_fo.Value);
            parameters.Add("minimumAnswers", test.GetMinimumAnswers(demographic, null, category_id, question_id, true));
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (category_id.HasValue)
            {
                if (!test.OneQuestionnaire && !questionnaire_id.HasValue)
                    parameters.Add("categorygroup", category_id.Value);
                else
                    parameters.Add("category", category_id.Value);
                if (question_id.HasValue)
                    parameters.Add("question", question_id.Value);
            }
            return parameters;
        }
        public FileResult FrequencyChartByAgeRange(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "AgeRange", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("AgeRange", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByPerformance(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Performance", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Performance", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByLocation(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Location", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Location", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByPositionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "PositionLevel", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("PositionLevel", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartBySeniority(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Seniority", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Seniority", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByGender(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Gender", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Gender", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByInstructionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "InstructionLevel", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("InstructionLevel", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByRegion(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Region", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Region", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult FrequencyChartByState(string chartSize, string chartType, int graphic_id, int country_id, int? questionnaire_id, int? category_id, int? question_id, int test_id)
        {
            Test test = new TestsServices().GetById(test_id);
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "State", country_id);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("State", "Frequency", new CountriesServices().GetById(country_id).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, null, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public ActionResult FrequencyChartMapByCountry(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, string name)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "Country", null);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Country", "Frequency", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            Session["Chart2"] = ms.ToArray();
            return Content(cd.chart.GetHtmlImageMap(name));
        }
        public ActionResult FrequencyChartByCountry(string chartSize, string chartType, int? questionnaire_id, int? category_id, int? question_id)
        {
            byte[] data = Session["Chart2"] as byte[];
            return File(data, @"image/png");
        }
        public FileResult FrequencyChartByFunctionalOrganizationType(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, int? question_id, int type_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = GetParametersForFrequency(test, questionnaire_id, category_id, question_id, "FunctionalOrganizationType", type_id);
            int options = new SelectionAnswersServices().GetCountByTest(test_id, questionnaire_id, category_id, question_id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Frequency", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("FunctionalOrganizationType", "Frequency", new FunctionalOrganizationTypesServices().GetById(type_id).Name, "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Bivariate", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        #endregion

        #region CategoryChart
        public FileResult GeneralCategoryChart(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "General");
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("General", "Category", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByAgeRange(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "AgeRange" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new AgesServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByPerformance(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Performance" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new PerformanceEvaluationsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByLocation(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Location" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new LocationsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByPositionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "PositionLevel" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new PositionLevelsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartBySeniority(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Seniority" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new SenioritiesServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByGender(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Gender" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value == 0 ? "Female" : "Male");
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? (id.Value == 0 ? ViewRes.Classes.ChiSquare.FemaleGender : ViewRes.Classes.ChiSquare.MaleGender) : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByInstructionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "InstructionLevel" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new InstructionLevelsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByRegion(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Region" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new RegionsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public FileResult CategoryChartByState(string chartSize, string chartType, int graphic_id, int country_id, int test_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "State" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new StatesServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", new CountriesServices().GetById(country_id).Name, auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        public ActionResult CategoryChartMapByCountry(string chartSize, string chartType, int graphic_id, int test_id, int? questionnaire_id, int? category_id, string name)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            int? id = category_id;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "Country" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new CountriesServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", "", auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            Session["Chart2"] = ms.ToArray();
            return Content(cd.chart.GetHtmlImageMap(name));
        }
        public ActionResult CategoryChartByCountry(string chartSize, string chartType)
        {
            byte[] data = Session["Chart2"] as byte[];
            return File(data, @"image/png");
        }
        public FileResult CategoryChartByFunctionalOrganizationType(string chartSize, string chartType, int graphic_id, int test_id, int type_id, int? questionnaire_id, int? id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? compare = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string demographic = id.HasValue ? "FunctionalOrganizationType" : "General";
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test.MinimumPeople);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (id.HasValue)
                parameters.Add("id", id.Value);
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string auxdemo = id.HasValue ? new FunctionalOrganizationsServices().GetById(id.Value).Name : "";
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle(demographic, "Category", new FunctionalOrganizationTypesServices().GetById(type_id).Name, auxdemo);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, compare, "Category", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }
        #endregion

        #region ComparativeCharts

        //Comparativo de participación
        public FileResult ComparativeChartByPopulation(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int options = test.Company.Tests.Select(v => v.EvaluationNumber).Max();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Population");
            parameters.Add("selector", "General");
            parameters.Add("company", test.Company_Id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForComparativeGraph("Comparative", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title : GetTitle("Population", "Comparative", "", "");
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, null, "Comparative", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        //Comparativo de clima por demográfico selector
        public FileResult ComparativeChartByClimate(string chartSize, string chartType, int test_id, int graphic_id)
        {
            Test test = new TestsServices().GetById(test_id);
            DemographicsInTest dit = test.DemographicsInTests.Where(s => s.Selector).FirstOrDefault();
            int options = test.GetOptionsByTest().Select(v => v.Value).Max();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Climate");
            parameters.Add("selector", dit.Demographic.Name);
            if (dit.FOT_Id.HasValue)
                parameters.Add("fot", dit.FOT_Id);
            parameters.Add("company", test.Company_Id);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForComparative("Comparative", parameters);
            GraphicDetail graphDetail = new GraphicsDetailsServices().GetDetailsOrNewByGraphicId(graphic_id);
            string title = (graphDetail.Title != "" && graphDetail.Title != null) ? graphDetail.Title :
                GetTitle("Climate", "Comparative", dit.FOT_Id.HasValue ? dit.FunctionalOrganizationType.Name : "", dit.Demographic.Name);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetUnivariateTitle(title, graphDetail), 2,
                              dictionary, test_id, 1, "Comparative", options, false);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return File(ms.GetBuffer(), @"image/png");
        }

        #endregion

        public void InitializeViews(string type, string demographic, int? c_fo, int test_id, int? compare_id)
        {
            Test _test;
            GraphicDetail[] _details = null;
            string _chartType;
            Dictionary<string, int> _demographicsCount;
            SelectList demographicsList;
            SelectList _question;
            bool _condition = GetUserSession();
            _chartType = type;//aqui se sabe que tipo de grafico es
            _test = new TestsServices().GetById(test_id);

            //Actualizamos el verdadero número de mediciones realizadas al momento
            int evaluations = new EvaluationsServices().GetByTest(test_id).Count();
            _test.CurrentEvaluations = evaluations;

            //_details = new GraphicsDetailsServices().GetDetailsByTest(test_id);
            SelectList _questionnaires = _test.OneQuestionnaire ? new SelectList(new Dictionary<int, string>(), "Key", "Value") ://si es uno o varios cuestionarios
                                            new SelectList(new DemographicSelectorDetailsServices().GetQuestionnairesByTestForDropDownList(test_id), "Key", "Value");
            SelectList _categories;
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            int _questionType = type == "TextAnswers" ? 2 : (type == "Frequency" ? _test.GetQuestionsType() : 1);//es para el reporte de las respuestas de texto,sino es texto,pregunta si es el tipo de pregunta de frecuencia
            Dictionary<string, object> table;
            switch (type)
            {
                case "Bivariate":
                    demographicsList = new SelectList(new ChartsServices().DemographicsDropDownList(_test), "Key", "Value");
                    _chartViewModel = new ChartReportViewModel(_test, _chartType, _details, demographicsList);
                    break;
                case "Category":
                    _categories = new SelectList(_chartService.DemographicDDLForCategory(_test, demographic, c_fo, null), "Key", "Value");
                    _question = new SelectList(new QuestionsServices().GetQuestionsByCategory(null), "key", "Value");
                    _demographicsCount = _chartService.GetDemographicsCount(_test);
                    _chartViewModel = new ChartReportViewModel(_test, _chartType, _details, _demographicsCount, _question, _categories, _questionnaires, _condition, 0, user);
                    break;
                case "Comparative":
                    DemographicsInTest dit = _test.DemographicsInTests.Where(s => s.Selector).FirstOrDefault();
                    table = (Dictionary<string, object>)new Commands("Comparative", GetParametersForComparative(_test, dit, false)).ExecuteCommand();
                    Dictionary<string, int[]> total = (Dictionary<string, int[]>)new Commands("Comparative", GetParametersForComparative(_test, dit, true)).ExecuteCommand();
                    _chartViewModel = new ChartReportViewModel(_test, table, total, dit, _details, user);
                    break;
                case "Satisfaction":
                    string title = GetTitleForSatisfactionTable(demographic);
                    Dictionary<int, string> tabs = demographic == "Category" && !c_fo.HasValue ? GetSatisfactionTabsByTest(_test)
                                                    : new Dictionary<int, string>();
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("demographic", demographic);
                    parameters.Add("test", _test.Id);
                    if (c_fo.HasValue)
                        parameters.Add("id", c_fo.Value);
                    parameters.Add("minimumPeople", _test.MinimumPeople);
                    table = (Dictionary<string, object>)new Commands("Satisfaction", parameters).ExecuteCommand();
                    _chartViewModel = new ChartReportViewModel(_test, tabs, table, title, _details, user);
                    break;
                default:
                    int compare = compare_id.HasValue ? compare_id.Value : 0;
                    _categories = _test.OneQuestionnaire ?
                        new SelectList(new CategoriesServices().GetCategoriesForList(_test.Questionnaire_Id.Value, _questionType), "Value", "Text") :
                        new SelectList(new CategoriesServices().GetGroupingCategoriesByCompanyForDropDownList(_test.Company_Id), "Key", "Value");
                    _demographicsCount = _chartService.GetDemographicsCount(_test);
                    _question = new SelectList(new QuestionsServices().GetQuestionsByCategory(null), "key", "Value");
                    _chartViewModel = new ChartReportViewModel(_test, _chartType, _details, _demographicsCount, _question, _categories, _questionnaires, _condition, compare, user);
                    break;
            }
        }

        private static string GetTitleForSatisfactionTable(string demographic)
        {
            string title;
            switch (demographic)
            {
                case "Category":
                    title = ViewRes.Views.ChartReport.Graphics.CategoryTab;
                    break;
                case "Location":
                    title = ViewRes.Views.ChartReport.Graphics.LocationTab;
                    break;
                case "FunctionalOrganizationType":
                    title = ViewRes.Views.ChartReport.Graphics.FOTypeTab;
                    break;
                default:
                    title = "";
                    break;
            }
            return title;
        }

        private Dictionary<int, string> GetSatisfactionTabsByTest(Test test)
        {
            Dictionary<int, string> tabs = new Dictionary<int, string>();
            DemographicsInTest dit = test.DemographicsInTests.Where(s => s.Selector).FirstOrDefault();
            switch (dit.Demographic.Name)
            {
                case "AgeRange":
                    return test.Evaluations.Select(e => e.Age).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Gender":
                    tabs.Add(0, ViewRes.Classes.ChiSquare.FemaleGender);
                    tabs.Add(1, ViewRes.Classes.ChiSquare.MaleGender);
                    return tabs;
                case "InstructionLevel":
                    return test.Evaluations.Select(e => e.InstructionLevel).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "PositionLevel":
                    return test.Evaluations.Select(e => e.PositionLevel).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Performance":
                    return test.Evaluations.Select(e => e.PerformanceEvaluation).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Seniority":
                    return test.Evaluations.Select(e => e.Seniority).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Location":
                    return test.Evaluations.Select(e => e.Location).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "State":
                    return test.Evaluations.Select(e => e.Location.State).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Region":
                    return test.Evaluations.Select(e => e.Location.Region).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "Country":
                    return test.Evaluations.Select(e => e.Location.State.Country).Distinct().ToDictionary(k => k.Id, v => v.Name);
                case "FunctionalOrganizationType":
                    return test.Evaluations.SelectMany(e => e.EvaluationFOs.Where(fot => fot.FunctionalOrganization.Type_Id == dit.FOT_Id.Value)).Distinct().ToDictionary(k => k.FunctionalOrganization.Id, v => v.FunctionalOrganization.Name);
                default:
                    return tabs;
            }
        }

        private static Dictionary<string, object> GetParametersForComparative(Test _test, DemographicsInTest dit, bool general)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Population");
            if (general)
                parameters.Add("selector", "General");
            else
            {
                parameters.Add("selector", dit.Demographic.Name);
                if (dit.FOT_Id.HasValue)
                    parameters.Add("fot", dit.FOT_Id.Value);
            }
            parameters.Add("company", _test.Company_Id);
            return parameters;
        }

        public bool GetUserSession()
        {
            int company_id = new UsersServices().GetByUserName(User.Identity.Name).Company_Id;

            if ((new PerformanceEvaluationsServices().GetByCompany(company_id)) != null)
            {
                return true;

            }
            return false;
        }

        #region JsonResults
        [HttpPost]
        public JsonResult UpdateTable(int? questionnaire_id, int? category_id, int? question_id, double? pValue, int test_id, string demographic, bool condition, int? FO_id, int? compare_id)
        {
            return Json(new SummaryTable().UpdateTable(questionnaire_id, category_id, question_id, pValue, test_id, demographic, condition, FO_id, compare_id));
        }

        public JsonResult UpdateTableFC(int? questionnaire_id, int? category_id, int? question_id, int test_id, string demographic, int? FO_id, string type, string header)
        {
            Dictionary<string, object> dictionary = new TestsServices().GetById(test_id).GetDataFrequencyCategory(type, demographic, questionnaire_id, category_id, question_id, FO_id, null);

            List<string> keys;
            Test test = new TestsServices().GetById(test_id);
            if (test.OneQuestionnaire)
            {
                if (test.Questionnaire.Options.Count == 0) //test_id == 99 || test_id == 100,si el cuestionario tiene opciones, y si las tiene busca esas opciones
                {//test.Questionnaire.Options.Count == 0 esto quiere decir que el cuestionario no tiene opciones asociadas

                    //keys = dictionary.Keys.ToList();
                    keys = question_id.HasValue ? new QuestionsServices().GetById(question_id.Value).Options.Select(q => q.Text).ToList() : new List<string>();
                }
                else
                {
                    keys = test.Questionnaire.Options.Select(q => q.Text).ToList();
                    //keys = header.Split('-').ToList();// dictionary.Keys.ToList();
                    //keys.RemoveAll(k => k == "");
                }
            }
            else
            {

                //se asumio que cuando eran vaRios cuestionarios las opciones eran las mismas para todos

                keys = test.GetOptionsByTest().Select(q => q.Text).ToList();

                //modificar en caso de que los cuestionarios tengan las opciones asociadas a las preguntas y no al cuestionario

            }
            //Busco el 
            int posMayor = 0, cont = 0, contMayor = 0, posActual = 0;
            foreach (Dictionary<string, double> a in dictionary.Values)
            {

                foreach (var key in a.Keys)
                {
                    cont++;
                }
                if (cont > contMayor)
                {
                    contMayor = cont;
                    posMayor = posActual;
                }
                posActual++;
                cont = 0;

            }
            Dictionary<string, double> aux = (Dictionary<string, double>)dictionary.Values.ElementAt(posMayor);
            List<object> data = new List<object>();
            data.Add(keys);
            List<object> dataAux = new List<object>();
            foreach (string key in aux.Keys.ToList())
            {
                dataAux = new List<object>();
                dataAux.Add(key);
                foreach (string keyHead in keys)
                {
                    if (dictionary.ContainsKey(keyHead))
                    {
                        Dictionary<string, double> dicHead = (Dictionary<string, double>)dictionary[keyHead];
                        dataAux.Add(dicHead.ContainsKey(key) ? dicHead[key] : 0);
                    }
                    else
                        dataAux.Add(0);
                }
                data.Add(dataAux);
            }
            data.Add(keys);
            return Json(data);
        }

        [HttpPost]
        public JsonResult UpdateTableForTextAnswers(int? questionnaire_id, int? category_id, int? question_id, int test_id, string demographic, int? FO_id, int? compare_id)
        {
            //List<SummaryTable> summary = new SummaryTable().UpdateTable(category_id, question_id, test_id, demographic, FO_id, compare_id);
            //if (compare_id.HasValue)
            //    return Json(summary);
            //else
            //{
            //    Dictionary<string, string[]> answers = new Dictionary<string, string[]>();
            //    foreach (SummaryTable sum in summary)
            //        answers.Add(sum.Label, sum.TextAnswers);
            //    return Json(answers);
            //}
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (category_id.HasValue)
            {
                if (!(new TestsServices().GetById(test_id).OneQuestionnaire) && !questionnaire_id.HasValue)//si ese test no es de un solo cuestionario y ese cuestionario no tiene valor
                    parameters.Add("categorygroup", category_id.Value);
                else
                    parameters.Add("category", category_id.Value);
                if (question_id.HasValue)
                    parameters.Add("question", question_id.Value);
            }
            if (demographic == "FunctionalOrganizationType")
                parameters.Add("fot", FO_id);
            return Json((Dictionary<string, List<string>>)new Commands("TextAnswer", parameters).ExecuteCommand());
        }

        [HttpPost]
        public JsonResult UpdateChiSquare(int? questionnaire_id, int? category_id, int? question_id, int? country_id, double? pValue, int test_id, string demographic, int? FO_id, int? compare_id)
        {
            List<object> chiS = new List<object>();
            Test test = new TestsServices().GetById(test_id);
            Test testCompare;
            double pvalue;
            if (pValue.HasValue)
                pvalue = pValue.Value;
            else
                pvalue = 0.05;
            ChiSquare[] chiSquare = new ChiSquare[2];
            chiSquare[0] = new ChiSquare(test, demographic, questionnaire_id, category_id, question_id, country_id, FO_id, pvalue, null, null, null);
            chiSquare[0].GetAssociation();
            chiS.Add(
                new
                {
                    chiSquareValue = chiSquare[0].ChiSquareValue,
                    ourChiSquare = chiSquare[0].OurChiSquare,
                    conclusion = chiSquare[0].Conclusion,
                    testName = test.Name
                });
            if (compare_id.HasValue)
            {
                testCompare = new TestsServices().GetById(compare_id.Value);
                chiSquare[1] = new ChiSquare(testCompare, demographic, questionnaire_id, category_id, question_id, country_id, FO_id, pvalue, null, null, null);
                chiSquare[1].GetAssociation();
                chiS.Add(
                new
                {
                    chiSquareValue = chiSquare[1].ChiSquareValue,
                    ourChiSquare = chiSquare[1].OurChiSquare,
                    conclusion = chiSquare[1].Conclusion,
                    testName = testCompare.Name
                });
            }
            return Json(chiS);
        }

        [HttpPost]
        public string GetTestName(int test_id)
        {
            return new TestsServices().GetById(test_id).Name;
        }

        [HttpPost]
        public JsonResult GetTestsToCompare(int test_id)
        {
            List<object> testsToCompare = new List<object>();
            foreach (KeyValuePair<int, string> test in new TestsServices().GetTestsToCompareForDropdownist(test_id))
            {
                testsToCompare.Add(
                    new
                    {
                        optionValue = test.Key,
                        optionDisplay = test.Value,
                    });
            }
            return Json(testsToCompare);
        }

        [HttpPost]
        public JsonResult GetDemographicsByQuestionnaire(int test_id, string demographic, int? c_fo, int? questionnaire_id)
        {
            List<object> demographicsInQuestionnaire = new List<object>();
            Test test = new TestsServices().GetById(test_id);
            foreach (KeyValuePair<int, string> demo in _chartService.DemographicDDLForCategory(test, demographic, c_fo, questionnaire_id))
            {
                demographicsInQuestionnaire.Add(
                    new
                    {
                        optionValue = demo.Key,
                        optionDisplay = demo.Value,
                    });
            }
            return Json(demographicsInQuestionnaire);
        }

        [HttpPost]
        public ActionResult LoadTab(string demographic, string type, int test_id, int? FO_id, int? compare_id)
        {
            int demoCount;
            InitializeViews(type, demographic, FO_id, test_id, compare_id);
            ViewData["option"] = demographic;
            ViewData["FO_id"] = FO_id;
            if (demographic == "Category" || demographic == "AllTests" || demographic == "Climate" || type == "Satisfaction")
                demoCount = 1;// new QuestionnairesServices().GetQuestionsCount(new TestsServices().GetById(test_id).Questionnaire_Id);
            else
            {
                if (demographic == "FunctionalOrganizationType")
                    demoCount = _chartViewModel.GetFOCount(FO_id.Value);
                else
                    demoCount = _chartViewModel.demographicsCount[demographic];
            }
            switch (type)
            {
                case "Population":
                    return PartialView("Population", _chartViewModel);
                case "TextAnswers":
                    return PartialView("TextAnswersForm", _chartViewModel);
                case "Frequency":
                    return PartialView("FrequencyTablesForm", _chartViewModel);
                case "Category":
                    if (_chartViewModel.GetCategoriesCountByTest(_chartViewModel.test.Id) > 10)
                        return PartialView("CategoryTablesForm", _chartViewModel);
                    else
                        return PartialView("CategoryChartsForm", _chartViewModel);
                case "Comparative":
                    return PartialView("Comparative", _chartViewModel);
                case "Satisfaction":
                    return PartialView("SatisfactionTable", _chartViewModel);
                default:
                    if (demoCount <= 10)
                    {
                        if (demographic == "Category")
                            return PartialView("ChartsFormForCategories", _chartViewModel);
                        else if (demographic == "AllTests")
                            return PartialView("ChartsFormForAllTests", _chartViewModel);
                        else
                            return PartialView("ChartsForm", _chartViewModel);
                    }
                    else
                        return PartialView("TablesForm", _chartViewModel);
            }
        }

        [HttpPost]
        public ActionResult LoadTableCategory(string demographic, int test_id, int? FO_id, int? id_questionnaire, int? id_demographic, int? compare_id)
        {
            Test test1 = new TestsServices().GetById(test_id);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (id_demographic.HasValue)
            {
                parameters.Add("demographic", demographic);
                string id = id_demographic.Value.ToString();
                if (demographic == "Gender")
                {
                    id = id == "1" ? "Male" : "Female";
                }
                parameters.Add("id", id);
            }
            else
            {
                parameters.Add("demographic", "General");
            }
            parameters.Add("test", test_id);
            parameters.Add("minimumPeople", test1.MinimumPeople);
            if (test1.OneQuestionnaire)
                parameters.Add("questionnaire", test1.Questionnaire_Id);
            else
                if (id_questionnaire.HasValue)
                parameters.Add("questionnaire", id_questionnaire.Value);
            Dictionary<string, object> dictionary = new ChartsServices().GetGraphicDataForFrequencyOrCategory("Category", parameters);
            IEnumerable<KeyValuePair<string, double>> pair = (IEnumerable<KeyValuePair<string, double>>)dictionary.Values.First();
            List<KeyValuePair<string, double>> listOfValues = pair.ToList();
            return Json(listOfValues);
        }



        #endregion

        #region ResultsPdf
        public ActionResult Results(int test_id)
        {
            Test test = new TestsServices().GetById(test_id);
            int? questionnaire_id = null;
            ResultViewModel resultViewModel = GetReport1(new WeighingsServices(), test, questionnaire_id, new Results(test), false);
            return View(resultViewModel);
        }

        private ResultViewModel GetReport1(WeighingsServices weighingService, Test test, int? questionnaire_id, Results r, bool print)
        {
            //double promPos = 0, prom = 0;
            //Dictionary<string, double> results = new Dictionary<string, double>();
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    prom = r.GetAnswersCount(cat.Id, null, "Positivas");
            //    results.Add(cat.Name, prom);
            //    if (test.Weighted)
            //        promPos = promPos + (prom * (double)weighingService.GetValueByTestAndCategory(test.Id, cat.Id) / 100);
            //}

            return new ResultViewModel(r.GetResult1Data(questionnaire_id), test, questionnaire_id, print, 1);
        }

        private ResultViewModel GetReport2(WeighingsServices weighingService, Test test, int? questionnaire_id, Results r, bool print)
        {
            //int catCount = test.Questionnaire.Categories.Count;
            //int optCount = test.Questionnaire.Options.Count;
            //double promPos = 0, promNeg = 0, promNeu = 0, promTPos = 0, promTNeg = 0, promTNeu = 0, weighted = 0;
            //Dictionary<string, double[]> results = new Dictionary<string, double[]>();
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    if (test.Weighted)
            //        weighted = (double)weighingService.GetValueByTestAndCategory(test.Id, cat.Id) / 100;
            //    else
            //        weighted = 1;
            //    promPos = r.GetAnswersCount(cat.Id, null, "Positivas");
            //    promTPos = promTPos + (promPos * weighted);
            //    promNeg = r.GetAnswersCount(cat.Id, null, "Negativas");
            //    promTNeg = promTNeg + (promNeg * weighted);
            //    if (optCount % 2 != 0)
            //    {
            //        promNeu = r.GetAnswersCount(cat.Id, null, "Neutras");
            //        promTNeu = promTNeu + (promNeu * weighted);
            //    }
            //    results.Add(cat.Name, new double[]{promPos, promNeu, promNeg});
            //}
            //if (!test.Weighted)
            //{
            //    promTPos = promTPos / catCount;// GetTotalByCategory(test, null);
            //    if (optCount % 2 != 0)
            //        promTNeu = promTNeu / catCount;
            //    promTNeg = promTNeg / catCount;
            //}
            //results.Add("Total", new double[]{promTPos, promTNeu, promTNeg});
            return new ResultViewModel(r.GetResult2Data(questionnaire_id), test, questionnaire_id, print, 2);
        }

        private ResultViewModel GetReport3(WeighingsServices weighingService, Test test, int? questionnaire_id, Results r, bool print)
        {
            //double promPos = 0;
            //Dictionary<string, object> results = new Dictionary<string, object>();
            //Dictionary<string, double> questions;
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    questions = new Dictionary<string, double>();
            //    foreach(Question quest in cat.Questions.Where(q => q.QuestionType_Id == 1))
            //    {
            //        questions.Add(quest.Text, r.GetAnswersCount(cat.Id, quest.Id, "Positivas"));
            //    }
            //    promPos = questions.Values.Average();
            //    questions.Add("Total", promPos);
            //    results.Add(cat.Name, questions);
            //}
            //return new ResultViewModel(results, test, print, 3);
            return new ResultViewModel(r.GetResult3Data(questionnaire_id), test, questionnaire_id, print, 3);
        }

        private ResultViewModel GetReport4(WeighingsServices weighingService, Test test, int? questionnaire_id, Results r, bool print)
        {
            //double promPos=0, promNeg=0, promNeu=0, promTPos, promTNeg, promTNeu;
            //int questCount;
            //int optCount = test.Questionnaire.Options.Count;
            //Dictionary<string, object> results = new Dictionary<string, object>();
            //Dictionary<string, double[]> questions;
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    questCount = cat.Questions.Count;
            //    promTPos = 0; promTNeg = 0; promTNeu = 0;
            //    questions = new Dictionary<string, double[]>();
            //    foreach (Question quest in cat.Questions.Where(q => q.QuestionType_Id == 1))
            //    {
            //        promPos = r.GetAnswersCount(cat.Id, quest.Id, "Positivas");
            //        promTPos = promTPos + promPos;
            //        if (optCount % 2 != 0)
            //        {
            //            promNeu = r.GetAnswersCount(cat.Id, quest.Id, "Neutras");
            //            promTNeu = promTNeu + promNeu;
            //        }
            //        promNeg = r.GetAnswersCount(cat.Id, quest.Id, "Negativas");
            //        promTNeg = promTNeg + promNeg;
            //        questions.Add(quest.Text, new double[]{promPos,promNeu,promNeg});
            //    }
            //    promTPos = promTPos / questCount;
            //    if (optCount % 2 != 0)
            //        promTNeu = promTNeu / questCount;
            //    promTNeg = promTNeg / questCount;
            //    questions.Add("Total", new double[]{promTPos,promTNeu,promTNeg});
            //    results.Add(cat.Name, questions);
            //}
            //return new ResultViewModel(results, test, print, 4);
            return new ResultViewModel(r.GetResult4Data(questionnaire_id), test, questionnaire_id, print, 4);
        }

        private ResultViewModel GetReport5(WeighingsServices weighingService, Test test, int? questionnaire_id, Results r, bool print)
        {
            //TestsServices testService = new TestsServices();
            //Dictionary<string, double> resultsByCompany = new Dictionary<string,double>();
            //Dictionary<string, double[]> resultsByCategory = new Dictionary<string,double[]>();
            //IQueryable<Company> Companies = testService.GetByQuestionnaire(test.Questionnaire_Id).Where(t => t.Evaluations != null && t.Evaluations.Count > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.Id != test.Company_Id && c.CompanySector_Id == test.Company.CompanySector_Id).Distinct();
            //Test companyTest;
            //double prom, myProm, theirProm, myPromPos=0, theirPromPos=0, weighing;
            //bool comp = false;
            //int catCount = test.Questionnaire.Categories.Count;
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    myProm = r.GetAnswersCount(cat.Id, null, "Positivas");
            //    resultsByCompany = new Dictionary<string, double>();
            //    foreach (Company company in Companies)
            //    {
            //        comp = true;
            //        companyTest = testService.GetLastTestByCompany(company.Id,test.Questionnaire_Id, null);
            //        prom = new Results(companyTest).GetAnswersCount(cat.Id, null, "Positivas");
            //        resultsByCompany.Add(company.Name, prom);
            //    }
            //    theirProm = comp ? resultsByCompany.Values.Average() : 0;
            //    resultsByCategory.Add(cat.Name, new double[] { myProm, theirProm, myProm-theirProm});
            //    if (test.Weighted)
            //        weighing = (double)weighingService.GetValueByTestAndCategory(test.Id, cat.Id) / 100;
            //    else
            //        weighing = 1;
            //    myPromPos = myPromPos + (myProm * weighing);
            //    theirPromPos = theirPromPos + (theirProm * weighing);
            //}
            //if (!test.Weighted)
            //{
            //    myPromPos = myPromPos / catCount;
            //    theirPromPos = theirPromPos / catCount;
            //}
            //resultsByCategory.Add("Total", new double[] { myPromPos, theirPromPos, myPromPos - theirPromPos });
            //return new ResultViewModel(resultsByCategory,test, print, 5);
            Dictionary<string, double> company = r.GetResult1Data(questionnaire_id);
            Dictionary<string, double> market = r.GetResult5Data(questionnaire_id);
            Dictionary<string, double[]> comparative = new Dictionary<string, double[]>();
            foreach (string category in company.Keys)
            {
                comparative.Add(category, new double[] { company[category], market[category], (company[category] - market[category]) });
            }
            return new ResultViewModel(comparative, test, questionnaire_id, print, 5);
        }

        private ResultViewModel GetReport6(Test test, int? questionnaire_id, Results r, bool print)
        {
            //TestsServices testService = new TestsServices();
            //Dictionary<string, double> resultsByCompany = new Dictionary<string,double>();
            //Dictionary<string, double[]> resultsByQuestion = new Dictionary<string,double[]>();
            //Dictionary<string, object> resultsByCategory = new Dictionary<string, object>();
            //IQueryable<Company> Companies = testService.GetByQuestionnaire(test.Questionnaire_Id).Where(t => t.Evaluations != null && t.Evaluations.Count > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.Id != test.Company_Id && c.CompanySector_Id == test.Company.CompanySector_Id).Distinct();
            //Test companyTest;
            //double prom, myProm, theirProm, myPromPos=0, theirPromPos=0;
            //bool comp = false;
            //int questCount;
            //foreach (Category cat in test.Questionnaire.Categories)
            //{
            //    questCount = cat.Questions.Count;
            //    myPromPos = 0; theirPromPos = 0;
            //    resultsByQuestion = new Dictionary<string, double[]>();
            //    foreach (Question que in cat.Questions.Where(q => q.QuestionType_Id == 1))
            //    {
            //        myProm = r.GetAnswersCount(cat.Id, que.Id, "Positivas");
            //        resultsByCompany = new Dictionary<string, double>();
            //        foreach (Company company in Companies)
            //        {
            //            comp = true;
            //            companyTest = testService.GetLastTestByCompany(company.Id, test.Questionnaire_Id, null);
            //            prom = new Results(companyTest).GetAnswersCount(cat.Id, que.Id, "Positivas");
            //            resultsByCompany.Add(company.Name, prom);
            //        }
            //        theirProm = comp ? resultsByCompany.Values.Average() : 0;
            //        resultsByQuestion.Add(que.Text, new double[] { myProm, theirProm, myProm - theirProm });
            //        myPromPos = myPromPos + myProm;
            //        theirPromPos = theirPromPos + theirProm;
            //    }
            //    myPromPos = myPromPos / questCount;
            //    theirPromPos = theirPromPos / questCount;
            //    resultsByQuestion.Add("Total", new double[] {myPromPos, theirPromPos, myPromPos-theirPromPos });
            //    resultsByCategory.Add(cat.Name, resultsByQuestion);
            //}
            //return new ResultViewModel(resultsByCategory, test, print, 6);
            Dictionary<string, object> company = r.GetResult3Data(questionnaire_id);
            Dictionary<string, object> market = r.GetResult6Data(questionnaire_id);
            Dictionary<string, object> comparative = new Dictionary<string, object>();
            foreach (string category in company.Keys)
            {
                Dictionary<string, double> auxCompany = (Dictionary<string, double>)company[category];
                Dictionary<string, double> auxMarket = (Dictionary<string, double>)market[category];
                Dictionary<string, double[]> auxComparative = new Dictionary<string, double[]>();
                foreach (string question in auxCompany.Keys)
                {
                    double auxMarketQuestion = auxMarket.ContainsKey(question) ? auxMarket[question] : 0;
                    auxComparative.Add(question, new double[] { auxCompany[question], auxMarketQuestion, (auxCompany[question] - auxMarketQuestion) });
                }
                comparative.Add(category, auxComparative);
            }
            return new ResultViewModel(comparative, test, questionnaire_id, print, 6);
        }

        [HttpPost]
        public ActionResult LoadResult(int result, int test_id, int? questionnaire_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ResultViewModel resultViewModel = new ResultViewModel(test, false, 0);
            WeighingsServices weighingService = new WeighingsServices();
            Results r = new Results(test);
            string partialView = "Report" + result + (questionnaire_id.HasValue ? "Form" : "");
            int? q = !test.OneQuestionnaire && !questionnaire_id.HasValue && (result == 3 || result == 4) ? test.GetQuestionnairesByTest().First().Id : questionnaire_id;
            switch (result)
            {
                case 1:
                    resultViewModel = GetReport1(weighingService, test, questionnaire_id, r, false);
                    return PartialView(partialView, resultViewModel);
                case 2:
                    resultViewModel = GetReport2(weighingService, test, questionnaire_id, r, false);
                    return PartialView(partialView, resultViewModel);
                case 3:
                    resultViewModel = GetReport3(weighingService, test, q, r, false);
                    return PartialView(partialView, resultViewModel);
                case 4:
                    resultViewModel = GetReport4(weighingService, test, q, r, false);
                    return PartialView(partialView, resultViewModel);
                case 5:
                    resultViewModel = GetReport5(weighingService, test, questionnaire_id, r, false);
                    return PartialView(partialView, resultViewModel);
                case 6:
                    resultViewModel = GetReport6(test, questionnaire_id, r, false);
                    return PartialView(partialView, resultViewModel);
                default:
                    return View("Results");
            }
        }

        [HttpGet]
        public ViewResult Print(int test_id, int? questionnaire_id, int? report)
        {
            Test test = new TestsServices().GetById(test_id);
            WeighingsServices weighingService = new WeighingsServices();
            Results r = new Results(test);
            ResultViewModel[] resultViewModel;
            if (report.HasValue)
            {
                resultViewModel = new ResultViewModel[1];
                switch (report.Value)
                {
                    case 1:
                        resultViewModel[0] = GetReport1(weighingService, test, questionnaire_id, r, true);
                        break;
                    case 2:
                        resultViewModel[0] = GetReport2(weighingService, test, questionnaire_id, r, true);
                        break;
                    case 3:
                        resultViewModel[0] = GetReport3(weighingService, test, questionnaire_id, r, true);
                        break;
                    case 4:
                        resultViewModel[0] = GetReport4(weighingService, test, questionnaire_id, r, true);
                        break;
                    case 5:
                        resultViewModel[0] = GetReport5(weighingService, test, questionnaire_id, r, true);
                        break;
                    case 6:
                        resultViewModel[0] = GetReport6(test, questionnaire_id, r, true);
                        break;
                }
            }
            else
            {
                resultViewModel = new ResultViewModel[6];
                resultViewModel[0] = GetReport1(weighingService, test, questionnaire_id, r, true);
                resultViewModel[1] = GetReport2(weighingService, test, questionnaire_id, r, true);
                resultViewModel[2] = GetReport3(weighingService, test, questionnaire_id, r, true);
                resultViewModel[3] = GetReport4(weighingService, test, questionnaire_id, r, true);
                resultViewModel[4] = GetReport5(weighingService, test, questionnaire_id, r, true);
                resultViewModel[5] = GetReport6(test, questionnaire_id, r, true);
            }
            return View("PrintResults", resultViewModel);
        }

        [HttpGet]
        public PdfResult PdfResults(int test_id, int? report)
        {
            //return Print(test_id,report);
            return new PdfResult(GetHttpUrl() + "/ChartReports/Print?test_id=" + test_id + "&report=" + report, "PdfResults", true);
        }

        #endregion

        #region PrintGraphics

        //public ActionResult PrintBivariateGraphics(int test_id, string demographic_1, string demographic_2)
        //{
        //    FileResult graphic = null;
        //    object[] data = new object[8];
        //    bool isTable = GetIsTable(test_id, demographic_1, demographic_2);
        //        data = GetDataForTable(test_id, demographic_1, demographic_2);
        //    if (!isTable)
        //        graphic = SelectBivariateGraphic(test_id, demographic_1, demographic_2);
        //    _chartViewModel = new ChartReportViewModel(data[7].ToString(), data[0].ToString(), (int)data[1], data[2].ToString(), (int)data[3], isTable, graphic, (Dictionary<string, object>)data[4], data[6].ToString(), (int)data[5]);
        //    return View(_chartViewModel);
        //}

        [HttpGet]
        public ViewResult PrintPopulation(int test_id)
        {
            this.InitializeViews("Population", "", null, test_id, null);
            this.series = 1;
            this.tresD = true;
            return View("PrintPopulation", _chartViewModel);
        }

        [HttpGet]
        public ViewResult PrintUnivariate(int test_id, int graphic_id, int elementsCount, int? questionnaire_id, int? category_id,
            int? question_id, double pValue, int? FO_id, int? country_id, int? compare_id)
        {
            ChartReportViewModel chartPrintViewModel = InitializePrintView(test_id, graphic_id, elementsCount, questionnaire_id, category_id,
                question_id, pValue, FO_id, country_id, compare_id);
            this.series = 2;
            this.tresD = false;
            return View("PrintUnivariate", chartPrintViewModel);
        }

        public ChartReportViewModel InitializePrintView(int test_id, int graphic_id, int elementsCount, int? questionnaire_id, int? category_id,
            int? question_id, double pValue, int? FO_id, int? country_id, int? compare_id)
        {
            Test test = new TestsServices().GetById(test_id);
            Test testCompare = new Test();
            if (compare_id.HasValue)
                testCompare = new TestsServices().GetById(compare_id.Value);
            MedinetClassLibrary.Models.Graphic graphic = new GraphicsServices().GetById(graphic_id);
            ChiSquare[] chiSquare = new ChiSquare[2];
            if (elementsCount >= 2)
            {
                chiSquare[0] = new ChiSquare(test, graphic.Demographic, questionnaire_id, category_id, question_id, country_id, FO_id, pValue, null, null, null);
                chiSquare[0].GetAssociation();
                if (compare_id.HasValue)
                {
                    chiSquare[1] = new ChiSquare(testCompare, graphic.Demographic, questionnaire_id, category_id, question_id, country_id, FO_id, pValue, null, null, null);
                    chiSquare[1].GetAssociation();
                }
            }
            else
                chiSquare[0] = new ChiSquare();
            string categoryName;
            string questionText;
            string compareName;
            if (category_id.HasValue)
                categoryName = new CategoriesServices().GetById(category_id.Value).Name;
            else
                categoryName = "Todas las categorías";
            if (question_id.HasValue)
                questionText = new QuestionsServices().GetById(question_id.Value).Text;
            else
                questionText = "Todas las preguntas";
            if (compare_id.HasValue)
                compareName = testCompare.Name;
            else
                compareName = "Ninguno";
            ChartReportViewModel chartPrintViewModel = new ChartReportViewModel(
                test_id, test.Name, graphic, chiSquare,
                new SummaryTable().UpdateTable(questionnaire_id, category_id, question_id, pValue, test_id, graphic.Demographic, !(elementsCount < 7), FO_id, compare_id),
                FO_id, country_id, category_id, categoryName, question_id, questionText, pValue, elementsCount, compare_id, compareName);
            return chartPrintViewModel;
        }

        [HttpGet]
        public PdfResult PdfPopulation(int test_id)
        {
            //return PrintPopulation(test_id);
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintPopulation?test_id=" + test_id, "PdfGrpahics", true);
        }

        [HttpGet]
        public PdfResult PdfUnivariate(int test_id, int graphic_id, int elementsCount, int category_id,
            int question_id, double pValue, int FO_id, int country_id, int compare_id)
        {
            int? c_id; int? q_id; int? f_id; int? cy_id; int? tc_id;
            SetNullableValues(category_id, question_id, FO_id, country_id, compare_id, out c_id, out q_id, out f_id, out cy_id, out tc_id);
            //return PrintUnivariate(chartPrintViewModel);
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintUnivariate?test_id=" + test_id
                + "&graphic_id=" + graphic_id + "&elementsCount=" + elementsCount + "&category_id=" + c_id + "&question_id=" + q_id
                + "&pValue=" + pValue + "&FO_id=" + f_id + "&country_id=" + cy_id + "&compare_id=" + tc_id, "PdfGrpahics", true);
        }

        private static void SetNullableValues(int category_id, int question_id, int FO_id, int country_id, int compare_id, out int? c_id, out int? q_id, out int? f_id, out int? cy_id, out int? tc_id)
        {

            if (category_id == 0) c_id = null;
            else c_id = category_id;

            if (question_id == 0) q_id = null;
            else q_id = question_id;

            if (FO_id == 0) f_id = null;
            else f_id = FO_id;

            if (country_id == 0) cy_id = null;
            else cy_id = country_id;

            if (compare_id == 0) tc_id = null;
            else tc_id = compare_id;
        }

        [HttpPost]
        public string UpdateLink(string currentValue, string changeType, string newValue/*, string FO_id*/)
        {
            // 0:controller/action?test_id - 1:graphic_id - 2:elementsCount - 3:category_id - 4:question_id - 5:pValue - 6:FO_id - 7:country_id - 7:compare_id
            string[] link = currentValue.Split(new char[] { '&' });
            string newLink = "";
            string newV = "0";
            if (newValue != "")
                newV = newValue;
            switch (changeType)
            {
                case "category_id":
                    link[3] = "category_id=" + newV;
                    link[4] = "question_id=" + 0;
                    break;
                case "question_id":
                    link[4] = "question_id=" + newV;
                    break;
                case "pValue":
                    link[5] = "pValue=" + newV;
                    break;
                case "country_id":
                    link[7] = "country_id=" + newV;
                    break;
                case "compare_id":
                    link[8] = "compare_id=" + newV;
                    break;
                default:
                    break;
            }
            foreach (string li in link)
            {
                if (li == link.First())
                    newLink = li;
                else
                    newLink = newLink + '&' + li;
            }
            return newLink;
        }

        #endregion

        #region Analytical

        private AnalyticalReportViewModel InitializeAnalyticalView(int test_id, int? country, int? state,
                                                                    int? region, bool? Print)
        {
            Test Test = new TestsServices().GetById(test_id);
            //Actualizamos el verdadero número de mediciones realizadas al momento
            Test.CurrentEvaluations = Test.Evaluations.Count; ;
            User userLogged = new UsersServices().GetByUserName(User.Identity.Name);
            AnalyticalReport analyticalClass = new AnalyticalReport(Test, userLogged, country, state, region);
            double SatisfiedCountPercentage;
            double GeneralClimate;
            string ColourByClimate;
            List<string> DemographicsWhereThereIsAssociation;
            Dictionary<string, string> Ubication;
            Dictionary<string, double> ClimateByCategories;
            Dictionary<string, double> PositiveAnswersByPositionLevels;
            Dictionary<string, double> PositiveAnswersByFOTypes;
            Dictionary<string, double> ClimateByBranches;
            Dictionary<string, double> ClimateByFOTypes;
            Dictionary<string, double> ClimateByAgeRanges;
            Dictionary<string, object> SatNotSat;
            Dictionary<string, double> StepwiseValues;
            string FOTName;
            if (Test.ExecutiveReports.Where(e => e.Country_Id == country && e.State_Id == state && e.Region_Id == region).Count() == 0)
            {
                FunctionalOrganizationType fot = Test.Company.FunctionalOrganizationTypes.Count > 0 ? Test.Company.FunctionalOrganizationTypes.FirstOrDefault() : null;
                int fot_0 = fot != null ? fot.Id : 0;
                FOTName = fot != null ? fot.Name : "";
                SatisfiedCountPercentage = analyticalClass.GetSatisfiedCountPercentage();//porcentaje de resultados favorables
                GeneralClimate = analyticalClass.GetGeneralClimate();//obtener clima general
                ColourByClimate = analyticalClass.GetColourByClimate(GeneralClimate);//obtener el color del clima
                ClimateByCategories = analyticalClass.GetClimateByCategories();//clima por categorias,que devuleve los dos primeros y los dos ultimos
                PositiveAnswersByPositionLevels = analyticalClass.GetPositiveAnswersByPositionLevels();//respuestas positivas por nivel de cargo
                PositiveAnswersByFOTypes = analyticalClass.GetPositiveAnswersByFOTypes();//obtener respuestas positivas por la estructura funcional principal o padreS
                StepwiseValues = analyticalClass.GetStepwiseValues();//esto es para decir por ejemplo por categorias del cuestionario q por lo general se llaman dimensiones el % de lo que afectaba esa categoria en el contento o descontento de los empleados.el trabajo:trabajo en equipo aporto el 30%,comunicacion 30%.porcentaje de lo que aporto para que esto no llegara a 100
                DemographicsWhereThereIsAssociation = analyticalClass.GetDemographicsWhereThereIsAssociation();//demograficos donde hay asociacion
                ClimateByBranches = analyticalClass.GetClimateByBranches();//clima por sucursales
                ClimateByFOTypes = fot_0 > 0 ? analyticalClass.GetClimateByFOTypes(fot_0, FOTName) : null;//clima por sucursales
                ClimateByAgeRanges = analyticalClass.GetClimateByAgeRanges();//clima por sucursales
                SatNotSat = analyticalClass.GetSatisfiedAndNonSatisfied(fot_0);//son las tablas de satisfechos y no satisfechos por general,categorias y sucursal
                Ubication = analyticalClass.GetUbication();//ubicacion es el nombre de la medicion mas la medicion
            }
            else
            {
                ExecutiveReport er = Test.ExecutiveReports.Where(e => e.Country_Id == country && e.State_Id == state && e.Region_Id == region).FirstOrDefault();
                FOTName = "";
                SatisfiedCountPercentage = (double)er.SatisfiedCountPercentage;
                GeneralClimate = (double)er.GeneralClimate;
                ColourByClimate = er.ColourByClimate;
                DemographicsWhereThereIsAssociation = er.DemographicsWhereThereIsAssociation.Split(',').ToList(); DemographicsWhereThereIsAssociation.Remove("");
                Ubication = er.Ubication.Split(',').Select(s => s.Split(':')).ToDictionary(key => key[0].Trim(), value => value[1].Trim());
                ClimateByCategories = er.ER_ClimateByCategories.Count == 0 ? null : er.ER_ClimateByCategories.ToDictionary(d => d.Text, d => (double)d.Value);
                PositiveAnswersByPositionLevels = er.ER_AnswersByPositionLevels.Count == 0 ? null : er.ER_AnswersByPositionLevels.ToDictionary(d => d.Text, d => (double)d.Value);
                PositiveAnswersByFOTypes = er.ER_AnswersByFOTypes.Count == 0 ? null : er.ER_AnswersByFOTypes.ToDictionary(d => d.Text, d => (double)d.Value);
                ClimateByBranches = er.ER_ClimateByBranches.Count == 0 ? null : er.ER_ClimateByBranches.ToDictionary(d => d.Text, d => (double)d.Value);
                ClimateByFOTypes = null;
                ClimateByAgeRanges = null;
                Dictionary<string, double[]> GeneralSatNotSat = er.ER_GeneralSatEmployees.Count == 0 ? null : er.ER_GeneralSatEmployees.AsEnumerable().ToDictionary(d => d.Text, d => new double[] { d.Satisfied, (double)d.PctgSatisfied, d.NotSatisfied, (double)d.PctgNotSatisfied });
                Dictionary<string, double[]> CategorySatNotSat = er.ER_CategoriesSatEmployees.Count == 0 ? null : er.ER_CategoriesSatEmployees.AsEnumerable().ToDictionary(d => d.Text, d => new double[] { d.Satisfied, (double)d.PctgSatisfied, d.NotSatisfied, (double)d.PctgNotSatisfied, (double)(d.Average ?? 0), (double)(d.Median ?? 0) });
                Dictionary<string, double[]> LocationSatNotSat = er.ER_LocationsSatEmployees.Count == 0 ? null : er.ER_LocationsSatEmployees.AsEnumerable().ToDictionary(d => d.Text, d => new double[] { d.Satisfied, (double)d.PctgSatisfied, d.NotSatisfied, (double)d.PctgNotSatisfied, (double)(d.Average ?? 0), (double)(d.Median ?? 0) });
                SatNotSat = new Dictionary<string, object>();
                SatNotSat.Add("General", GeneralSatNotSat);
                SatNotSat.Add("Category", CategorySatNotSat);
                SatNotSat.Add("Location", LocationSatNotSat);
                StepwiseValues = er.ER_StepwiseValues.Count == 0 ? null : er.ER_StepwiseValues.ToDictionary(d => d.Text, d => (double)d.Value);
            }

            int[] PositionAndCompaniesCount = analyticalClass.GetPositionAndCompaniesCount();//ranking
            //Dictionary<string, string> ChartSources = analyticalClass.GetChartSources();//obtener las acciones de donde voy a buscar los charts q tengo en los reportes
            int graphicIdPopulationGender = new GraphicsServices().GetByDemographicAndType(analyticalClass.genderPopulation ? "Gender" : "General", "Population").Id;//busco el id del grafico de muestreo ya sea de genero o general
            return new AnalyticalReportViewModel(Test, Print, SatisfiedCountPercentage,
                                    ColourByClimate, GeneralClimate, ClimateByCategories,
                                    PositiveAnswersByPositionLevels, PositiveAnswersByFOTypes,
                                    PositionAndCompaniesCount, StepwiseValues, DemographicsWhereThereIsAssociation,
                                    null, ClimateByBranches, ClimateByFOTypes, ClimateByAgeRanges, SatNotSat,
                                    Ubication, FOTName, country, state, region, graphicIdPopulationGender);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager, FreeReports")]
        public ActionResult AnalyticalReport(int test_id, int? country_id, int? state_id, int? region_id, bool? print)
        {
            if (GetAuthorization(test_id))
            {
                if (print == true)
                    return PartialView(InitializeAnalyticalView(test_id, country_id, state_id, region_id, print));

                return View(InitializeAnalyticalView(test_id, country_id, state_id, region_id, print));
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult AnalyticalReportTest()
        {
            return View(InitializeAnalyticalView(41, null, null, null, null));
        }

        public ActionResult PrintAnalytical(int test_id, int? country_id, int? state_id, int? region_id)
        {
            return View(InitializeAnalyticalView(test_id, country_id, state_id, region_id, true));
            //return View();
        }

        [HttpGet]
        public PdfResult PdfAnalytical(int test_id, int? country_id, int? state_id, int? region_id)
        {
            //return AnalyticalReport(test_id);
            //string content = string.Empty;
            //var view = ViewEngines.Engines.FindView(ControllerContext, "PrintAnalytical", null);
            //using (var writer = new StringWriter())
            //{
            //    var context = new ViewContext(ControllerContext, view.View, ViewData, TempData, writer);
            //    view.View.Render(context, writer);
            //    writer.Flush();
            //    content = writer.ToString();
            //}
            //string html = PrintAnalytical(test_id, country_id, state_id, region_id, true);
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintAnalytical?test_id=" + test_id +
                "&country=" + country_id + "&state=" + state_id + "&region=" + region_id +
                "&print=true", "PdfAnalytical", true);

        }
        #endregion

        #region Ranking

        [HttpPost]
        public ActionResult LoadRankingTab(int questionnaire_id, string demographic, int? FO_id, int? company_id)
        {
            RankingViewModel rvm = InitializeRankingTabs(questionnaire_id, demographic, FO_id, company_id);

            switch (demographic)
            {
                case "Internal":
                    return PartialView("RankingInternal", rvm);
                case "Customer":
                    return PartialView("RankingForAssociated", rvm);
                case "GeneralCountry":
                    return PartialView("RankingByCountry", rvm);
                default:
                    ViewData["option"] = demographic;
                    ViewData["FO_id"] = FO_id;
                    return PartialView("RankingByDemographic", rvm);
            }
        }

        [HttpPost]
        public ActionResult LoadCompanyTabs(int questionnaire, int company)
        {
            RankingViewModel rvm = InitializeRankingTabs(questionnaire, "Tabs", null, company);
            return PartialView("RankingTabs", rvm);
        }

        private RankingViewModel InitializeRankingTabs(int questionnaire_id, string demographic, int? fot, int? company_id)
        {
            User UserLogged = new UsersServices().GetByUserName(User.Identity.Name);
            Dictionary<string, double> ranking = null;
            SelectList sectors = null;
            SelectList companies = null;
            SelectList countries = null;
            Dictionary<int, string> FO = null;
            Dictionary<string, int> demographicsCount = null;
            int company = company_id.HasValue ? company_id.Value : (UserLogged.Role.Name == "HRAdministrator" ? 0 : UserLogged.Company_Id);
            int fo = fot.HasValue ? fot.Value : 0;
            Rankings auxRanking = new Rankings(new QuestionnairesServices().GetById(questionnaire_id),
                            new CompaniesServices().GetById(company_id.HasValue ? company_id.Value : UserLogged.Company_Id), UserLogged);
            if (UserLogged.Role.Name == "HRAdministrator")
            {
                if (company_id.HasValue)
                    ranking = GetInternalRanking(questionnaire_id, company_id.Value, fot, demographic == "Tabs" ? "Gender" : demographic, UserLogged);
                if (demographic == "GeneralCountry" || demographic == "Customer")
                    sectors = new SelectList(new CompanySectorsServices().GetCompanySectorsForDropDownList(), "Key", "Value");
            }
            else if (demographic != "GeneralCountry")
                ranking = GetInternalRanking(questionnaire_id, company, fot, demographic == "Internal" ? "Gender" : demographic, UserLogged);
            switch (demographic)
            {
                case "Internal":
                    if (UserLogged.Role.Name == "HRAdministrator")
                        companies = new SelectList(new CompaniesServices().GetCustomersByAssociatedAndQuestionnaireForDropDownList(UserLogged.Company_Id, questionnaire_id), "Key", "Value");
                    else
                    {
                        demographicsCount = _chartService.GetDemographicsCount(auxRanking.test); //_chartService.GetDemographicsCount(UserLogged.Company_Id);
                        FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(UserLogged.Company_Id);
                    }
                    return new RankingViewModel(questionnaire_id, null, companies, null, demographicsCount, FO, UserLogged, ranking, company, fo); // questionaire, company
                case "Tabs":
                    demographicsCount = _chartService.GetDemographicsCount(auxRanking.test);//_chartService.GetDemographicsCount(company_id.Value);
                    FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(company_id.Value);
                    return new RankingViewModel(questionnaire_id, null, null, null, demographicsCount, FO, UserLogged, ranking, company, fo); // questionaire, company
                case "Customer":
                    return new RankingViewModel(questionnaire_id, sectors, null, null, null, null, UserLogged, ranking, company, fo); // questionnaire, sector
                case "GeneralCountry":
                    countries = new SelectList(new Rankings().GetCountriesForDropdownList(UserLogged), "Key", "Value");
                    return new RankingViewModel(questionnaire_id, sectors, null, countries, null, null, UserLogged, ranking, company, fo); // questionnaire, sector, country
                default:
                    return new RankingViewModel(questionnaire_id, null, companies, countries, null, null, UserLogged, ranking, company, fo); //company, questionnaire
            }
        }

        private RankingViewModel InitializeRankingViewGeneral(User UserLogged, bool print, int questionnaire_id, int sector_id)
        {
            Dictionary<string, double> ranking = null;
            SelectList sectors = null;
            int questionnaire = questionnaire_id;
            SelectList companies = null;
            Dictionary<Company, double> resultsByCompany = new Dictionary<Company, double>();

            if (UserLogged.Role.Name == "HRAdministrator")
            {
                sectors = new SelectList(new CompanySectorsServices().GetCompanySectorsForDropDownList(), "Key", "Value");
                companies = new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(UserLogged.Company_Id), "Key", "Value");
                return new RankingViewModel(questionnaire_id, sectors, companies, null, null, null, UserLogged, ranking, 0, 0);
            }
            else
            {
                ranking = GetExternalRanking(questionnaire, UserLogged.Company.CompanySector_Id, null, "General", UserLogged);
                return new RankingViewModel(questionnaire_id, sectors, companies, null, null, null, UserLogged, ranking, UserLogged.Company_Id, 0);
            }
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public ActionResult Ranking(int questionnaire)
        {
            //if (GetAuthorization(questionnaire))
            //{
            return View(InitializeRankingViewGeneral(new UsersServices().GetByUserName(User.Identity.Name), false, questionnaire, 0));
            //}
            //else
            //    return RedirectToLogOn();
        }

        public ActionResult PrintRanking(int questionnaire_id, int? sector_id, int? country_id, int? company_id, string demographic, int? fot)
        {
            return View(InitializaePrintViewModel(questionnaire_id, sector_id, country_id, company_id, fot, demographic));
        }

        private string GetHttpUrl()
        {
            string http = "http://" + Request.ServerVariables["SERVER_NAME"];
            if (Request.ServerVariables["SERVER_NAME"] != "www.hrmedinet.com")
                http += ":" + Request.ServerVariables["SERVER_PORT"];
            return http;
        }

        [HttpGet]
        public PdfResult PdfRanking(int questionnaire_id, int? sector_id, int? country_id, int? company_id, string demographic, int? fot)
        {
            return new PdfResult(GetHttpUrl() + "/ChartReports/PrintRanking?questionnaire_id=" + questionnaire_id + "&sector_id=" + sector_id
                 + "&country_id=" + country_id + "&company_id=" + company_id + "&demographic=" + demographic + "&fot=" + fot, "PdfRanking", true);
        }

        private RankingViewModel InitializaePrintViewModel(int questionnaire_id, int? sector_id, int? country_id, int? company_id, int? fot, string demographic)
        {
            RankingViewModel rankingVM;
            Rankings rankingClass = new Rankings();
            User UserLogged = new UsersServices().GetByUserName(User.Identity.Name);
            List<string> list = new List<string>();
            List<string> demos = new List<string>();
            Dictionary<int, string> FO = new Dictionary<int, string>();
            Dictionary<string, object> resultRanking = new Dictionary<string, object>();
            Dictionary<string, string> demographicsName = new Dictionary<string, string>();
            string questionnaire, sector, country, company, foname, title, nameTH;
            questionnaire = new QuestionnairesServices().GetById(questionnaire_id).Name;
            sector = sector_id.HasValue && sector_id.Value != 0 ? new CompanySectorsServices().GetById(sector_id.Value).Name : "";
            country = country_id.HasValue && country_id.Value != 0 ? new CountriesServices().GetById(country_id.Value).Name : "";
            company = company_id.HasValue && company_id.Value != 0 ? new CompaniesServices().GetById(company_id.Value).Name : "";
            foname = fot.HasValue && fot.Value != 0 ? new FunctionalOrganizationTypesServices().GetById(fot.Value).Name : "";
            switch (demographic)
            {
                case "External":
                    nameTH = ViewRes.Views.ChartReport.Graphics.CompanyTab;
                    title = ViewRes.Views.ChartReport.Ranking.External;
                    resultRanking = rankingClass.FillRankingDictionary(resultRanking, ViewRes.Views.ChartReport.Ranking.General, GetExternalRanking(questionnaire_id, sector_id, country_id, "General", UserLogged));
                    if (UserLogged.Role.Name == "HRAdministrator")
                        resultRanking = rankingClass.FillRankingDictionary(resultRanking, ViewRes.Views.ChartReport.Ranking.Customers, GetExternalRanking(questionnaire_id, sector_id, country_id, "Customer", UserLogged));
                    resultRanking = rankingClass.FillRankingDictionary(resultRanking, ViewRes.Views.ChartReport.Ranking.ByCountry, GetExternalRanking(questionnaire_id, sector_id, country_id, "GeneralCountry", UserLogged));
                    break;
                case "Internal":
                    title = ViewRes.Views.ChartReport.Ranking.Internal;
                    demographicsName = new Rankings(new CompaniesServices().GetById(company_id.Value)).GetDemographicNames();
                    demos = demographicsName.Keys.ToList();
                    demos.Remove(demos.Last());
                    nameTH = "";
                    FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(UserLogged.Company_Id);
                    foreach (string d in demos)
                    {
                        resultRanking = rankingClass.FillRankingDictionary(resultRanking, demographicsName[d], GetInternalRanking(questionnaire_id, company_id, fot, d, UserLogged));
                    }
                    foreach (KeyValuePair<int, string> fopar in FO)
                    {
                        resultRanking = rankingClass.FillRankingDictionary(resultRanking, fopar.Value, GetInternalRanking(questionnaire_id, company_id, fopar.Key, "FunctionalOrganizationType", UserLogged));
                    }
                    break;
                default:
                    list.Add("General"); list.Add("Customer"); list.Add("GeneralCountry");
                    if (list.Contains(demographic))
                    {
                        nameTH = ViewRes.Views.ChartReport.Graphics.CompanyTab;
                        title = ViewRes.Views.ChartReport.Ranking.External + ": " + GetDeographicNameForExternal(demographic);
                        resultRanking = rankingClass.FillRankingDictionary(resultRanking, title, GetExternalRanking(questionnaire_id, sector_id, country_id, demographic, UserLogged));
                    }
                    else
                    {
                        nameTH = "";
                        demographicsName = new Rankings(new CompaniesServices().GetById(company_id.Value)).GetDemographicNames();
                        title = ViewRes.Views.ChartReport.Ranking.Internal + ": " + (fot.HasValue ? foname : demographicsName[demographic]);
                        resultRanking = rankingClass.FillRankingDictionary(resultRanking, title, GetInternalRanking(questionnaire_id, company_id, fot, demographic, UserLogged));
                    }
                    break;
            }
            rankingVM = new RankingViewModel(questionnaire, sector, country, company, title, nameTH, resultRanking, UserLogged);
            return rankingVM;
        }

        private string GetDeographicNameForExternal(string demographic)
        {
            switch (demographic)
            {
                case "General":
                    return ViewRes.Views.ChartReport.Ranking.General;
                case "Customer":
                    return ViewRes.Views.ChartReport.Ranking.Customers;
                case "GeneralCountry":
                    return ViewRes.Views.ChartReport.Ranking.ByCountry;
                default:
                    return "";
            }
        }

        [HttpPost]
        public JsonResult UpdateRanking(int questionnaire_id, int sector_id, int? country_id, int? company_id, int? fot, string demographic)
        {
            User UserLogged = new UsersServices().GetByUserName(User.Identity.Name);
            List<string> list = new List<string>();
            list.Add("General"); list.Add("Customer"); list.Add("GeneralCountry");
            if (list.Contains(demographic))
            {
                return GetJsonFromDictionary(GetExternalRanking(questionnaire_id, sector_id, country_id, demographic, UserLogged), false);
            }
            else
            {
                return GetJsonFromDictionary(GetInternalRanking(questionnaire_id, company_id, fot, demographic, UserLogged), true);
            }
        }

        private JsonResult GetJsonFromDictionary(Dictionary<string, double> dictionary, bool intern)
        {
            User UserLogged = new UsersServices().GetByUserName(User.Identity.Name);
            string companyName = UserLogged.Company.Name;
            bool userVal = UserLogged.Role.Name == "HRAdministrator";
            List<object> listObj = new List<object>();
            foreach (KeyValuePair<string, double> dic in dictionary)
            {
                listObj.Add(
                    new
                    {
                        companyName = dic.Key,
                        companyClimate = dic.Value,
                        show = userVal || (dic.Key == companyName) || intern
                    });
            }
            return Json(listObj);
        }

        private Dictionary<string, double> GetExternalRanking(int questionnaire_id, int? sector_id, int? country_id, string demographic, User UserLogged)
        {
            bool associated = demographic == "Customer" ? true : false;
            Dictionary<Company, double> resultsByCompany = new Rankings().GetRankingForCompany(UserLogged, new Test(), sector_id, questionnaire_id, country_id, associated);
            Dictionary<string, double> companyC = new Dictionary<string, double>();
            foreach (KeyValuePair<Company, double> companyDouble in resultsByCompany.OrderByDescending(key => key.Value))
            {
                companyC.Add(companyDouble.Key.Name, companyDouble.Value);
            }
            return companyC;
        }

        private Dictionary<string, double> GetInternalRanking(int questionnaire_id, int? company_id, int? fot, string demographic, User UserLogged)
        {
            Questionnaire questionnaire = new QuestionnairesServices().GetById(questionnaire_id);
            Company company = new CompaniesServices().GetById(company_id.HasValue ? company_id.Value : UserLogged.Company_Id);
            Dictionary<string, double> resultsByDemo = new Rankings(questionnaire, company, UserLogged).GetClimateByDemographic(demographic, fot);
            return resultsByDemo;
        }

        #endregion

        public ActionResult ConvertToPDF(int test_id, int? country_id, int? state_id, int? region_id)
        {
            bool print = true;
            string cusomtswitches = string.Format("--print-media-type --ignore-load-errors --header-html {0} --footer-html {1}", new { data = "" }, new { data = "" });
            var printpdf = new ActionAsPdf("AnalyticalReport", new { test_id, country_id, state_id, region_id, print });
            return printpdf;
        }

        public JsonResult LoadQuestionnaires(int test_id)
        {
            List<int> questionnaires = new List<int>();
            questionnaires = new TestsServices().GetById(test_id).GetQuestionnairesByTest().Select(m => m.Id).ToList();
            return Json(questionnaires);

        }
        public int FindMaxAge(IEnumerable<Option> list)
        {

            int maxAge = int.MinValue;
            foreach (Option type in list)
            {
                if (type.Value > maxAge)
                {
                    maxAge = type.Value;
                }
            }
            return maxAge;
        }


        public int GetOptionsByTest(int test_id)
        {
            Test test = new TestsServices().GetById(test_id);
            IEnumerable<Option> options = test.GetOptionsByTest();
            var x = FindMaxAge(options);

            return x;
        }

    }
}



