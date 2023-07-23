using DinkToPdf;
using DinkToPdf.Contracts; 
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP;
using Microcredit.Services.AddNewLonaSVC;
using Microcredit.Services.InterestRateSVC;
using Microcredit.Services.PaymentOfistallmentsSVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Linq;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AddNewLonaController : ControllerBase
    {
        private  readonly ApplicationDbContext _db;


        private readonly IAddNewLona  _IaddNewLona;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string addNewLonaListCacheKey = "addNewLonaList";
        private ILogger<AddNewLonaController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private IExecuteReport _executeReport;

         

        public AddNewLonaController(IAddNewLona iaddNewLona, ApplicationDbContext db,
            IConverter converter,
            IDistributedCache cache, ILogger<AddNewLonaController> logger ,IExecuteReport executeReport)
        {
            _db = db;
            _IaddNewLona = iaddNewLona;
            _converter = converter;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executeReport = executeReport;
        }

        //TODO check all name view in all contrroller
        [HttpGet]
       public async Task<IActionResult> GetAllLona()
        {

            if (_cache.TryGetValue(addNewLonaListCacheKey, out IEnumerable<TrackLoanObjectModel> trackLoanObjectModel))
            {
                _logger.Log(LogLevel.Information, "Lona list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("Lonalist", out trackLoanObjectModel))
                    {
                        _logger.Log(LogLevel.Information, "Lona list found in cache.");
                    }
                    else
                    {

                        _logger.Log(LogLevel.Information, "Lona list not found in cache. Fetching from database.");
                        trackLoanObjectModel = _IaddNewLona.GetAllLonaAsync("View_TrackLona");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(addNewLonaListCacheKey, trackLoanObjectModel, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(trackLoanObjectModel);
        }

        [HttpGet("SearchLonaGuarantorStatuses/{LonaGuarantorId}")]
        public IActionResult SearchLonaGuarantorStatuses(int LonaGuarantorId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@LonaGuarantorId", Value = LonaGuarantorId };
            var SearchCustomerStatusObject = _IaddNewLona.SearchLonaGuarantorStatuses("dbo.SP_GetNoLonaGuarantor @LonaGuarantorId", sqlParms);

            return Ok(SearchCustomerStatusObject);
        }

        [HttpGet("SearchcanCustomerBeGuanantorStatuses/{customerId}")]
        public IActionResult SearchcanCustomerBeGuanantorStatuses(int customerId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@customerId", Value = customerId };
            var SearchCustomerStatusObject = _IaddNewLona.SearchcanCustomerBeGuanantorStatuses("dbo.SP_SearchcanCustomerBeGuanantorStatuses @customerId", sqlParms);

            return Ok(SearchCustomerStatusObject);
        }

         

        [HttpGet("GetLonaByidAsync/{LonaId}")]
        public IActionResult GetLonaByidAsync(int LonaId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@LonaId", Value = LonaId };
            var GetlonawithidObject = _IaddNewLona.GetLonaByidAsync("dbo.SP_GetTrackLonaByLonaId @LonaId", sqlParms);

            return Ok(GetlonawithidObject);
        }

        [HttpGet("trackLonaWithGuarantorIds/{LonaId}")]
        public IActionResult trackLonaWithGuarantorIds(int LonaId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@LonaId", Value = LonaId };
             var GetlonawithidObject = _IaddNewLona.trackLonaWithGuarantorIds("dbo.SP_GetlonaWithId @LonaId", sqlParms);
             
            return Ok(GetlonawithidObject);
        }

        [HttpPost("CreateNewLona")]
        public async Task<IActionResult> CreateNewLona([FromBody] AddNewLoanObjectModel addNewLoanObjectModel)
        {

            //AddNewLonaMasterModel addNewLonaMaster = new();
            //AddnewLonaDetailsModel addnewLonaDetailsModel = new();

            //var addNewLoanObjectModelresult = await _IaddNewLona.CreateNewLona(addNewLonaMaster, addnewLonaDetailsModel, addNewLoanObjectModel);
             var _addNewLoanObjectModelresult =  await _IaddNewLona.CreateNewLona(addNewLoanObjectModel);
             if (_addNewLoanObjectModelresult.IsValid)

            {
                return Ok(new { Message = "Added successfully", addNewLoanObjectModel.LonaId });
              
            }
            return BadRequest("Cannot Save");

        }

        [HttpPost("CreateNewLonaMaster")]

        public async Task<IActionResult> CreateNewLonaMaster([FromBody] AddNewLoanObjectModel addNewLoanObjectModel)
        {

 
            //var addNewLoanObjectModelresult = await _IaddNewLona.CreateNewLona(addNewLonaMaster, addnewLonaDetailsModel, addNewLoanObjectModel);
            var addNewLoanObjectModelresult = await _IaddNewLona.CreateNewLonaMaster(addNewLoanObjectModel);
            //var _addNewLoanObjectModelresult = await _IaddNewLona.CreateNewLona(addNewLoanObjectModel);
             if (addNewLoanObjectModelresult.IsValid)
            {
                return Ok(new { Message = "Added successfully", addNewLoanObjectModel.LonaId });

            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("UpdateLonaMasterAsync/{LonaId}")]

        public async Task<IActionResult> UpdateLonaMasterAsync(int LonaId, [FromBody] AddNewLonaMasterModel  addNewLonaMasterModel   )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _IaddNewLona.UpdateMasterLonaAsync(LonaId, addNewLonaMasterModel);

            //if (!result)
            //{
            //    return BadRequest();
            //}

            return NoContent();
        }
        //[HttpPut("UpdateLonaAsync/{LonaId}")]

        [HttpPut("IssuanceLonaAsync")]
        public async Task<IActionResult> IssuanceLonaAsync(   [FromBody] IssuanceLonaModel issuanceLonaModel  )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _IaddNewLona.IssuanceLonaAsync(  issuanceLonaModel);

            //if (!result)
            //{
            //    return BadRequest();
            //}

            return NoContent();
        }
        //TODO Add Existis Function before start update
        [HttpPut("UpdateLonaAsync")]
        public async Task<IActionResult> UpdateLonaAsync([FromBody] AddnewLonaDetailsModel addnewLonaDetailsModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _IaddNewLona.UpdateLona(    addnewLonaDetailsModel);

            //if (!result)
            //{
            //    return BadRequest();
            //}

            return NoContent();
        }

        //[HttpDelete("DeleteLonaGuarantorAsync/{lonaDetailsId}")]
        //public async Task<IActionResult> DeleteLonaGuarantorAsync(int lonaDetailsId)
        //{
        //    if (!ModelState.IsValid) return BadRequest();
        //    var GETlonaDetailsId = await _IaddNewLona.DeleteLonaGuarantorAsync(lonaDetailsId);
        //    if (!GETlonaDetailsId) return BadRequest("Error Id IS NULL");
        //    return Ok();
        //}


        [HttpPut("ChangeStatusMasterLona/{LonaId}")]
        public async Task<IActionResult> ChangeStatusMasterLona(int LonaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _IaddNewLona.ChangeStatusMasterLona(LonaId);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }
        [HttpDelete("DeleteLonaMasterAsync/{LonaId}")]
        public async Task<IActionResult> DeleteLonaMasterAsync(int LonaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _IaddNewLona.DeleteLonaAsync(LonaId);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }
         
         [HttpDelete("DeleteLonaDetailsAsync/{LonaDetailsId}")]
        public async Task<IActionResult> DeleteLonaDetailsAsync(int LonaDetailsId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _IaddNewLona.DeleteLonaDetailsAsync(LonaDetailsId);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("ReportAllinfoAboutcustomer/{CustomerId}")]
        public IActionResult ReportBRANCHEBranchCode(int CustomerId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@CustomerId", Value = CustomerId };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\ReportAllinfoAboutcustomer.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeReport.GetHTMLString(sqlParms),
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
