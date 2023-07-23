using Microcredit.ClassProject;
using Microcredit.ModelService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Microcredit.Services.PaymentOfistallmentsSVC
{
    public class PaymentOfistallmentsSVC : IPaymentOfistallments
    {
        private readonly ApplicationDbContext _db;
        public PaymentOfistallmentsDetails AddPaymentOfistallmentsDetails;
        private static int GetCurrentId;
        public PaymentOfistallmentsSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ResponseObject> CreatePaymentOfistallmentsAsync(PaymentOfistallments paymentOfistallments)
        {
            ResponseObject responseObject = new();

            //await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            //{


            if (paymentOfistallments != null && paymentOfistallments.LonaId != 0 && paymentOfistallments.ProdouctsID != 0)
                try
                {
                    //for (int i = 1; i < paymentOfistallments.MonthNumber; i++)
                    //{


                    var _AddpaymentOfistallmentsModel = new PaymentOfistallments
                    {
                        CustomerId = paymentOfistallments.CustomerId,

                        LonaAmount = paymentOfistallments.LonaAmount,
                        UserId = paymentOfistallments.UserId,
                        DateAdd = paymentOfistallments.DateAdd,
                        IsDelete = paymentOfistallments.IsDelete,
                        MonthNumber = paymentOfistallments.MonthNumber,
                        ProdouctsID = paymentOfistallments.ProdouctsID,
                        LonaId = paymentOfistallments.LonaId,
                       AmountBeforeDiscount =paymentOfistallments.AmountBeforeDiscount,
                        AmountAfterDiscount=paymentOfistallments.AmountAfterDiscount,
                        DiscountPercentage =paymentOfistallments.DiscountPercentage,
                        ExpeditedPaymentDate= paymentOfistallments.ExpeditedPaymentDate,
                        StatusLona =paymentOfistallments.StatusLona 
 

                    };
                     var result =   _db.paymentOfistallments.Add(_AddpaymentOfistallmentsModel);
                    await _db.SaveChangesAsync();
                    GetCurrentId = _AddpaymentOfistallmentsModel.PaymentId;


                    //}




                    //await dbContextTransaction.CommitAsync();
                    //var GetCurrentId = AddpaymentOfistallmentsModel.PaymentId;

                    responseObject.IsValid = true;
                    //responseObject.Message = ">" + GetCurrentId;
                    responseObject.Message = "Added successfully";

                    responseObject.Data = DateTime.Now.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                          ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                    //await dbContextTransaction.RollbackAsync();

                    responseObject.IsValid = false;
                    responseObject.Message = "failed";
                    responseObject.Data = DateTime.Now.ToString();
                }
            //GC.Collect();
            //}
            return responseObject;
        }

        public IEnumerable<ReadPaymentOfistallmentsModel> GetAllPaymentOfistallmentsAsync(string SPName)
        {
            return _db.readPaymentOfistallmentsModels.FromSqlRaw("select * from " + SPName).ToList();

        }

        public async Task<bool> UpdatePaymentOfistallments(int PaymentId, PaymentOfistallments paymentOfistallmentsModel)
        {

            ResponseObject responseObject = new();

            if (PaymentId == paymentOfistallmentsModel.PaymentId)
            {
                //_db.Entry(PaymentId).State = EntityState.Modified;
                 _db.Entry(paymentOfistallmentsModel).State = EntityState.Modified;
            }
            try
            {
                if (PaymentId == 0)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {

                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
            ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                GC.Collect();

                return false;
            }


        }




      


        public async Task<ResponseObject> ExpeditedPayment(PaymentOfistallments paymentOfistallments)
        {
            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();
            try
            {
                PaymentOfistallments _PaymentOfistallments = new();
               //هنا
               //     كل 
               //     اوبجيكت
               //     بيجيب 
               //     داتا 
               //     مختلفه

                _PaymentOfistallments = _db.paymentOfistallments.FirstOrDefault
                 (x => x.LonaId == paymentOfistallments.LonaId);

                if (_PaymentOfistallments != null)
                {
                 
                 _PaymentOfistallments.DiscountPercentage = paymentOfistallments.DiscountPercentage; _PaymentOfistallments.AmountBeforeDiscount = paymentOfistallments.AmountBeforeDiscount;
                _PaymentOfistallments.AmountAfterDiscount = paymentOfistallments.AmountAfterDiscount;
                  _PaymentOfistallments.StatusLona = paymentOfistallments.StatusLona;
                _PaymentOfistallments.ExpeditedPaymentDate = paymentOfistallments.ExpeditedPaymentDate;
 

                 await _db.SaveChangesAsync();

                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();
                }
            }



            catch (Exception ex)
            {


                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return responseObject;
            }
            return responseObject;


        }

        //public IEnumerable<UpdateLoanObjectModel> GetLonaByid(string SPName, [Optional] SqlParameter ParamValue)
        //{
        //    var result = _db.updateLoanObjectModels.FromSqlRaw(SPName, ParamValue).AsNoTracking();

        //    return result;
        //}


        public IEnumerable<PaymentOfistallments> GetLonaByidAsync(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.paymentOfistallments.FromSqlRaw(SPName, ParamValue).AsNoTracking();

            return result;
        }

        public IEnumerable<UpdateLoanObjectModel> GetDetialsLonawithIDAsync(string SPName, [Optional] SqlParameter ParamValue)
        {
            try
            {
                var result = _db.updateLoanObjectModels.FromSqlRaw(SPName, ParamValue).AsNoTracking();

                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseObject> CreatePaymentOfistallmentsDetailsAsync(PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {

            ResponseObject responseObject = new();
            //if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            //await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            //{
            for (int i = 1; i < paymentOfistallmentsDetails.MonthNumber; i++)
            {
                //GetLastId = _db.addNewLonaMasters.Max(a => a.LonaId);

                AddPaymentOfistallmentsDetails = new PaymentOfistallmentsDetails
                {
                    AmountPaid = paymentOfistallmentsDetails.AmountPaid,
                    AmountRemaining = paymentOfistallmentsDetails.AmountRemaining,
                    IstalmentsAmount = paymentOfistallmentsDetails.IstalmentsAmount,
                    NoIstalments = paymentOfistallmentsDetails.NoIstalments,
                    MonthNumber = paymentOfistallmentsDetails.MonthNumber,
                    PaymentId = GetCurrentId,
                    DateAdd = paymentOfistallmentsDetails.DateAdd,
                    StatusIstalments = paymentOfistallmentsDetails.StatusIstalments,
                    DueDate = paymentOfistallmentsDetails.DueDate



                };
            }

            var resultAddaddNeewLoanModel = await _db.PaymentOfistallmentsDetails.AddAsync(AddPaymentOfistallmentsDetails);

            await _db.SaveChangesAsync();

            _db.Database.CloseConnection();
            responseObject.IsValid = true;
            responseObject.Message = "Added successfully";
            responseObject.Data = DateTime.Now.ToString();


            //}
            if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;


        }

        public IEnumerable<PaymentOfistallmentsObject> GetPaymentOfistallmentsByIdAsync(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.PaymentOfistallmentsObjects.FromSqlRaw(SPName, ParamValue).AsNoTracking();

            return result;


        }

        public async Task<ResponseObject> UpdatePayMonthAmount(PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {

            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            try
            {
                PaymentOfistallmentsDetails _paymentOfistallmentsDetails = new();

                _paymentOfistallmentsDetails = _db.PaymentOfistallmentsDetails.FirstOrDefault
                 (x => x.PaymentIdDetails == paymentOfistallmentsDetails.PaymentIdDetails);


                if (_paymentOfistallmentsDetails != null)
                {
                    _db.Entry(_paymentOfistallmentsDetails).State = EntityState.Detached;
                }

                _paymentOfistallmentsDetails = new PaymentOfistallmentsDetails()
                {
                    AmountPaid = paymentOfistallmentsDetails.AmountPaid,
                    AmountRemaining = paymentOfistallmentsDetails.AmountRemaining,
                    DateAdd = DateTime.Now,
                    StatusIstalments = paymentOfistallmentsDetails.StatusIstalments,
                    IstalmentsAmount = paymentOfistallmentsDetails.IstalmentsAmount,
                    PaymentIdDetails = paymentOfistallmentsDetails.PaymentIdDetails,
                    MonthNumber = paymentOfistallmentsDetails.MonthNumber,
                    NoIstalments = paymentOfistallmentsDetails.NoIstalments,
                    PaymentId = paymentOfistallmentsDetails.PaymentId

                };

                //_paymentOfistallmentsDetails = paymentOfistallmentsDetails;


                _db.PaymentOfistallmentsDetails.Update(_paymentOfistallmentsDetails);
                await _db.SaveChangesAsync();
                //var resultMasterProductsWarehouse = _db.addNewLonaMasters.Update(addNewLonaMasterModel);
                //_db.SaveChanges();
                //GetAddMasterid = addNewLonaMasterModel.LonaId;
                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();

            }


            catch (Exception ex)
            {


                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return responseObject;
            }
            return responseObject;


        }


        public async Task<ResponseObject> DeleteLonaPaymentOfistallments(PaymentOfistallments paymentOfistallments)
        {
            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            try
            {
                PaymentOfistallments _paymentOfistallments = new();

                _paymentOfistallments = _db.paymentOfistallments.FirstOrDefault
                 (x => x.PaymentId == _paymentOfistallments.PaymentId);


                if (_paymentOfistallments != null)
                {
                    _db.Entry(_paymentOfistallments).State = EntityState.Detached;
                }

                _paymentOfistallments = new PaymentOfistallments()
                {
                    IsDelete = true

                };

                _paymentOfistallments = paymentOfistallments;


                _db.paymentOfistallments.Update(_paymentOfistallments);
                await _db.SaveChangesAsync();

                //var resultMasterProductsWarehouse = _db.addNewLonaMasters.Update(addNewLonaMasterModel);
                //_db.SaveChanges();
                //GetAddMasterid = addNewLonaMasterModel.LonaId;
                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();
            }



            catch (Exception ex)
            {


                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return responseObject;
            }
            return responseObject;



        }

        public async Task<ResponseObject> DeleteLona(PaymentOfistallmentsDetails paymentOfistallmentsDetails)
        {

            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            try
            {
                PaymentOfistallmentsDetails _paymentOfistallmentsDetails = new();

                _paymentOfistallmentsDetails = _db.PaymentOfistallmentsDetails.FirstOrDefault
                 (x => x.PaymentIdDetails == paymentOfistallmentsDetails.PaymentIdDetails);


                if (_paymentOfistallmentsDetails != null)
                {
                    _db.Entry(_paymentOfistallmentsDetails).State = EntityState.Detached;
                }

                for (int i = 0; i < paymentOfistallmentsDetails.MonthNumber; i++)
                {


                    _paymentOfistallmentsDetails = new PaymentOfistallmentsDetails()
                    {
                        StatusIstalments = paymentOfistallmentsDetails.StatusIstalments,
                        PaymentIdDetails = paymentOfistallmentsDetails.PaymentIdDetails,
                        MonthNumber = paymentOfistallmentsDetails.MonthNumber,

                    };

                    _paymentOfistallmentsDetails = paymentOfistallmentsDetails;


                    _db.PaymentOfistallmentsDetails.Update(paymentOfistallmentsDetails);
                    await _db.SaveChangesAsync();
                }
                //var resultMasterProductsWarehouse = _db.addNewLonaMasters.Update(addNewLonaMasterModel);
                //_db.SaveChanges();
                //GetAddMasterid = addNewLonaMasterModel.LonaId;
                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();

            }


            catch (Exception ex)
            {


                Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
 ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return responseObject;
            }
            return responseObject;


        }

  public IEnumerable<T>Calculateremainingamount<T>( int LonaId)
        {

            //var result = _db.paymentOfistallments.FromSqlRaw(SPName, LonaId).AsNoTracking();
            //_db.Dispose();

            var sqlParms = new SqlParameter { ParameterName = "@LonaId", Value = LonaId };

            var GETAllInfoAboutcustomerLoan = _db.calculateRemainingAmountModels.FromSqlRaw("dbo.SP_Calculate_remaining_amount @LonaId", sqlParms).AsNoTracking();

            return (IEnumerable<T>)GETAllInfoAboutcustomerLoan;


        }

       
    }
}
