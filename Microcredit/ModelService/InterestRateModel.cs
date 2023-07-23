using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class InterestRate
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestRateId { get; set; }
        public string InterestRateName { get; set; }
        public int InterestRateValue { get; set; }
        public bool IsDelete { get; set; }

        public DateTime DateAdd { get; set; }

    }
}
