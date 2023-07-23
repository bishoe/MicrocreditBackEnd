using Castle.Core.Resource;
using Microcredit.Models;
using Microcredit.ModelService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ModelService;
using Serilog;
using Stimulsoft.Blockly.Blocks.Maths;
using Stimulsoft.Blockly.Model;
using System;
using System.Runtime.InteropServices;

namespace Microcredit.Services.AddNewLonaSVC
{
    public class AddNewLonaSVC : IAddNewLona
    {
        private readonly ApplicationDbContext _db;
        private int GetLastId;
        public static int GetAddMasterid;
        public AddnewLonaDetailsModel addnewLonaDetailsModel;
        private enum EnumLonaGuarantorId
        {
            AllLonaGuarantorId = 2
        }
        public AddNewLonaSVC(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// GetNewLonaId from Master pass this to details
        /// </summary>
        public int GetNewLonaId;
        public async Task<ResponseObject> CreateNewLona(AddNewLoanObjectModel addNewLoanObjectModel)
        {
            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            //await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            //{
            for (int i = 0; i < addNewLoanObjectModel.Nocolumn; i++)
            {
                GetLastId = _db.addNewLonaMasters.Max(a => a.LonaId);

                addnewLonaDetailsModel = new AddnewLonaDetailsModel
                {
                    LonaGuarantorId = addNewLoanObjectModel.LonaGuarantorId,
                    LonaId = GetAddMasterid

                };
            }

            var resultAddaddNeewLoanModel = await _db.addnewLonaDetails.AddAsync(addnewLonaDetailsModel);

            await _db.SaveChangesAsync();

            _db.Database.CloseConnection();
            responseObject.IsValid = true;
            responseObject.Message = "Added successfully";
            responseObject.Data = DateTime.Now.ToString();
            GetAddMasterid = 0;


            //}
            if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;

        }
         
        public async Task<ResponseObject> CreateNewLonaMaster(AddNewLoanObjectModel addNewLoanObjectModel)
        {
            ResponseObject responseObject = new();

            await using (var dbContextTransaction = await _db.Database.BeginTransactionAsync())
            {

                if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

                try
                {
                    var _AddNewLonaMasterModel = new AddNewLonaMasterModel
                    {
                        ProdcutId = addNewLoanObjectModel.ProdcutId,
                        CustomerId = addNewLoanObjectModel.CustomerId,
                        InterestRateid = addNewLoanObjectModel.InterestRateid,
                        MonthNumber = addNewLoanObjectModel.MonthNumber,
                        StatusLona = addNewLoanObjectModel.StatusLona,
                        UserID = addNewLoanObjectModel.UserID = "1",
                        DateAdd = Convert.ToDateTime(DateTime.UtcNow.ToString()),
                        AmountBeforeAddInterest = addNewLoanObjectModel.AmountBeforeAddInterest

                    }
                        ;
                    //CountColumn = CountColumn + 1;



                    var resultMasterProductsWarehouse = await _db.addNewLonaMasters.AddAsync(_AddNewLonaMasterModel);
                    await _db.SaveChangesAsync();
                    GetAddMasterid = _AddNewLonaMasterModel.LonaId;
                    responseObject.IsValid = true;
                    responseObject.Message = "Added successfully";
                    responseObject.Data = DateTime.Now.ToString();

                }
                //}
                #region catch
                catch (Exception ex)
                {

                    Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                    await dbContextTransaction.RollbackAsync();
                    responseObject.IsValid = false;
                    responseObject.Message = "failed";
                    responseObject.Data = DateTime.Now.ToString();
                    GC.Collect();

                }

                #endregion

            }

            GetLastId = _db.addNewLonaMasters.Max(a => a.LonaId);
            //if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;
        }
    
      
        public async Task<bool> DeleteLonaGuarantorAsync(int lonaDetailsId)

        {
            var GetlonaDetailsId = await _db.addnewLonaDetails.FindAsync(lonaDetailsId);

            try
            {
                ResponseObject responseObject = new();

                if (GetlonaDetailsId != null)
                {
                    _db.addnewLonaDetails.Remove(GetlonaDetailsId);
                    _db.SaveChanges();
                    return true; 
                }
                   responseObject.Message = "Error Id IS NULL";
                    return false;
            }
            catch (Exception ex)
            {


                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return false;
        }
        public async Task<bool> DeleteLonaAsync(int lonaId)

        {
            var GetlonaId = await _db.addNewLonaMasters.FindAsync(lonaId);

            try
            {
                ResponseObject responseObject = new();

                if (GetlonaId != null)
                {
                    _db.addNewLonaMasters.Remove(GetlonaId);
                    _db.SaveChanges();
                    //await DeleteLonaDetailsAsync(lonaId);
                    return true;
                }
                responseObject.Message = "Error Id IS NULL";
                return false;
            }
            catch (Exception ex)
            {


                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return false;
        }

        public async Task<bool> DeleteLonaDetailsAsync(int LonaDetailsId)

        {
            //All LonaGuarantorId = 2 
            var GetlonaId = await _db.addnewLonaDetails.FindAsync(LonaDetailsId);
             try
            {
                ResponseObject responseObject = new();
                //_db.Entry(_db.addnewLonaDetails).State = EntityState.Detached;

                if (GetlonaId != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                    _db.addnewLonaDetails.Remove(GetlonaId);
                    _db.SaveChanges();

                    }
                    return true;
                }
                responseObject.Message = "Error Id IS NULL";
                return false;
            }
            catch (Exception ex)
            {


                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return false;
        }
        public IEnumerable<TrackLoanObjectModel> GetAllLonaAsync(string SPName)
        {

            return _db.TrackLoanObjectModels.FromSqlRaw("select * from " + SPName).ToList();
        }

        public IEnumerable<SearchLonaGuarantorStatusT> SearchLonaGuarantorStatuses(string SPName, [Optional] SqlParameter ParamValue)
        {

            var result = _db.searchLonaGuarantorStatuses.FromSqlRaw(SPName, ParamValue).ToList();

            return (result);

        }


        //public async Task<AddNewLoanObjectModel> GetLonaByidAsync(int LonaId)
        //{
        //    //GET all invoic from Warehouse BY storeId 

        //    var Result = (AddnewLonaDetailsModel)null;
        //    var checkexistsLona = true;
        //    if (LonaId != 0) checkexistsLona = _db.addnewLonaDetails.Any(x => x.LonaId == LonaId);
        //    if (checkexistsLona is true)
        //    {
        //        Result = await _db.addnewLonaDetails.Where(o => o.LonaId == LonaId)
        //          .FirstOrDefaultAsync();

        //    }

        //    return Result;


        //    //try
        //    //{
        //    //    if ( LonaId != 0) GetwarehouseStore = await _db.addnewLonaDetails.FindAsync(LonaId);


        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
        //    //       ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
        //    //}
        //    GC.Collect();

        //    return GetwarehouseStore;

        //}

        public async Task<ResponseObject> UpdateLona(AddnewLonaDetailsModel addnewLonaDetailsModel)
        {
            ResponseObject responseObject = new();


            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();


            try
            {
                if (addnewLonaDetailsModel.LonaId == GetAddMasterid)
                {
                    AddnewLonaDetailsModel _AddnewLonaDetailsModel = new();
                    for (int i = 0; i < 2; i++)
                    {
                        _AddnewLonaDetailsModel = _db.addnewLonaDetails.FirstOrDefault(x => x.LonaDetailsId == addnewLonaDetailsModel.LonaDetailsId);

                        _AddnewLonaDetailsModel.LonaGuarantorId = addnewLonaDetailsModel.LonaGuarantorId;
                        await _db.SaveChangesAsync();
                    }
                    _db.Database.CloseConnection();
                    responseObject.IsValid = true;
                    responseObject.Message = "Added successfully";
                    responseObject.Data = DateTime.Now.ToString();
                }
            }
 
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();
                GC.Collect();

            }
            if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;

        }
        public async Task<ResponseObject> UpdateMasterLonaAsync(int LonaId, AddNewLonaMasterModel addNewLonaMasterModel)
        {
      
            ResponseObject responseObject = new();
          if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();

            try
            {
                AddNewLonaMasterModel _AddNewLonaMasterModel = new();

                _AddNewLonaMasterModel = _db.addNewLonaMasters.FirstOrDefault(x => x.LonaId == addNewLonaMasterModel.LonaId);

                if (_AddNewLonaMasterModel != null)
                {
                    _db.Entry(_AddNewLonaMasterModel).State = EntityState.Detached;
                }
                _AddNewLonaMasterModel = addNewLonaMasterModel;
                await _db.SaveChangesAsync();
                var resultMasterProductsWarehouse = _db.addNewLonaMasters.Update(addNewLonaMasterModel);
                _db.SaveChanges();
                GetAddMasterid = addNewLonaMasterModel.LonaId;
                 responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();

            }


            catch (Exception ex)
            {
                if (!LonaExists(LonaId))

                    Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
     ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                return responseObject;
            }
            return responseObject;



        }

        
        bool LonaExists(int LonaId)
        {
            return _db.addNewLoanObject.Any(x => x.LonaId == LonaId);
        }
        public IEnumerable<UpdateLoanObjectModel> GetLonaByidAsync(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.updateLoanObjectModels.FromSqlRaw(SPName, ParamValue).AsNoTracking();

            return result;
        }


        public IEnumerable<AddNewLoanObjectModel> addNewLoanObjectModels(string SPName, [Optional] SqlParameter ParamValue)
        {

            var result = _db.addNewLoanObject.FromSqlRaw(SPName, ParamValue).ToList();

            return result;


        }

        public IEnumerable<TrackLonaWithGuarantorId> trackLonaWithGuarantorIds(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.trackLonaWithGuarantorIds.FromSqlRaw(SPName, ParamValue).AsNoTracking();

            return result;
        }

        public  async Task<ResponseObject> IssuanceLonaAsync(IssuanceLonaModel issuanceLonaModel)
        {
            ResponseObject responseObject = new();
            AddNewLonaMasterModel addNewLonaMasterModel= new();

            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();


            try
            {
                if (issuanceLonaModel.LonaId != 0 )
                {
                
                    addNewLonaMasterModel = _db.addNewLonaMasters.FirstOrDefault(x => x.LonaId == issuanceLonaModel.LonaId);

                 
                    addNewLonaMasterModel.LonaId = issuanceLonaModel.LonaId;
                    addNewLonaMasterModel.AmountAfterAddInterest = issuanceLonaModel.AmountAfterAddInterest;
                        addNewLonaMasterModel.StartDateLona = issuanceLonaModel.StartDateLona;
                        addNewLonaMasterModel.EndDateLona = issuanceLonaModel.EndDateLona;
                    addNewLonaMasterModel.StatusLona = issuanceLonaModel.StatusLona;
                
                        await _db.SaveChangesAsync();

           

                    _db.Database.CloseConnection();
                    responseObject.IsValid = true;
                    responseObject.Message = "Added successfully";
                    responseObject.Data = DateTime.Now.ToString();
                }
            }

            //}
            //} 
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);


                responseObject.IsValid = false;
                responseObject.Message = "failed";
                responseObject.Data = DateTime.Now.ToString();
                GC.Collect();

            }
            if (responseObject.IsValid == true) responseObject.Message = "Added successfully"; responseObject.Data = DateTime.Now.ToString();
            return responseObject;
        }

