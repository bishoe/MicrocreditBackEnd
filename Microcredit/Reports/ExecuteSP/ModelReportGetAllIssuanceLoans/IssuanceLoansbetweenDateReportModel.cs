using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelReportGetAllIssuanceLoans
{
    public class IssuanceLoansbetweenDateReportModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int LonaId { get; set; }
 
         public int PaymentId { get; set; }


        public DateTime DateAdd { get; set; }
        public decimal LonaAmount { get; set; }
    }
}
