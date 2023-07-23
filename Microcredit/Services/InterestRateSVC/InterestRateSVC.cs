﻿using Microcredit.ClassProject;
 using Microcredit.Models;
using Microcredit.ModelService;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.Services.InterestRateSVC
{
    public class InterestRateSVC : IInterestRate
    {
        private readonly ApplicationDbContext _db;

        public InterestRateSVC(ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task<ResponseObject> CreateInterestRateAsync(InterestRate interestRate)
        {
            ResponseObject responseObject = new();
            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddInterestRateModel = new InterestRate
                {
                     InterestRateName = interestRate.InterestRateName,
                    InterestRateValue = interestRate.InterestRateValue 
                    //UserID = 1

                };
                var result = await _db.interestRate.AddAsync(AddInterestRateModel);
                await _db.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
                var GetCurrentId = AddInterestRateModel.InterestRateId;

                responseObject.IsValid = true;
                responseObject.Message = ">" + GetCurrentId;
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
            GC.Collect();

            return responseObject;
        }

        public IEnumerable<InterestRate> GetAllInterestAsync(string SPName)
        {
            return _db.interestRate.FromSqlRaw("select * from " + SPName).ToList();

        }
  
        public async Task<bool> UpdateInterestRate(int InterestRateId, InterestRate interestRate)
        {
            ResponseObject responseObject = new();
 
            if (InterestRateId != interestRate.InterestRateId)
            {
                responseObject.Message = "Error Please check that all fields are entered";


            }
            try
            {
                _db.Entry(interestRate).State = EntityState.Modified;

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
        public async Task<InterestRate> GETInterestBYIdAsync(int InterestRateId)
        {
            var interestRate = (InterestRate)null;
            try
            {
                if (InterestRateId != 0)
                {
                    interestRate = await _db.interestRate.FindAsync(InterestRateId);


                }
            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return interestRate;
        }
    }
}
