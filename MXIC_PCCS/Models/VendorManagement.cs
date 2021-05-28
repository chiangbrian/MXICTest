namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("VendorManagement")]
    public partial class VendorManagement
    {
        [Key]
        [Column(Order = 0)]
        public Guid VenID { get; set; }

       
        [Column(Order = 1)]
        [StringLength(50)]
        public string PoNo { get; set; }

       
        [Column(Order = 2)]
        [StringLength(50)]
        public string VendorName { get; set; }

        
        [Column(Order = 3)]
        [StringLength(50)]
        public string EmpID { get; set; }

       
        [Column(Order = 4)]
        [StringLength(50)]
        public string EmpName { get; set; }

        
        [Column(Order = 5)]
        public Guid DeleteID { get; set; }

       
        [Column(Order = 6)]
        public Guid EditID { get; set; }

        [StringLength(50)]
        public string Shifts { get; set; }
    }
}
