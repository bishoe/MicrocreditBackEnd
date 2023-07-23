using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class AddNewLonaMasterModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LonaId { get; set; }
        [Required]
        public int ProdcutId { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int InterestRateid { get; set; }

        [Required]
        public int MonthNumber { get; set; }

        [Required]
        public DateTime StartDateLona { get; set; }

        [Required]
        public DateTime EndDateLona { get; set; }
        [Required]
        public decimal AmountBeforeAddInterest { get; set; }

        [Required]
        public decimal AmountAfterAddInterest { get; set; }

        [Required]
        public DateTime DateAdd { get; set; }
        [Required]
        public int StatusLona { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        //public int MId { get; set; }

        [Required]
        public string UserID { get; set; }

    }

    public class IssuanceLonaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
 
        public int LonaId { get; set; }
        [Required]

        public int StatusLona { get; set; }

        [Required]

        public decimal AmountAfterAddInterest { get; set; }

        [Required]
        public DateTime StartDateLona { get; set; }


        [Required]
        public DateTime EndDateLona { get; set; }
    }
}