using Microcredit.ClassProject;
using Microcredit.ModelService;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Microcredit.Services.PaymentOfistallmentsSVC
{
    public interface IPaymentOfistallments
    {

        IEnumerable<ReadPaymentOfistallmentsModel> GetAllPaymentOfistallmentsAsync(string SPName);

        IEnumerable<PaymentOfistallments> GetLonaByidAsync(string SPName, [Optional] SqlParameter ParamValue);

        IEnumerable<T> Calculateremainingamount<T>( int LonaId);


        IEnumerable<UpdateLoanObjectModel> GetDetialsLonawithIDAsync(string SPName, [Optional] SqlParameter ParamValue);

        Task<ResponseObject> CreatePaymentOfistallmentsAsync(PaymentOfistallments paymentOfistallments);

        Task<ResponseObject> CreatePaymentOfistallmentsDetailsAsync(PaymentOfistallmentsDetails paymentOfistallmentsDetails);
        public IEnumerable<PaymentOfistallmentsObject> GetPaymentOfistallmentsByIdAsync(string SPName, [Optional] SqlParameter ParamValue);

        Task<bool> UpdatePaymentOfistallments(int PaymentId, PaymentOfistallments paymentOfistallments);


        Task<ResponseObject> UpdatePayMonthAmount(PaymentOfistallmentsDetails paymentOfistallmentsDetails);


        Task<ResponseObject> ExpeditedPayment(PaymentOfistallments paymentOfistallments);


        Task<ResponseObject> DeleteLona(PaymentOfistallmentsDetails paymentOfistallmentsDetails);
        Task<ResponseObject> DeleteLonaPaymentOfistallments(PaymentOfistallments paymentOfistallments);



    }
}