        public IEnumerable<SearchCanCustomerBeGuanantorT> SearchcanCustomerBeGuanantorStatuses(string SPName, [Optional] SqlParameter ParamValue)
        {
            var result = _db.searchCanCustomerBeGuanantors.FromSqlRaw(SPName, ParamValue).AsNoTracking();

            return result;
        }




        /// <summary>
        /// UpdateMasterLonaAsync this method can find in addnewlona 
        /// using here to change  addNewLonaMasterModel.IsDelete =true
        /// </summary>
        /// <param name="paymentOfistallmentsDetails"></param>
        /// <returns></returns>
        public async Task<ResponseObject> ChangeStatusMasterLona(int LonaId)
        {
            ResponseObject responseObject = new();
            if (_db.Database.CanConnect() == true) _db.Database.CloseConnection();
            try
            {
                AddNewLonaMasterModel _addNewLonaMasterModel = new();
                if (_addNewLonaMasterModel != null)
                {
                    _db.Entry(_addNewLonaMasterModel).State = EntityState.Detached;
                }
                _addNewLonaMasterModel = new AddNewLonaMasterModel()
                {
                    LonaId = LonaId,
                    IsDelete = true

                };
                 
                _db.addNewLonaMasters.Update(_addNewLonaMasterModel);
                await _db.SaveChangesAsync();

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

    }
}
