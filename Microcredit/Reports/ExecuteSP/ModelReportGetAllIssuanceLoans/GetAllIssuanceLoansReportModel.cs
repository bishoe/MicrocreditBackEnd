
using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelReportGetAllIssuanceLoans
{
    public class GetAllIssuanceLoansReportModel
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public decimal LonaAmount { get; set; }
        public int ProdouctsID { get; set; }
        public int StatusLona { get; set; }

        public string ProdouctName { get; set; }
    }
}
