using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Reports.ExecuteSP.ModelReportCustomers
{
    public class GetCountCustomersReportModel
    {
        [Key]

        public int CustomerId { get; set; }
    }
}
