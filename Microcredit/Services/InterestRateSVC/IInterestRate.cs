using Microcredit.ClassProject;
using Microcredit.ModelService;

namespace Microcredit.Services.InterestRateSVC
{
    public interface IInterestRate
    {
        IEnumerable<InterestRate> GetAllInterestAsync(string SPName);
        Task<InterestRate> GETInterestBYIdAsync(int InterestRateId);

        Task<ResponseObject> CreateInterestRateAsync( InterestRate interestRate);

        Task<bool> UpdateInterestRate(int InterestRateId, InterestRate interestRate);

    }
}
