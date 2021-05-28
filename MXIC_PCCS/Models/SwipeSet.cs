namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SwipeSet")]
    public partial class SwipeSet
    {
        [Key]
        [Column("SwipeSet")]
        public Guid SwipeSet1 { get; set; }

        [Required]
        [StringLength(50)]
        public string PoNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkShift { get; set; }

        [Required]
        [StringLength(50)]
        public string SwipeStartTime { get; set; }

        [Required]
        [StringLength(50)]
        public string SwipeEndTime { get; set; }

        public Guid EditID { get; set; }
    }
}
