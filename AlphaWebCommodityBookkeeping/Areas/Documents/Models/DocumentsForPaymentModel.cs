using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Models
{
    public class DocumentsForPaymentModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Tip dokumenta")]
        public short DocumentType { get; set; }

        [Required]
        [Display(Name = "Broj dokumenta")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}