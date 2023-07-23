

using Microcredit.Models;
using Microcredit.ModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Microcredit.ClassProject.CustomersSVC
{
    public class CustomersSVC : ICustomers
    {
        private readonly ApplicationDbContext _db;
        private bool GetCustomersExistsbyNationalid;
        public CustomersSVC(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;

        }

        public async Task<List<CustomersT>> GETCustomersAsync()
        {
            List<CustomersT> customersT = new List<CustomersT>();

            try
            {

                customersT = await _db.Customers.OrderBy(x => x.CustomerName).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            }
            return customersT;

        }
        public async Task<CustomersT> GETCustomersBYIdAsync(int CustomerId)
        {
            var customerst = (CustomersT)null;
            try
            {
                if (CustomersExists(CustomerId) != false) 
                
                    customerst = await _db.Customers.FindAsync(CustomerId);

                    
                }
            
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return customerst;
        }

        public async Task<ResponseObject> CreateCustomersAsync(CustomersT customersT)
        {
            ResponseObject responseObject = new ResponseObject();


            var GetCustomersExistsbyNationalid = _db.Customers.Where(x => x.CustomerNationalid == customersT.CustomerNationalid).FirstOrDefault();

            //GetCustomersExistsbyNationalid = CustomersExistsbyNationalid(customersT.CustomerNationalid);

            //if (GetCustomersExistsbyNationalid.Equals(true)) {

            if (GetCustomersExistsbyNationalid == null)
            {
                // Will hold all the errors related to registration
                var errorList = new List<string>();  
 
                await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();
                try
                {

                    //var AddCustomers = new CustomersT
                    //{
                    //    CustomerName = customersT.CustomerName,
                    //    CustomerAddress = customersT.CustomerAddress,
                    //    DateissuancenationalID=Convert.ToDateTime( jsonString),
                    //    Notes = customersT.Notes,
                    //    UsersID = 1

                    //};

                    //var jsonString = JsonSerializer.Serialize(customersT.DateissuancenationalID);

                    var result = await _db.Customers.AddAsync(customersT);
                    await _db.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    responseObject.IsValid = true;
                    responseObject.Message = "Added successfully";
                    responseObject.Data = DateTime.Now.ToString();

                }
                catch (Exception ex)
                {

                    Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                    await dbContextTransaction.RollbackAsync();
                    responseObject.IsValid = false;
                    responseObject.Message = "failed";
                    responseObject.Data = DateTime.Now.ToString();

                }
              

            }else if  
              (GetCustomersExistsbyNationalid.CustomerNationalid.Equals(customersT.CustomerNationalid))
                {

                    responseObject.Message = "هذا العميل مسجل مسبقا بكود " + GetCustomersExistsbyNationalid.CustomerId;
                    return responseObject;
                }
            


            
             return responseObject;
        }

        public async Task<bool> UpdateCustomersAsync(int CustomerId, CustomersT customersT)
        {
            ResponseObject responseObject = new();
            if (CustomerId == customersT.CustomerId)
            {

                _db.Entry(customersT).State = EntityState.Modified;

            }
            try
            {
                if (customersT == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;
            }

            catch (Exception ex)
            {
                if (!CustomersExists(CustomerId))

                    Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return false;
            }
        }



        bool CustomersExists(int CustomerId)
        {
            return _db.Customers.Any(x => x.CustomerId == CustomerId);
        }

        bool CustomersExistsbyNationalid(string Nationalid)
        {
                        return _db.Customers.Any(x => x.CustomerNationalid == Nationalid);
        }

        public async Task<bool> DeleteCustomersAsync(int CustomerId)
        {
            var GETCustomerBYId = await _db.Customers.FindAsync(CustomerId);
            ResponseObject responseObject = new();
            if (GETCustomerBYId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.Customers.Remove(GETCustomerBYId);
            _db.SaveChanges();

            return true;
        }

    public IEnumerable<SearchCustomerStatusT> SearchCustomerStatus(string SPName, [Optional] SqlParameter ParamValue)
        {

            var result = _db.searchCustomerStatuses.FromSqlRaw(SPName, ParamValue).ToList();

            return (result);

        }

       
    }
}
