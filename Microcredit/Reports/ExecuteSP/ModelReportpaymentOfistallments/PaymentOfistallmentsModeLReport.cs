using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelReportpaymentOfistallments
{
    public class PaymentOfistallmentsModeLReport
    {

        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int LonaId { get; set; }

         
        public decimal? AmountPaid { get; set; }
        //public decimal AmountRemaining { get; set; }
        public decimal IstalmentsAmount { get; set; }
        public int? NoIstalments { get; set; }
        //public int StatusIstalments { get; set; }
        public int PaymentId { get; set; }

        public DateTime  DateAdd { get; set; }

        public decimal? TotalPaid { get; set; }
    }

    public class DuelmentsbetweenDateModelReport 
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int LonaId { get; set; }
        public int  NoIstalments { get; set; }

        //public decimal AmountRemaining { get; set; }
        public decimal IstalmentsAmount { get; set; }
         //public int StatusIstalments { get; set; }
        public int PaymentId { get; set; }

 
         public  DateTime  DueDate { get; set; }
        public decimal LonaAmount { get; set; }
    }
}
