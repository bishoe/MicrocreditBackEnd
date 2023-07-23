using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelRepor
{
    public class GETAllInfoAboutcustomerLoanReport
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
         
        public int LonaId { get; set; }



        //  public int LonaGuarantorId { get; set; }

        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }
        public decimal IstalmentsAmount { get; set; }
        public int NoIstalments { get; set; }
        public int StatusIstalments { get; set; }
        public int PaymentId { get; set; }


     


    }
    public class ExpeditedPayment { 
        [Key]

     public int DiscountPercentage { get; set; }

    public decimal AmountBeforeDiscount { get; set; }

    public decimal AmountAfterDiscount { get; set; }

    public DateTime ExpeditedPaymentDate { get; set; }

    public int StatusLona { get; set; }

     
    }
}
