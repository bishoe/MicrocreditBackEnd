﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microcredit.Models
{
    public class ProductsWarehouseObjectT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManageStoreID { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public DateTime DateAdd { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalPrice { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal TotalBDiscount { get; set; }
        [Required]
        [Range(2, 15)]
        public decimal AMountDicount { get; set; }

        public string Notes { get; set; }

        public int UsersID { get; set; }

        public int StoreId { get; set; }
        [Required]
        public int SuppliersID { get; set; }
        public int Tax { get; set; }

        [Required]
        public int Billno { get; set; }
        [Required]
        public int PermissionToEntertheStoreProductId { get; set; }
        //public int BranchCode { get; set; }
        [Required]
        public int CategoryProductId { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        //[Range(2, 15)]
        public decimal PurchasingPrice { get; set; }
        [Required]
        //[Range(2, 15)]
        public decimal SellingPrice { get; set; }
        [Required]
        public DateTime Productiondate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        [Required]
        public int SizeProducts { get; set; }
        [Required]
        public int UnitesId { get; set; }
        [Required]
        //[Range(2, 15)]
        public decimal TotalAmountRow { get; set; }
        //[Required]
        //[Range(2, 15)]
        //public decimal TotalSize { get; set; }
        [Required]
        public int QuntityProduct { get; set; }
        //[Required]
        //public int QtStartPeriod { get; set; }
        [Required]
        public DateTime Dateofregistration { get; set; }
        [Required]
        public bool Anexpiredproduct { get; set; }
        public int Nocolumn { get; set; }

    }
}
