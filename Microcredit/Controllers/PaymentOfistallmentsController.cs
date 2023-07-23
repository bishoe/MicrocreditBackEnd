using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP;
using Microcredit.Services.AddNewLonaSVC;
using Microcredit.Services.InterestRateSVC;
using Microcredit.Services.PaymentOfistallmentsSVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOfistallmentsController : ControllerBase
    {
        private readonly IPaymentOfistallments _paymentOfistallments;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string paymentOfistallmentsListCacheKey = "paymentOfistallmentsList";
        private ILogger<PaymentOfistallmentsController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private IExecuteReportWithmultipleParam  _executeReportWithmultipleParam;

        private IExecuteReportDuelmentsbetweenDate  _executeReportDuelmentsbetweenDate;

        public PaymentOfistallmentsController(IPaymentOfistallments paymentOfistallments, IConverter converter, IDistributedCache cache, ILogger<PaymentOfistallmentsController> logger, IExecuteReportWithmultipleParam executeReportWithmultipleParam, IExecuteReportDuelmentsbetweenDate executeReportDuelmentsbetweenDate)
        {
            _paymentOfistallments = paymentOfistallments;
            _converter = converter;
            _cache = cache;
            _logger = logger;
            _executeReportWithmultipleParam = executeReportWithmultipleParam;
            _executeReportDuelmentsbetweenDate = executeReportDuelmentsbetweenDate;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentOfistallmentsAsync()
        {
            if (_cache.TryGetValue(paymentOfistallmentsListCacheKey, out IEnumerable<ReadPaymentOfistallmentsModel> readPaymentOfistallmentsModel))
            {
                _logger.Log(LogLevel.Information, "paymentOfistallmentsList list found in cache.");
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("paymentOfistallmentsList", out readPaymentOfistallmentsModel))
                    {
                        _logger.Log(LogLevel.Information, "paymentOfistallments list found in cache.");
                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, "paymentOfistallments list not found in cache. Fetching from database.");
                        readPaymentOfistallmentsModel = _paymentOfistallments.GetAllPaymentOfistallmentsAsync("View_GetAllPaymentOfistallments");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(paymentOfistallmentsListCacheKey, readPaymentOfistallmentsModel, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(readPaymentOfistallmentsModel);

        }

        [HttpPost("CreatePaymentOfistallmentsAsync")]
        public async Task<IActionResult> CreatePaymentOfistallmentsAsync([FromBody] PaymentOfistallments paymentOfistallmentsModel)
        {

            // Will hold all the errors related to registration
            if (paymentOfistallmentsModel is null)
            {
                return BadRequest("paymentOfistallmentsModel is null");

            }
            var result = await _paymentOfistallments.CreatePaymentOfistallmentsAsync(paymentOfistallmentsModel);
            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
             return BadRequest("Cannot Save");


        }
        
        [HttpPost("CreatePaymentOfistallmentsDetails")]
        public async Task<IActionResult> CreatePaymentOfistallmentsDetails([FromBody] PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {

            // Will hold all the errors related to  
            if (paymentOfistallmentsDetails is null)
            {
                return BadRequest("paymentOfistallmentsObject is null");

            }
            var result = await _paymentOfistallments.CreatePaymentOfistallmentsDetailsAsync(paymentOfistallmentsDetails);
            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");


        }

        //SP_GetInfoLonaByLonaIdforpaymentOfistallments
        [HttpGet("GetLonaByidAsync/{PaymentId}")]
        public IActionResult GetLonaByidAsync(int PaymentId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@PaymentId", Value = PaymentId };
            var GetlonawithidObject = _paymentOfistallments.GetLonaByidAsync("dbo.GetpaymentOfistallmentswithPaymentId @PaymentId", sqlParms);

            return Ok(GetlonawithidObject);
        }

        [HttpGet("GetDetialsLonawithIDAsync/{LonaId}")]
        public IActionResult GetDetialsLonawithIDAsync(int LonaId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@LonaId", Value = LonaId };
            var GetlonawithidObject = _paymentOfistallments.GetDetialsLonawithIDAsync("dbo.SP_GetInfoLonaByLonaIdforpaymentOfistallments @LonaId", sqlParms);

            return Ok(GetlonawithidObject);
        }

        [HttpGet("GetPaymentOfistallmentsById/{PaymentId}")]
        public IActionResult GetPaymentOfistallmentsById(int PaymentId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@PaymentId", Value = PaymentId };
            var GetlonawithidObject = _paymentOfistallments.GetPaymentOfistallmentsByIdAsync("dbo.SP_GetPaymentOfistallmentsById @PaymentId", sqlParms);

            return Ok(GetlonawithidObject);
        }


        [HttpGet("GetPaymentOfistallmentsForRemove/{PaymentId}")]
        public IActionResult GetPaymentOfistallmentsForRemove(int PaymentId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@PaymentId", Value = PaymentId };
            var GetlonawithidObject = _paymentOfistallments.GetPaymentOfistallmentsByIdAsync("dbo.SP_GetPaymentOfistallmentsForRemove @PaymentId", sqlParms);

            return Ok(GetlonawithidObject);
        }

        [HttpGet("GetCalculateremainingamount/{LonaId}")]

        public IActionResult GetCalculateremainingamount(int LonaId)
        {
            //("dbo.SP_Calculate_remaining_amount @LonaId", LonaId).AsNoTracking();


            var GetlonawithidObject = _paymentOfistallments.Calculateremainingamount<CalculateRemainingAmountModel>(LonaId).ToList();
            return Ok(GetlonawithidObject);

        }
        //Calculateremainingamount

        [HttpPut("UpdatePayMonthAmount")]
        public async Task<IActionResult> UpdatePayMonthAmount([FromBody] PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _paymentOfistallments.UpdatePayMonthAmount( paymentOfistallmentsDetails);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("ExpeditedPayment")]
        public async Task<IActionResult> ExpeditedPayment([FromBody] PaymentOfistallments paymentOfistallments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
            var result = await _paymentOfistallments.ExpeditedPayment(paymentOfistallments);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }


        [HttpPut("DeleteLonaPaymentOfistallments")]
        public async Task<IActionResult> DeleteLonaPaymentOfistallments([FromBody] PaymentOfistallments paymentOfistallments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _paymentOfistallments.DeleteLonaPaymentOfistallments(paymentOfistallments);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("DeleteLona")]
        public async Task<IActionResult> DeleteLona([FromBody] PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _paymentOfistallments.DeleteLona(paymentOfistallmentsDetails);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }



        [HttpGet("GETpaymentOfistallmentsbetweenDate/{DateFrom}/{DateTo}")]
        public IActionResult GETpaymentOfistallmentsbetweenDate(DateTime DateFrom, DateTime DateTo)
        {
            var sqlParmsDateFrom = new SqlParameter { ParameterName = "@DateFrom", Value = DateFrom };
            var sqlParmsDateTo = new SqlParameter { ParameterName = "@DateTo", Value = DateTo };


            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\paymentOfistallments.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReportWithmultipleParam.GetHTMLStringWitmultipleParam(sqlParmsDateFrom, sqlParmsDateTo),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(),
                "assets", "styles.css") },
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

        [HttpGet("GETDuelmentsbetweenDate/{DateFrom}/{DateTo}")]
        public IActionResult GETDuelmentsbetweenDate(DateTime DateFrom, DateTime DateTo)
        {
            var sqlParmsDateFrom = new SqlParameter { ParameterName = "@DateFrom", Value = DateFrom };
            var sqlParmsDateTo = new SqlParameter { ParameterName = "@DateTo", Value = DateTo };


            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\paymentOfistallments.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReportDuelmentsbetweenDate.GetHTMLStringWitmultipleParam(sqlParmsDateFrom, sqlParmsDateTo),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(),
                "assets", "styles.css") },
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
