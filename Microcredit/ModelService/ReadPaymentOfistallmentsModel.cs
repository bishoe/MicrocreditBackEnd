using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class ReadPaymentOfistallmentsModel
    {


        [Key]
        public int LonaId { get; set; }
        //[NotMapped]

        public int ProdcutId { get; set; }
        public int CustomerId { get; set; }
        public int InterestRateid { get; set; }
        public int MonthNumber { get; set; }
        //public DateTime StartDateLona { get; set; }
        //public DateTime? EndDateLona { get; set; }
        public DateTime DateAdd { get; set; }
 
        public decimal AmountBeforeAddInterest { get; set; }

        public decimal? AmountAfterAddInterest { get; set; }

         public int StatusLona { get; set; }


        //--------
        //public int? LonaDetailsId { get; set; }
        //public int? LonaGuarantorId { get; set; }


        //public int IstalmentsNo { get; set; }
        public bool IsDelete { get; set; }
         public string CustomerName { get; set; }
         public string CustomerNationalid { get; set; }

        public string ProdouctName { get; set; }

        public int ProdouctsID { get; set; }

         public int PaymentId { get; set; }

 
        //public decimal IstalmentsAmount { get; set; }
        //public decimal AmountPaid { get; set; }
        //public decimal AmountRemaining { get; set; }
        public decimal LonaAmount { get; set; }


        public string UserId { get; set; }
 

    }
}
