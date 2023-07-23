using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class CustomersT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        //[StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(250)]
        
        public string CustomerAddress { get; set; }
        [Required]
        //[StringLength(14)]
        public string CustomerNationalid { get; set; }
        [Required]
        public DateTime DateissuancenationalID { get; set; }
        [Required]
        public DateTime ExpirationdatenationalID { get; set; }
        [Required]
        //[StringLength(11)]
        public string FirstPhone { get; set; }
        [Required]
        //[StringLength(11)]
        public string SecondPhone { get; set; }
        [Required]
        //[MaxLength(50)]
        public string BusinessName { get; set; }
        [Required]
        //[MaxLength(250)]
        public string WorkAddress { get; set; }
        [Required]
        public int  MaxLonaForCustomer { get; set; }
        [Required]
       public int  MaxNumberGuarantorLona {get; set;}
        [Required]
         
        public int CanCustomerBeGuanantor { get; set; }

    public DateTime DateAdd { get; set; }

        public DateTime? DateEdit { get; set; }
        [Required]
        //[StringLength(250, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
        public string Notes { get; set; }
        public int UsersID { get; set; }

      

    }
 


    public class SearchLonaGuarantorStatusT
    {

        /// <summary>
        ///used in proc  SP_GetNoLonaGuarantor get count LonaGuarantor StatusLona==true if count  ==   max in cusomer 
        /// show Error screen 
        /// </summary>
        [Key]

        public int CustomerId { get; set; }


        public int MaxLonaForCustomer { get; set; }

        public int MaxNumberGuarantorLona { get; set; }

        public int CanCustomerBeGuanantor { get; set; }
        public int LonaId { get; set; }

        public int StatusLona { get; set; }


        public bool IsDelete { get; set; }
         public int? LonaDetailsId { get; set; }
         public int? LonaGuarantorId { get; set; }

        //public bool StatusLonaGuarantor { get; set; }

    }



    public class SearchCanCustomerBeGuanantorT
    {
         
        [Key]
        public int CustomerId { get; set; }


        public int MaxLonaForCustomer { get; set; }

        public int MaxNumberGuarantorLona { get; set; }

        public int CanCustomerBeGuanantor { get; set; }
     
        public int LonaId { get; set; }

        public int StatusLona { get; set; }


        public bool IsDelete { get; set; }
 

        //public bool StatusLonaGuarantor { get; set; }

    }


 
}
