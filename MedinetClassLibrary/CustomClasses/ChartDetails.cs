using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.CustomClasses
{
    public class ChartDetails
    {
        public Chart chart;
        public ChartArea chartArea;
        private int width;
        private int height;
        private string chartType;
        private string chartSize;
        private IEnumerable Values;
        private IEnumerable Median;
        private bool tresD;
        private string chartAreaName, axisXName, axisYName, legendName;
        private string demographic;
        private int series;
        public int test_id;
        public int total;
        public int compare_id;
        public int catCount;
        private bool countryTooltip;
        private int options;
        private bool first;
        Dictionary<string, object> Dictionary;
        private float minus;

        public ChartDetails(string chartSize, string chartType, bool tresD, string title, int series,
                            string demographic, IEnumerable Values, int? test_id, int? country_id, int? state_id, int? region_id, bool print)
        {
            this.demographic = demographic;
            int tot = GetEvaluationsByUbication(test_id.Value, country_id, state_id, region_id).Count();
            SetProperties(chartSize, chartType, tresD, title, series, Values, test_id, tot, print);
        }

        public ChartDetails(string chartSize, string chartType, bool tresD, string title, int series,
                            string demographic, IEnumerable Values,int? test_id, bool print)//devuelve el gráfico
        {
            this.demographic = demographic;
            int tot;
            if (demographic == "General" && chartType == "Pie")
                tot = new TestsServices().GetById(test_id.Value).EvaluationNumber;
            else
                tot = new TestsServices().GetById(test_id.Value).Evaluations.Count;
            SetProperties(chartSize, chartType, tresD, title, series, Values, test_id, tot, print);
        }

        public void SetProperties(string chartSize, string chartType, bool tresD, string title, int series,
            IEnumerable Values, int? test_id, int total, bool print)//sirve para poner los valores que se pasaron por parametros en variables que puedan ser usadas en la clase
        {
            IEnumerable Nuevo;
            this.chartSize = chartSize;
            this.chartType = chartType;
            this.chartAreaName = title;
            this.legendName = ViewRes.Controllers.ChartReports.Total;//para que diga total
            this.series = series;

            KeyValuePair<string, double> r = new KeyValuePair<string, double>();
            KeyValuePair<string, double> nr = new KeyValuePair<string, double>();
            if (this.demographic == "General") // para colocar la leyenda de este grafico dependiendo del idioma
            {
                foreach (KeyValuePair<string, double> v in Values)
                {
                    if (v.Key == "Received")
                    {
                        r = new KeyValuePair<string, double>(ViewRes.Views.ChartReport.Graphics.Received.ToString(), v.Value);
                    }
                    else
                    {
                        nr = new KeyValuePair<string, double>(ViewRes.Views.ChartReport.Graphics.NotReceived.ToString(), v.Value);
                    }
                }
                Nuevo = new List<KeyValuePair<string, double>> { r, nr };
            }
            else {
                Nuevo = Values;
            }
            this.Values = Nuevo;
            this.tresD = tresD;
            this.test_id = test_id.Value;
            this.compare_id = 0;
            this.countryTooltip = false;
            //this.demographic = "";
            this.total = total;
            this.minus = print ? 2F : 0F;
            CreateChart();
        }

        public ChartDetails(string chartSize, string chartType, bool tresD, string[] title, int series,
                            Dictionary<string, object> dic, int? test_id, int? compare_id, string demographic,
                            int options, bool print)
        {
            this.minus = print ? 2F : 0F;
            this.chartSize = chartSize;
            this.chartType = chartType;
            this.chartAreaName = title[0];
            this.series = series;
            this.Dictionary = dic;//.OrderByDescending(v => v.Value);
            try
            {
                this.catCount = ((Dictionary<string, double>)dic.First().Value).Count;
            }
            catch
            {
                try
                {
                    IOrderedEnumerable<KeyValuePair<string, double>> kvp = ((IOrderedEnumerable<KeyValuePair<string, double>>)dic.First().Value);
                    this.catCount = kvp.Count();
                }
                catch
                {
                    this.catCount = 1;
                }
            }
            this.axisXName = title[1];
            this.axisYName = title[2];
            legendName = title[3];
            this.tresD = tresD;
            this.test_id = test_id.Value;
            if (compare_id.HasValue)
                this.compare_id = compare_id.Value;
            else
                this.compare_id = 0;
            this.demographic = demographic;
            this.options = options;
            this.countryTooltip = false;
            this.first = true;
            CreateChart();
        }

        public ChartDetails(string chartSize, string chartType, bool tresD, string[] title, int series, Dictionary<string, object> dic, int? test_id, bool print)
        {
            this.minus = print ? 2F : 0F;
            this.chartSize = chartSize;
            this.chartType = chartType;
            this.chartAreaName = title[0];
            this.series = series;
            this.Dictionary = dic;
            this.axisXName = title[1];
            this.axisYName = title[2];
            legendName = title[3];
            this.tresD = tresD;
            this.test_id = test_id.Value;
            this.compare_id = 0;
            options = new TestsServices().GetById(test_id.Value).GetOptionsByTest().Count();// .Questionnaire.Options.Count;//getOptionsByTest chequea si le estoy pasando o no el cuestionario//ya se ´puede usar para varios cuestionarios
            this.demographic = "Bivariate";
            CreateChart();

        }

        private IQueryable<Evaluation> GetEvaluationsByUbication(int test, int? country_id, int? state_id, int? region_id)
        {
            IQueryable<Evaluation> evaluations = new TestsServices().GetById(test).Evaluations.AsQueryable();
            if (country_id.HasValue)
            {
                if (state_id.HasValue)
                    evaluations = evaluations.Where(l => l.Location.State_Id == state_id.Value);
                else
                    evaluations = evaluations.Where(l => l.Location.State.Country_Id == country_id.Value);
            }
            else
                if (region_id.HasValue)
                    evaluations = evaluations.Where(l => l.Location.Region_Id == region_id.Value);
            return evaluations;
        }

        private void CreateChart()
        {
            chart = new Chart();
            SelectSize();
            AddPalette();
            chart.BackColor = ColorTranslator.FromHtml("#cccccc");
            chart.ImageType = ChartImageType.Png;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 2;
            chart.BorderSkin.BackColor = Color.Transparent;
            chart.BorderSkin.PageColor = Color.Transparent;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.Height = height;
            chart.Width = width;


            AddChartArea();
            if (tresD)
                Add3D(chartArea);
            if (chartType != "Pie")
            {
                AddData(Dictionary, ref chart);
                if ((!(demographic == "Category" && compare_id == 0)) && demographic != "AllTests" && !(demographic == "Comparative" && compare_id == 0))
                    SetLegends(ref chart);//elimina el cuadro qe tiene la leyenda
            }
            else
            {
                CreatePie(Values, ref chart);
                Title title = new Title();
                title.Name = ViewRes.Controllers.ChartReports.Total;
                title.Visible = true;
                title.Alignment = ContentAlignment.BottomCenter;
                ElementPosition position = new ElementPosition();
                position.Height = 3;
                position.Y = 95;
                title.Position = position;
                chart.Titles.Add(title);
            }
            
        }

        private void SetLegends(ref Chart chart)
        {
            chart.Legends[0].Title = legendName;
            chart.Legends[0].BackColor = Color.FromArgb(145, 145, 145);
            chart.Legends[0].Docking = Docking.Right;
            chart.Legends[0].IsDockedInsideChartArea = false;
        }

        private void AddData(Dictionary<string, object> Values, ref Chart chart)
        {
            //demographic != "General" && 
            if (demographic != "Bivariate" && !(demographic == "Comparative" && compare_id == 0))//dependendiendo del tipo de demográfico y del reporte quita la paleta de colores para que la paleta del grafico sean los mismos que los del semaforo
                chart.Palette = ChartColorPalette.None;
            int i = 0;
            int valuesCount = Values.Count;
            int pointCount = 0;
            List<string> listFirst = new List<string>();
            if (demographic == "Bivariate")
            {
                foreach (string key in Values.Keys)
                {
                    listFirst.AddRange(((IDictionary<string, double>)Values[key]).Keys.ToList());
                }
                listFirst = listFirst.Distinct().ToList();
            }
            foreach (string key in Values.Keys)
            {
                if (key.ToLower() != "median")
                {
                    Series serie = new Series();
                    DataPoint point;
                    int cont = 0;
                    SelectChartType(ref serie);
                    serie.Name = GetSerieName(key);
                    serie["DrawingStyle"] = "Emboss";//"LightToDark"; //"Cylinder";
                    serie["PointWidth"] = (demographic != "Bivariate" && chartType != "Bar") ? "1" : "0.8";//ancho de la barra
                    serie["YValueType"] = "Double";
                    IEnumerable val = (IEnumerable)Values[key];
                    
                    if (demographic == "Bivariate")
                    {
                        Dictionary<string, double> dictFirst = new Dictionary<string, double>();
                        foreach (string str in listFirst)
                        {
                            if (((Dictionary<string, double>)val).Keys.Contains(str))
                                dictFirst.Add(str, ((Dictionary<string, double>)val)[str]);
                            else
                                dictFirst.Add(str, 0);
                        }
                        val = dictFirst;
                    }

                    foreach (KeyValuePair<string, double> v in val)
                    {
                        string a = v.Key;
                        double d = v.Value;
                        point = new DataPoint();
                        PointProperties(ref point, v);
                        if (series == 1)
                            cont++;
                        serie.Points.Add(point);
                        pointCount++;
                    }
                    if (demographic != "AllTests" && demographic != "Category" && !(demographic == "Comparative" && compare_id == 0))
                    {
                        chart.Legends.Add(key); //SHORTNAMES,aqui pone el nombre de la serie
                        AddLegendStyle(i);
                        i++;
                    }
                    //demographic != "General" && 
                    if (demographic != "Bivariate")
                    {
                        serie.Color = serie.Points[0].Color;
                        serie.Palette = ChartColorPalette.None;
                        if (!first)
                            serie.BackHatchStyle = ChartHatchStyle.BackwardDiagonal;
                        else
                            serie.BackHatchStyle = ChartHatchStyle.None;
                    }
                    chart.Series.Add(serie);
                    ChangeFirstValue();
                    //count++;
                    //if(count == 3)
                    //    break;
                }
            }
        }

        private void ChangeFirstValue()
        {
            if (first)
                first = false;
            else
                first = true;
        }

        private string GetCategoryShortName(string category)
        {
            string shortname = "";
            string[] catvec = category.Split(' ');
            foreach (string word in catvec)
            {
                if (word != "")
                    shortname = shortname + word.Substring(0, 1);
            }
            return shortname;
        }

        private string GetSerieName(string key)
        {
            if (key == "Average")
                return ViewRes.Views.ChartReport.Graphics.Average;
            else
            {
                if (key == "Median")
                    return ViewRes.Views.ChartReport.Graphics.Median;
                else
                    return key;
            }
        }

        private void CreatePie(IEnumerable Values, ref Chart chart)//crea la torta. IEnumerable es un diccionario
        {
            Series serie = new Series();
            DataPoint point;
            int cont = 0;
            SelectChartType(ref serie);
            foreach (KeyValuePair<string, double> value in Values)//KeyValuePair se coloca porque es un registro double y se necesita para poderlo recorrer registro a registro
            {
                point = new DataPoint();
                PointProperties(ref point, value);
                cont++;
                serie.Points.Add(point);
            }
            chart.Legends.Add(serie.Legend);
            chart.Series.Add(serie);        
        }

        private void SelectChartType(ref Series serie)//para decirle cual es el tipo del gráfico o tipo de chart
        {
            switch (chartType)
            {
                case "Area":
                    serie.ChartType = SeriesChartType.Area;
                    break;
                case "Bar":
                    serie.ChartType = SeriesChartType.Bar;
                    break;
                case "Column":
                    serie.ChartType = SeriesChartType.Column;
                    break;
                case "Line":
                    serie.ChartType = SeriesChartType.Line;
                    serie.MarkerSize = 8;
                    serie.MarkerStyle = MarkerStyle.Circle;
                    break;
                case "Pie":
                    serie.ChartType = SeriesChartType.Pie;
                    break;
                case "Radar":
                    serie.ChartType = SeriesChartType.Radar;
                    break;
                default:
                    serie.ChartType = SeriesChartType.Pie;
                    break;
            }
        }

        private void SelectSize()
        {
            //int catCount;
            switch (chartSize)
            {
                case "Screen":
                    if (chartType == "Column" || chartType == "Line")//comumn es para las barras, y line es para una linea como el electrocardiograma
                    {
                        width = 650;
                        if (demographic == "Category" || demographic == "AllTests")
                        {
                            //catCount = ((Dictionary<string, double>)Dictionary[Dictionary.Keys.First()]).Count;
                            if (catCount > 6)
                                width = width + (catCount - 6) * 50;
                        }
                    }
                    else//es una torta
                        width = 550;
                    height = 400;
                    break;
                case "Print"://si va a imprimir
                    width = 450;
                    height = 350;
                    break;
                case "Tooltip"://ejemplo:para la torta de estados del pais X
                    width = 300;
                    height = 200;
                    break;
                case "BigScreen"://biVariate
                    width = 950;
                    height = 500;
                    break;
                default:
                    break;
            }
        }
     
        #region properties
       
        private void AddTitle(string _title)
        {
            Title title = new Title(_title);
            chart.Titles.Add(title);
        }

        private void AddPalette()//aquí estan los colores que van a tomar los gráficos
        {
            //chart.Palette = ChartColorPalette.BrightPastel;
            //chart.Palette = ChartColorPalette.SemiTransparent;
            //chart.Palette = ChartColorPalette.Bright;
            Color[] colorSet = new Color[5] { Color.FromArgb(0,198, 87, 7), Color.FromArgb(0,201, 76, 38), Color.FromArgb(0,142, 68, 2), Color.FromArgb(0,98, 124, 11), Color.FromArgb(0,150, 13, 93) };
            chart.PaletteCustomColors = colorSet;

        }    

        private void AddBorder()
        {
        }
        
        private void AddChartArea()
        {
            chartArea = new ChartArea();
            chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.BackColor = Color.FromArgb(64, 213, 225, 241);
            chartArea.BackSecondaryColor = Color.Transparent;
            chartArea.ShadowColor = Color.Transparent;
            chartArea.BackGradientStyle = GradientStyle.TopBottom;
            chartArea.Name = chartAreaName;//titulo del gráfico
            AddAxis();//se le dan valores a los ejes
            AddTitleStyle(new Font("Verdana,Arial,Helvetica,sans-serif", 11F - minus, FontStyle.Bold));//estilo de la fuente que va a ir en el titulo. estilo del gráfico
            chart.ChartAreas.Add(chartArea);
        }

        private void Add3D(ChartArea chartArea)
        {
            ChartArea3DStyle area3D = new ChartArea3DStyle();
            area3D.Enable3D = true;

            if (chartSize.CompareTo("BigScreen") == 0)
            {
                area3D.PointDepth = 70;
                area3D.Rotation = 20;
                area3D.Inclination = 30;
                area3D.Perspective = 20;
            }
            else {//torta
                area3D.PointDepth = 70;
                area3D.Rotation = 20;
                area3D.Inclination = 40;
                area3D.Perspective = 10;
            }

            area3D.IsRightAngleAxes = false;
            area3D.WallWidth = 0;
            area3D.IsClustered = false;
            chartArea.Area3DStyle = area3D;
        }

        private void AddTitleStyle(Font font)
        {
            chart.Titles.Add(chartAreaName).Font = font;
        }

        private void AddLegendStyle(int i)
        {
            chart.Legends[i].BackColor = Color.FromArgb(150, 147, 147);
            chart.Legends[i].BackSecondaryColor = Color.Transparent;
            chart.Legends[i].Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F - minus, FontStyle.Bold);
        }

        private void AddAxis()//se le dan valores a los ejes
        {
            //obtenemos el valor del eje, dependiendo si el test es en base a 100 o no
            int axisVal = (new TestsServices().GetById(test_id).ResultBasedOn100) ? 100 : options;

            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;

            chartArea.AxisX.TitleFont = new Font("Verdana,Arial,Helvetica,sans-serif", 10F - minus, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Verdana,Arial,Helvetica,sans-serif", 10F - minus, FontStyle.Bold);
            if (chartType != "Pie")
            {
                chartArea.AxisX.Title = axisXName;
                chartArea.AxisY.Title = axisYName;
                chartArea.AxisY.ScaleView.Size = double.Parse((axisVal).ToString());//tamaño de la escala es el número de opciones que tengo, ejemplo;si son 5 opciones va a llegar hasta 5
            }
            else {
                chartArea.AxisX.Title = "";
                chartArea.AxisY.Title = "";
            }
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F - minus, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F - minus, FontStyle.Regular);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisX.LabelStyle.Angle = -45;
        }

        private void PointProperties(ref DataPoint point, KeyValuePair<string, double> value)
        {
            //demographic != "General" && 
            bool SetColorPoint = !(demographic == "Comparative" && compare_id == 0) && demographic != "Bivariate";//para saber si le voy a cambiar el color
            double num = (double)value.Value;//se pasa a numerico porque se necesita hacer un porcentaje
            double numShow = (new TestsServices().GetById(test_id).ResultBasedOn100 && chartType != "Bar" && chartType != "Pie"
                                        && !(demographic == "Comparative" && compare_id == 0)) ? (num * 100 / options) : num;
            //point.YValues = new double[] { double.Parse(value.Value.ToString()) };
            point.YValues = new double[] { numShow };
            if (compare_id != 0 && value.Key == new TestsServices().GetById(test_id).Name)//en vez de colocar el nombre que trae el diccionario coloque General
            {
                point.AxisLabel = "General";
                point.LegendText = "General";
            }
            else if (demographic == "" && chartType == "Pie")
            {
                string viewres = "";
                if (value.Key == "Received")
                    viewres = ViewRes.Classes.Services.EvaluationsReceived;
                else
                    viewres = ViewRes.Classes.Services.EvaluationsNoReceived;
                point.AxisLabel = viewres;
                point.LegendText = viewres;
            }
            else if (demographic == "Gender" && chartType == "Pie")
            {
                string viewres = "";
                if (value.Key == "Female")
                    viewres = ViewRes.Views.Shared.Shared.Female;
                else
                    viewres = ViewRes.Views.Shared.Shared.Male;
                point.AxisLabel = viewres;
                point.LegendText = viewres;
                point.Color = value.Key == "Female" ? Color.HotPink : Color.DeepSkyBlue;
            }
            else
            {
                point.AxisLabel = value.Key;
                point.LegendText = value.Key.ToString();
            }
            double pct;
            if (series == 1)//si series == 1 es porque es una torta o pie
            {
                pct = num * 100 / total;
                if (num > 0)
                    point.Label = pct.ToString("f2") + "% (" + num + ")";
                else
                    point.Label = " ";
            }
            else
            {
                if (num > 0)
                {
                    //double numShow = (new TestsServices().GetById(test_id).ResultBasedOn100 && chartType != "Bar"
                    //                    && !(demographic == "Comparative" && compare_id == 0)) ? (num * 100 / options) : num;
                    point.Label = chartType != "Bar" && !(demographic == "Comparative" && compare_id == 0) ? numShow.ToString("f2") : numShow.ToString("f0");
                }
                else
                    point.Label = "";
            }
            point.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 7F - minus, FontStyle.Bold);
            if (SetColorPoint && options > 0)//si se le cambia el color a los colores del semanforo,del demografico
            {
                if (!first)
                    point.BackHatchStyle = ChartHatchStyle.BackwardDiagonal;
                point.Color = GetColorPoint(num);
            }
            
            Country country = new CountriesServices().GetByName(value.Key.ToString());
            if (country != null)
            {
                string tooltip;
                if (series == 1)
                {
                    tooltip = "<img src=/ChartReports/StatesPercentageChart?chartSize=Tooltip&chartType=Pie&graphic_id=24&country_id=" + country.Id + "&test_id="+test_id+ " />";
                    point.Url = "/ChartReports/StatesPercentageChart?chartSize=Screen&chartType=Pie&graphic_id=24&country_id=" + country.Id + "&test_id=" + test_id;
                }
                else
                {
                    string report = chartType == "Bar" ? "Frequency" : "UniVariate";
                    if (compare_id != 0 && !countryTooltip)
                    {
                        countryTooltip = true;
                        tooltip = "<img src=/ChartReports/UniVariateChartByState?chartSize=Tooltip&chartType=Column&graphic_id=25&country_id=" + country.Id + "&test_id=" + compare_id + " />";
                    }
                    else
                    {
                        tooltip = "<img src=/ChartReports/" + report + "ChartByState?chartSize=Tooltip&chartType=" + chartType + "&graphic_id=25&country_id=" + country.Id + "&test_id=" + test_id + " />";
                    }
                    point.Url = "/ChartReports/" + report + "ChartByState?chartSize=Screen&chartType=" + chartType + "&graphic_id=25&country_id=" + country.Id + "&test_id=" + test_id;
                }
                point.MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + tooltip + "');\" onmouseout=\"DisplayTooltip('');\"";
            }
        }

        private Color GetColorPoint(double value)
        {
            decimal pct = (decimal)(value * 100 / options);
            Test test = new TestsServices().GetById(test_id);
            if (test.ClimateScale_Id.HasValue)
            {
                ClimateRange range = test.ClimateScale.ClimateRanges.Where(r => r.MinValue <= pct && r.MaxValue >= pct).OrderBy(r => r.MaxValue).FirstOrDefault();
                return System.Drawing.ColorTranslator.FromHtml(range.Color);
            }
            else
            {
                if (pct > 80)
                    return Color.Green;
                else if (pct > 60 && pct <= 80)
                    return Color.Yellow;
                else
                    return Color.Red;
            }
        }

        #endregion properties

    }
}
