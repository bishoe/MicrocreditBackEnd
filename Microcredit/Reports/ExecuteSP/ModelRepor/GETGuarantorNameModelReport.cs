using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Reports.ExecuteSP.ModelRepor
{


    /// <summary>
    /// use this object in reports because all object must return all attribute in query and [notmapped ] not work return 0 and this result not correct 
    /// </summary>
    public class GETGuarantorNameModelReport
    {
        [Key]
        public int CustomerId { get; set; }
        //[NotMapped]
        public int LonaGuarantorId { get; set; }

        public int LonaId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        //[StringLength(14)]
        public string CustomerNationalid { get; set; }
        public string FirstPhone { get; set; }
        public DateTime StartDateLona { get; set; }
        public int MonthNumber { get; set; }

        public decimal AmountAfterAddInterest { get; set; }
        [NotMapped]

        public int ProdcutId { get; set; }
        [NotMapped]

        public int ProdouctsID { get; set; }

        public string ProdouctName { get; set; }


        public int StatusLona { get; set; }
        [NotMapped]
        public int InterestRateId { get; set; }

        public string InterestRateName { get; set; }
 
     

        //[NotMapped]
    }

}
