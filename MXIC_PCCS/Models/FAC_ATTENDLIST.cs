namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FAC_ATTENDLIST
    {
        public string ID { get; set; }

        [Required]
        [StringLength(128)]
        public string INDEX_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime WORK_DATETIME { get; set; }

        [Required]
        [StringLength(8)]
        public string SECTION { get; set; }

        [Required]
        [StringLength(32)]
        public string VENDOR { get; set; }

        [StringLength(32)]
        public string PO_NO { get; set; }

        [Required]
        [StringLength(256)]
        public string ACTIVITY { get; set; }

        [Required]
        [StringLength(32)]
        public string WORKER_NAME { get; set; }

        [Required]
        [StringLength(1)]
        public string GENDER { get; set; }

        [Required]
        [StringLength(32)]
        public string INSURANCE_APPLICANT { get; set; }

        [StringLength(1)]
        public string MAIN_ENTRANCE_FLAG { get; set; }

        [StringLength(1)]
        public string TBM_FLAG { get; set; }

        [StringLength(8)]
        public string TBM_TYPE { get; set; }

        public DateTime? TBM_DATETIME { get; set; }

        public DateTime? ENTRANCE_DATETIME { get; set; }

        public DateTime? EXIT_DATETIME { get; set; }

        [StringLength(1)]
        public string STAY_FLAG { get; set; }

        public DateTime CREATE_DATETIME { get; set; }

        public DateTime RPT_DATETIME { get; set; }

        [Required]
        [StringLength(1)]
        public string LAST_FLAG { get; set; }
    }
}
