using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.CustomClasses;
using System.IO;

namespace MedinetClassLibrary.Models
{
    public class UnivariateCharts
    {
        public Byte[] Chart2; 

        public UnivariateCharts() { 
        }

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
                details[0] = detailGraphic.Title;
                details[1] = detailGraphic.AxisXName;
                details[2] = detailGraphic.AxisYName;
                details[3] = "";
            }
            return details;
        }

        public FileResult UniVariateChartByAgeRange(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByAgeRanges(category_id, question_id),test_id);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByPerformance(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByPerformanceEvaluations(category_id, question_id), test_id);
            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByLocation(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByLocations(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByPositionLevels(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByPositionLevels(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartBySeniority(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedBySeniorities(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByGender(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByGender(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByInstructionLevel(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByInstructionLevels(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByRegion(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
                              test.GetAvgAndMedByRegions(category_id, question_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        public FileResult UniVariateChartByState(string chartSize, string chartType, int country_id, int test_id, int? category_id, int? question_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(12, 13), 2,
                              test.GetAvgAndMedByStates(null, null, country_id), test_id);

            MemoryStream ms = new MemoryStream();
            cd.chart.SaveImage(ms);
            return new FileContentResult(ms.GetBuffer(), @"image/png");
        }
        //public ActionResult UniVariateChartMapByCountry(string chartSize, string chartType, int graphic_id, int test_id, int? category_id, string name)
        //{
        //    Test test = new TestsServices().GetById(test_id);
        //    ChartDetails cd = new ChartDetails(chartSize, chartType, false, this.GetChartDetails(graphic_id, test_id), 2,
        //                      test.GetAvgAndMedByCountries(category_id, question_id));
        //    MemoryStream ms = new MemoryStream();
        //    cd.chart.SaveImage(ms);
        //    Chart2 = ms.ToArray();
        //    return ContentResult(cd.chart.GetHtmlImageMap(name));
        //}
        //public ActionResult UniVariateChartByCountry(string chartSize, string chartType, int? category_id, int? question_id)
        //{
        //    byte[] data = Chart2 as byte[];
        //    return new FileContentResult(data, @"image/png");
        //}
    }
}
