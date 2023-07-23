using Microsoft.Build.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Microcredit.ModelService
{
    public class PaymentOfistallments
    {
        [Key]
        //[DatabaseGenerated(/*DatabaseGeneratedOption*/.Identity)/*]*/
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        //public decimal IstalmentsAmount { get; set; }
        //public decimal AmountPaid { get; set; }
        //public decimal AmountRemaining { get; set; }
        [Required]
        public decimal LonaAmount { get; set; }
        [Required]
        public int MonthNumber { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int LonaId { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
         public int DiscountPercentage { get; set;}
         public decimal  AmountBeforeDiscount { get; set; }
         public decimal AmountAfterDiscount { get; set; }
         public int  StatusLona { get; set; }
         public DateTime ExpeditedPaymentDate { get; set; }

        // [Required]
        //public decimal PaidFine { get; set; }
        // [Required]
        // public decimal FineDue { get; set;}
        // [Required]
        // public decimal ExemptFine { get; set; }
    }
    public class PaymentOfistallmentsDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PaymentIdDetails { get; set; }
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public decimal IstalmentsAmount { get; set; }
        [Required]
        public decimal AmountPaid { get; set; }
        [Required]
        public decimal AmountRemaining { get; set; }
        [Required]
        public int NoIstalments { get; set; }
        [Required]
        public int MonthNumber { get; set; }
        [Required]

        public DateTime DateAdd { get; set; }
        [Required]
        public int StatusIstalments { get; set; }
        public DateTime DueDate { get; set; }

    }

    public class PaymentOfistallmentsObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
 
        public int PaymentId { get; set; }
        [Required]


        public int CustomerId { get; set; }
        [Required]


        public decimal LonaAmount { get; set; }
        [Required]


        public int MonthNumber { get; set; }
        [Required]


        public DateTime DateAdd { get; set; }
        [Required]


        public bool IsDelete { get; set; }
        [Required]


        public string UserId { get; set; }
        [Required]


        public int LonaId { get; set; }
        [Required]


        public int ProdouctsID { get; set; }
        [Required]


        public int PaymentIdDetails { get; set; }
        [Required]


        public decimal IstalmentsAmount { get; set; }
        [Required]


        public decimal AmountPaid { get; set; }
        [Required]


        public decimal AmountRemaining { get; set; }
        [Required]


        public int NoIstalments { get; set; }

        [Required]


        public DateTime DateAddDetails { get; set; }
        
        [Required]


        public int StatusIstalments { get; set; }
        public DateTime DueDate { get; set; }

    }

    public class UpdatePaymentOfistallmentsDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]


        public int PaymentIdDetails { get; set; }
       
       [Required]


        public decimal AmountPaid { get; set; }
       [Required]


        public decimal AmountRemaining { get; set; }
       [Required]


        public DateTime DateAdd { get; set; }
       [Required]


        public int StatusIstalments { get; set; }
        public DateTime DueDate { get; set; }

    }
}
