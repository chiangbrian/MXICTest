namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("SwipeInfo")]
    public partial class SwipeInfo
    {
        [Key]
        public Guid SwipID { get; set; }

        public string valid { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(50)]
        public string CheckType { get; set; }

        public DateTime? SwipeTime { get; set; }
      
        public Guid EditID { get; set; }

        [Required]
        [StringLength(50)]
        public string AttendType { get; set; }

        public double Hour { get; set; }

        public DateTime WORK_DATETIME { get; set; }
    }
}
