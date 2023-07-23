using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Microcredit.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllIStatusLoansController : ControllerBase
    {

        private IAllIStatusLoansReport _executeReport;
        private IConverter _converter;
        private IExecuteReportIssuanceLoansbetweenDate _executeReportIssuanceLoansbetweenDate;
        private readonly IWebHostEnvironment _appEnvironment;


        public AllIStatusLoansController(IAllIStatusLoansReport executeReport ,IConverter converter, IExecuteReportIssuanceLoansbetweenDate  executeReportIssuanceLoansbetweenDate
            ,
            IWebHostEnvironment appEnvironment
            )
        {
            _executeReport = executeReport;
            _converter = converter;
            _executeReportIssuanceLoansbetweenDate = executeReportIssuanceLoansbetweenDate;

            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            string rootPath = _appEnvironment.WebRootPath;
            yield return rootPath;
        }

        [HttpGet("ReportAllIssuanceLoans")]
        public IActionResult ReportAllIssuanceLoans()
        {
 
            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\ReportAllIssuanceLoans.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReport.GetHTMLStringWithoutParam(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {

                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);
            return Ok("Successfully created PDF document.");

        }




        [HttpGet("CountLoansUnderEdit")]
        public IActionResult  CountLoansUnderEdit()
        {

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\LoansUnderEdit.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReport.GetCountLoansUnderEdit(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {

                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);
            return Ok("Successfully created PDF document.");

        }



        [HttpGet("ReportAllIssuanceLoans/{DateFrom}/{DateTo}")]
        public IActionResult GetSIssuanceLoansbetweenDate(DateTime DateFrom, DateTime DateTo)
        {
            var sqlParmsDateFrom = new SqlParameter { ParameterName = "@DateFrom", Value = DateFrom };
            var sqlParmsDateTo = new SqlParameter { ParameterName = "@DateTo", Value = DateTo };
             
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\ReportIssuanceLoansbetweenDate.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReportIssuanceLoansbetweenDate.GetHTMLStringWitmultipleParam(sqlParmsDateFrom, sqlParmsDateTo),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {

                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);
            return Ok("Successfully created PDF document.");

        }

    }
}
