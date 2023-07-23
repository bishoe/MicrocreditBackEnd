 using Microcredit.Models;
using Microcredit.ModelService;
using Microcredit.Reports.ExecuteSP.ModelRepor;
using Microcredit.Reports.ExecuteSP.ModelReportAllLoansUnderEdit;
using Microcredit.Reports.ExecuteSP.ModelReportCustomers;
using Microcredit.Reports.ExecuteSP.ModelReportGetAllIssuanceLoans;
using Microcredit.Reports.ExecuteSP.ModelReportpaymentOfistallments;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microcredit
{
    public class ApplicationDbContext : IdentityDbContext<Appuser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }



        public DbSet<CustomersT> Customers { get; set; }
        public DbSet<GetCountCustomersReportModel> getCountCustomers { get; set; }


        
        public DbSet<EmployeesT> Employees { get; set; }

        public DbSet<AddNewLoanObjectModel> addNewLoanObject { get; set; }
        public DbSet<TrackLoanObjectModel> TrackLoanObjectModels { get; set; }

        public DbSet<UpdateLoanObjectModel> updateLoanObjectModels { get; set; }  
        public DbSet<AddNewLonaMasterModel> addNewLonaMasters { get; set; }

        public DbSet<AddnewLonaDetailsModel> addnewLonaDetails { get; set; }

        public DbSet<InterestRate> interestRate { get; set; }    

        public DbSet<AddNewLoanObjectModel> addNewLoanObjectModels { get; set; }
        public DbSet<GETGuarantorNameModelReport> getLonaGuarantModelReport { get; set; }
        public DbSet<ProductsT> Products { get; set; }

        public DbSet<SearchCustomerStatusT> searchCustomerStatuses { get; set; }

        public DbSet<SearchLonaGuarantorStatusT> searchLonaGuarantorStatuses { get; set; }
        public DbSet<SearchCanCustomerBeGuanantorT> searchCanCustomerBeGuanantors { get; set; }
        public DbSet<IssuanceLonaModel> issuanceLonas { get; set; }

    public DbSet<GetAllIssuanceLoansReportModel> getAllIssuanceLoansReportModels { get; set; }
        public DbSet<TrackLonaWithGuarantorId> trackLonaWithGuarantorIds { get; set; }

        public DbSet<ReadPaymentOfistallmentsModel>  readPaymentOfistallmentsModels { get; set; }
        public DbSet<PaymentOfistallments> paymentOfistallments { get; set; }

        public DbSet<PaymentOfistallmentsDetails> PaymentOfistallmentsDetails { get; set; }

        public DbSet<PaymentOfistallmentsObject> PaymentOfistallmentsObjects { get; set; }

        public DbSet<CalculateRemainingAmountModel> calculateRemainingAmountModels { get; set; }

        public DbSet<GETGuarantorNameModelReport>   GETGuarantorNameModelReports { get; set; }

        public DbSet<GETAllInfoAboutcustomerLoanReport> gETAllInfoAboutcustomerLoanReports { get; set; }


        public DbSet<ExpeditedPayment> expeditedPayments { get; set; }
      

        public DbSet<PaymentOfistallmentsModeLReport> PaymentOfistallmentsModeLReports  { get; set; }


        public DbSet<AllLoansUnderEditReportModel> allLoansUnderEditReportModels { get; set; }

        public DbSet<DuelmentsbetweenDateModelReport> duelmentsbetweenDateModelReports { get; set; }


        public DbSet<IssuanceLoansbetweenDateReportModel> issuanceLoansbetweenDateReportModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region model
            base.OnModelCreating(modelBuilder);
            #endregion

            #region default Create AdminUser
            modelBuilder.Entity<IdentityRole>().HasData(
                 new { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR", RoleName = "Administrator", Handle = "administrator", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true },
                 new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER", RoleName = "customer", Handle = "customer", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true }
             );
            #endregion
            modelBuilder.Entity<PaymentOfistallmentsModeLReport>().ToTable(nameof(PaymentOfistallmentsModeLReports), t => t.ExcludeFromMigrations());

            modelBuilder.Entity<AddNewLoanObjectModel>().HasKey( addN => addN.LonaId);

            //modelBuilder.Types().Configure(t => t.MapToStoredProcedures());

            //modelBuilder.Entity<PermissionToEntertheStoreProductT>().HasOne(p => p.Products).WithMany(PerMEnter => PerMEnter.PermissionToEntertheStoreProduct).HasForeignKey(PID => PID.ProdouctsID);

            //modelBuilder.Entity<PermissionToEntertheStoreProductT>().HasOne(MStore => MStore.ManageStore).WithMany(PerMEnter => PerMEnter.PermissionToEntertheStoreProduct).HasForeignKey(ManageSID => ManageSID.ManageStoreId);

            //modelBuilder.Entity<BranchesReportT>().HasNoKey().ToView("#BranchesReportT");

        }
    }

}
