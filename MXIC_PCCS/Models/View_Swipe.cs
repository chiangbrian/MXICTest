namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_Swipe
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string PoNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string VendorName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string EmpID { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string EmpName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime Date { get; set; }

        public DateTime? SwipeTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid EditID { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string AttendType { get; set; }

        [Key]
        [Column(Order = 7)]
        public double Hour { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string CheckType { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string WorkShift { get; set; }
    }
}
