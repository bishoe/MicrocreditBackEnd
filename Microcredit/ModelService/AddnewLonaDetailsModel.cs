using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.ModelService
{
    public class AddnewLonaDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]


        public int LonaDetailsId { get; set; }
        [Required]


        public int LonaId { get; set; }

        [Required]
        public int LonaGuarantorId { get; set; }


        [Required]

        public bool IsDelete { get; set; }

        [Required]
        public bool StatusLonaGuarantor { get; set; }

        public int? Nocolumn { get; set; }
 
        [Required]

        public DateTime DateAdd { get; set; }
    }



}
