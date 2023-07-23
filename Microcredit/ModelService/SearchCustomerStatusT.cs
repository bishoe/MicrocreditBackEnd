using System.ComponentModel.DataAnnotations;

namespace Microcredit.ModelService
{
    public class SearchCustomerStatusT
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int MaxLonaForCustomer { get; set; }

        public int MaxNumberGuarantorLona { get; set; }

        public int CanCustomerBeGuanantor { get; set; }


        public int LonaId { get; set; }

        public int StatusLona { get; set; }


        public bool IsDelete { get; set; }
    }
}
