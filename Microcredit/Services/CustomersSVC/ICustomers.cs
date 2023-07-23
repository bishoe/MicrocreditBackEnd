using Microcredit.Models;
using Microcredit.ModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Microcredit.ClassProject.CustomersSVC
{
    public interface ICustomers
    {


        Task<ResponseObject> CreateCustomersAsync(CustomersT customersT);

        Task<bool> UpdateCustomersAsync(int CustomerId, CustomersT customersT);

        Task<List<CustomersT>> GETCustomersAsync();

        Task<CustomersT> GETCustomersBYIdAsync(int CustomerId);
        //Task<IActionResult> SearchCustomerStatus(int CustomerId);

        Task<bool> DeleteCustomersAsync(int CustomerId);

        //public IEnumerable<SearchCustomerStatusT> SearchCustomerStatus(string SPName, [Optional] SqlParameter ParamValue);
        public IEnumerable<SearchCustomerStatusT> SearchCustomerStatus(string SPName, [Optional] SqlParameter ParamValue);

        //public IEnumerable<SearchCustomerStatusT> SearchLonaGuarantorStatus(string SPName, [Optional] SqlParameter ParamValue);

 
    }
}
