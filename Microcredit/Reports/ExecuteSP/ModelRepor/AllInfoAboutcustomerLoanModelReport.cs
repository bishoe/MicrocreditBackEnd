using System;
using System.ComponentModel.DataAnnotations;

namespace Microcredit.Reports.ExecuteSP.ModelRepor
{
    public class AllInfoAboutcustomerLoanModelReport
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int MaxLonaForCustomer { get; set; }
        public int MaxNumberGuarantorLona { get; set; }
        public DateTime StartDateLona { get; set; }
        public int LonaId { get; set; }
        public int InterestRateid { get; set; }
        public string InterestRateName { get; set; }
        public int MonthNumber { get; set; }
        public Decimal AmountBeforeAddInterest { get; set; }
        public int ProdouctsID { get; set; }
        public string ProdouctName { get; set; }
        public int LonaGuarantorId { get; set; }

    }
}
