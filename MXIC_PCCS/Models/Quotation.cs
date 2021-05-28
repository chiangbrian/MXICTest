namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Quotation")]
    public partial class Quotation
    {
        [Key]
        public Guid QuotationID { get; set; }

        [Required]
        [StringLength(50)]
        public string PoNo { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorName { get; set; }

        [Required]
        [StringLength(50)]
        public string PoClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string PoClassName { get; set; }

        [Required]
        [StringLength(50)]
        public string LicPossess { get; set; }

        [Required]
        [StringLength(50)]
        public string Unit { get; set; }

        [Required]
        [StringLength(50)]
        public string Amount { get; set; }

       
        public int Sequence { get; set; }
    }
}
