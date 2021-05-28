namespace MXIC_PCCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("UserManagement")]
    public partial class UserManagement
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserListID { get; set; }

   
        [Column(Order = 1)]
        [StringLength(50)]
        public string DepNo { get; set; }

     
        [Column(Order = 2)]
        [StringLength(50)]
        public string DepName { get; set; }

      
        [Column(Order = 3)]
        [StringLength(50)]
        public string UserID { get; set; }

      
        [Column(Order = 4)]
        [StringLength(50)]
        public string UserName { get; set; }

   
        [Column(Order = 5)]
        public string Admin { get; set; }

        [Column(Order = 6)]
        public string PassWord { get; set; }
     
        [Column(Order = 7)]
        public bool UserDisable { get; set; }
        [Column(Order = 8)]
        public Guid EditID { get; set; }
        [Column(Order = 9)]
        public Guid DeleteID { get; set; }

    }
}
