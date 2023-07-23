
using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelReportAllLoansUnderEdit
{
    public class AllLoansUnderEditReportModel
    {

        [Key]
        public int LonaId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}
