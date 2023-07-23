using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.BindingModel.DTO;
using Microcredit.ClassProject.CustomersSVC;
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
 
namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomers _customers;
        private IExecutCountCustomersReport _executeReport;
        private IConverter _converter;
        private readonly UserManager<Appuser> _userManager;
         public CustomersController(ApplicationDbContext db,
            ICustomers customers, IConverter converter
            , IExecutCountCustomersReport executeReport,UserManager<Appuser> userManager   )
        {
            _db = db;
            _customers = customers;
            _executeReport = executeReport;
            _converter = converter;
            _userManager = userManager;
 
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GETCustomersAsync()
        {

            List<CustomersT> GETCustomers = new List<CustomersT>();

            GETCustomers = await _customers.GETCustomersAsync();

            return Ok(GETCustomers);

        }


        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GETCustomersBYIdAsync(int CustomerId)
        {
            if (CustomerId == 0) return NotFound();

            var GetCustomesById = await _customers.GETCustomersBYIdAsync(CustomerId);

            return Ok(GetCustomesById);

        }
        //[HttpGet("{}")]
        //[HttpGet]
        //[Authorize]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "John Doe", "Jane Doe" };
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCustomers([FromBody] CustomersT customersT)
        {
            var result = await _customers.CreateCustomersAsync(customersT);
            //return Ok(new { Message = "Added successfully", addNewLoanObjectModel.LonaId });


            //if (result.Message =="هذا العميل مسجل مسبقا بكود " + customersT.CustomerId)
            if (result.Message != "Added successfully")

                return Ok(new { Message = ' '+ result.Message }) ;

            if (result.IsValid || result.Message == "Added successfully")
            {

                return Ok(new { Message = "Added successfully" });

            }
            return BadRequest("Cannot Save");

        }


        [HttpGet("SearchCustomerStatus/{CustomerId}")]
        public IActionResult SearchCustomerStatus(int CustomerId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@CustomerId", Value = CustomerId };
            var SearchCustomerStatusObject = _customers.SearchCustomerStatus("dbo.SP_SearchCustomerStatusById @CustomerId", sqlParms);

            return Ok(SearchCustomerStatusObject);
        }


        [HttpGet("SearchLonaGuarantorStatus/{LonaGuarantor}")]
        public IActionResult SearchLonaGuarantorStatus(int LonaGuarantor)
        {
            var sqlParms = new SqlParameter { ParameterName = "@LonaGuarantorId", Value = LonaGuarantor };
            var SearchCustomerStatusObject = _customers.SearchCustomerStatus("dbo.SP_SearchLonaGuarantorId @LonaGuarantorId", sqlParms);

            return Ok(SearchCustomerStatusObject);
        }


        [HttpPut("{CustomerId}")]
        public async Task<IActionResult> UpdateCustomers(int CustomerId, [FromBody] CustomersT customersT)
        {

            if (!ModelState.IsValid) return BadRequest();

            var result = await _customers.UpdateCustomersAsync(CustomerId, customersT);
            if (!result)
            {
                return BadRequest();
            }


            return NoContent();


        }


        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> DeleteCustomers(int CustomerId)
        {


            if (!ModelState.IsValid) return BadRequest();

            var GETCustomerBYId = await _customers.DeleteCustomersAsync(CustomerId);
            if (!GETCustomerBYId) return BadRequest();



            return Ok();
        }


        [HttpGet("ReportGetCountCustomers")]
        public IActionResult ReportGetCountCustomers()
        {


            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"C:\inetpub\wwwroot\MicrolonaFront\assets\Report\ReportCountCustomers.pdf"
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
    }
}

 