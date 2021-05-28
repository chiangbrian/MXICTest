namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("DepartmentManagement")]
    public partial class DepartmentManagement
    {
        [Key]
        public Guid DepID { get; set; }

        [Required]
        [StringLength(50)]
        public string DepNo { get; set; }

        [Required]
        [StringLength(50)]
        public string DepName { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorName { get; set; }

        public Guid EditID { get; set; }

        public Guid DeleteID { get; set; }
    }
}
