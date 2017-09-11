using EvoPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System_Realizacji_Zamowien.Helpers
{
    public static class HtmlToPdfConverter
    {
        public static byte[] DownloadPdf(Controller controller, string viewName, object model)
        {
            string html;
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                html = sw.GetStringBuilder().ToString();
            }
            return Download(html);
        }
        private static byte[] Download(string html)
        {
            //initialize the PdfConvert object
            PdfConverter pdfConverter = new PdfConverter();

            // set the license key - required
            pdfConverter.LicenseKey = "B4mYiJubiJiInIaYiJuZhpmahpGRkZE=";

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConverter.PdfDocumentOptions.ShowHeader = false;
            pdfConverter.PdfDocumentOptions.ShowFooter = false;

            // get the pdf bytes from html string
            byte[] pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(html);
            return pdfBytes;
        }

    }
}