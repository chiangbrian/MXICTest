namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NavData")]
    public partial class NavData
    {
        [Key]
        public Guid NavID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public int commonly_used { get; set; }
    }
}
