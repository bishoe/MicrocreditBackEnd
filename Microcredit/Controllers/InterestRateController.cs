using DinkToPdf.Contracts;
using Microcredit.BindingModel.DTO;
using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP;
using Microcredit.Services.InterestRateSVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestRateController : ControllerBase
    {
        private readonly IInterestRate _interestRatee;
        private IConverter _converter;
        //private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string interestRateeListCacheKey = "interestRateeList";
        private ILogger<InterestRateController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private readonly UserManager<Appuser> _userManager;

        public InterestRateController(IInterestRate interestRatee, IConverter converter, IDistributedCache cache, ILogger<InterestRateController> logger, UserManager<Appuser> userManager)
        {
            _interestRatee = interestRatee;
            _converter = converter;
            _cache = cache;
            _logger = logger;
            _userManager = userManager;

        }
        //[Authorize(Roles = "user")]

        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GetAllInterestAsync()
        {


            List<UserDTO> allUserDTO = new List<UserDTO>();
            var users = _userManager.Users.ToList();
        
                if (_cache.TryGetValue(interestRateeListCacheKey, out IEnumerable<InterestRate>? interestRates))
            {
                _logger.Log(LogLevel.Information, "interestRate list found in cache.");

            }
            else
            {

                try
                {  
                    foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).ToList();
                if (role.Any(x => x == "user"))
                {
                   
              
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("interestRatelist", out interestRates))
                    {
                        _logger.Log(LogLevel.Information, "interestRate list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "interestRate list not found in cache. Fetching from database.");
                        interestRates = _interestRatee.GetAllInterestAsync("dbo.GetAllInterestRate");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(interestRateeListCacheKey, interestRates, cacheEntryOptions);

                    }  }
            }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(interestRates);

        }

        [HttpPost]
        public async Task<IActionResult> CreateInterestRateAsync([FromBody] InterestRate  interestRate)
        {

            // Will hold all the errors related to registration
            if (interestRate is null )
            {
                return BadRequest("interestRate is null");

            }
            var result = await _interestRatee.CreateInterestRateAsync(interestRate);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            _cache.Remove(interestRateeListCacheKey);
            return BadRequest("Cannot Save");


        }
        [HttpGet("{InterestRateId}")]
        public async Task<IActionResult> GETInterestRateBYIdAsync(int InterestRateId)
        {
            if (InterestRateId == 0) return NotFound();
            var GetInterestRateId = await _interestRatee.GETInterestBYIdAsync(InterestRateId);

            return Ok(GetInterestRateId);

        }
        [HttpPut("{InterestRateId}")]
        public async Task<IActionResult> UpdateBranches(int InterestRateId, [FromBody] InterestRate interestRate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _interestRatee.UpdateInterestRate(InterestRateId, interestRate);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }

}
 