using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvoPdf.HtmlToPdf;
using EvoPdf.HtmlToPdf.PdfDocument;
using System.Text.RegularExpressions;
using evointernal;
using System.Web.Security;
namespace System.Web.Mvc
{
    public class PdfResult : ViewResult
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string OutputFileName { get; set; }
        public bool ReturnAsAttachment { get; set; }

        public PdfResult(string html, string outputFileName, bool returnAsAttachment)
        {
            this.ContentType = "application/pdf";
            this.Content = html;
            this.OutputFileName = outputFileName;
            this.ReturnAsAttachment = returnAsAttachment;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = ContentType;
            string baseURL = "";
            string htmlString = this.Content;
            // Create the PDF converter. Optionally you can specify the virtual browser width as parameter.
            //1024 pixels is default, 0 means autodetect
            PdfConverter pdfConverter = new PdfConverter();
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                pdfConverter.HttpRequestCookies.Add(FormsAuthentication.FormsCookieName,
                HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            }
            // set the converter options
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;
            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            pdfConverter.PdfHeaderOptions.HeaderHeight = 0;
            pdfConverter.PdfDocumentOptions.ShowFooter = false;
            pdfConverter.PdfFooterOptions.FooterHeight = 0;
            pdfConverter.PdfDocumentOptions.BottomMargin = 30;
            pdfConverter.PdfDocumentOptions.TopMargin = 20;
            pdfConverter.PdfDocumentOptions.LeftMargin = 20;
            pdfConverter.PdfDocumentOptions.RightMargin = 20;
            pdfConverter.LicenseKey ="LwQeDxwcDx4XGA8dAR8PHB4BHh0BFhYWFg==";
            pdfConverter.NavigationTimeout = 600;
            pdfConverter.AvoidImageBreak = true;
            pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = false;
            //pdfConverter.JavaScriptEnabled = true;
            pdfConverter.ConversionDelay = 300;
            //pdfConverter.InterruptSlowJavaScript = false;
            // Performs the conversion and get the pdf document bytes that you can further
            // save to a file or send as a browser response
            // The baseURL parameterhelps the converter to get the CSS files and images
            // referenced by a relative URL in the HTML string. This option has efect only
            // if the HTML string contains a valid HEAD tag.
            // The converter will automatically inserts a <BASE HREF="baseURL"> tag.
            byte[] pdfBytes = pdfConverter.GetPdfBytesFromUrl(htmlString);
            //byte[] pdfBytes = null;
            //if (baseURL.Length > 0)
            //{
            //    pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(htmlString, baseURL);
            //}
            //else
            //{
            //    pdfBytes = pdfConverter.GetPdfBytesFromUrl(htmlString);
            //}
            // send the PDF document as a response to the browser for download
            // System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            // this.OutputFileName = Regex.Replace(this.OutputFileName, " ", "_", RegexOptions.IgnoreCase);
            // this.OutputFileName = StringHelper.StripNonAlphaNumeric(this.OutputFileName);
            response.AddHeader("Content-Type", "application/pdf");
            if (this.ReturnAsAttachment)
            {
                response.AddHeader("Content-Disposition", "attachment; filename=" + this.OutputFileName + ".pdf; size=" + pdfBytes.Length.ToString());
            }
            //response.Flush();
            response.BinaryWrite(pdfBytes);
            //response.Flush();
            response.End();
        }
    }
}
