using Microcredit.Reports.ExecuteSP.ModelRepor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class CalculateRemainingAmountModel 

    {


        /// <summary>
        /// non-composable sql and with a query composing over it because use CalculateRemainingAmountModel : another class
        /// </summary>
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int LonaId { get; set; }



        //  public int LonaGuarantorId { get; set; }
        [NotMapped]
        public decimal AmountPaid { get; set; }
        [NotMapped]

        public decimal AmountRemaining { get; set; }
 
        public decimal IstalmentsAmount { get; set; }
        [NotMapped]

        public int NoIstalments { get; set; }
        [NotMapped]

        public int StatusIstalments { get; set; }

        public int PaymentId { get; set; }
        public int StatusLona { get; set; }
    }
}
