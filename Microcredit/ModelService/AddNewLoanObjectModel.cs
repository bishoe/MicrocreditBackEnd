 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{


    /// <summary>
    /// 0 underedit
    /// 1 IssuanceLona
    /// 2 complete
    /// 3 delete
    /// </summary>
    /// 
   

    public class AddNewLoanObjectModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         [Key]
            //[NotMapped]
        public int LonaId { get; set; }
   


        public int ProdcutId { get; set; }
       

        public int CustomerId { get; set; }

   

        public int InterestRateid { get; set; }
   


        public int MonthNumber { get; set; }
   
        public DateTime DateAdd { get; set; }
       
        public decimal AmountBeforeAddInterest { get; set; }
 
   

        public int StatusLona { get; set; }
        //--------
            

        public int LonaDetailsId { get; set; }
         
        public int LonaGuarantorId { get; set; }
           

        public string UserID { get; set; }
     

        public bool IsDelete { get; set; }

        //public int Myid { get; set; }
   

        public int? Nocolumn { get; set; }

[NotMapped]
        public string CustomerName { get; set; }

    }


    public class TrackLoanObjectModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
         public int LonaId { get; set; }

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

        public string UserID { get; set; }
        //public int IstalmentsNo { get; set; }

        public bool IsDelete { get; set; }


        public string CustomerName { get; set; }


        public string CustomerNationalid { get; set; }

        public string ProdouctName { get; set; }

        public int ProdouctsID { get;set; }
    
    
    
    
    }

    public class UpdateLoanObjectModel
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int LonaId { get; set; }
        public int ProdcutId { get; set; }
        public int CustomerId { get; set; }
        public int InterestRateid { get; set; }
        public int MonthNumber { get; set; }
        public DateTime DateAdd { get; set; }
        public decimal AmountBeforeAddInterest { get; set; }
        public decimal AmountAfterAddInterest { get; set; }
        public int StatusLona { get; set; }
        //--------
        public int? LonaDetailsId { get; set; }
        public int? LonaGuarantorId { get; set; }
        public string UserID { get; set; }
        public DateTime StartDateLona { get; set; }
        public bool IsDelete { get; set; }
        public int ProdouctsID { get; set; }
    }

    //public class ReadPaymentOfistallmentsobject : TrackLoanObjectModel
    //{
    //    public decimal IstalmentsAmount { get; set; }
    //    public decimal AmountPaid { get; set; }
    //    public decimal AmountRemaining { get; set; }
    //    public decimal LonaAmount { get; set; }
    //    public new DateTime DateAdd { get; set; }
    //    public new bool IsDelete { get; set; }

    //    public string UserId { get; set; }
    //    public new int LonaId { get; set; }

    //}
}
