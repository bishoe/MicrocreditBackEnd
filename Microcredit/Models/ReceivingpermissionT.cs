﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternalShop.Models
{
    public class ReceivingpermissionT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceivingpermissionId { get; set; }
        [Required]
        public int MasterOFSToresID { get; set; }
        [Required]
        public int ProdouctsID { get; set; }
        [Required]
        public int quantityProduct { get; set; }
        [Required]
        public DateTime DateAdd { get; set; }
        public DateTime DateEdit { get; set; }
        [Required]
        public int UserID { get; set; }
     
    }
}
