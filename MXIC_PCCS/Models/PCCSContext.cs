namespace MXIC_PCCS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PCCSContext : DbContext
    {
        public PCCSContext()
            : base("name=MXIC_PCCS")
        {
        }
        public virtual DbSet<DepartmentManagement> MXIC_DepartmentManagements { get; set; }
        public virtual DbSet<LisenceManagement> MXIC_LisenceManagements { get; set; }
        public virtual DbSet<Quotation> MXIC_Quotations { get; set; }
        public virtual DbSet<ScheduleSetting> MXIC_ScheduleSettings { get; set; }
        public virtual DbSet<SwipeInfo> MXIC_SwipeInfos { get; set; }
        public virtual DbSet<UserManagement> MXIC_UserManagements { get; set; }
        public virtual DbSet<InputGenerate> MXIC_InputGenerates { get; set; }
        public virtual DbSet<VendorManagement> MXIC_VendorManagements { get; set; }
        public virtual DbSet<FAC_ATTENDLIST> FAC_ATTENDLISTs { get; set; }

        public virtual DbSet<View_Swipe> MXIC_View_Swipes { get; set; }

        public virtual DbSet<View_Swipe_Double> View_Swipe_Doubles { get; set; }

        public virtual DbSet<NavData> NavDatas { get; set; }

        public virtual DbSet<SwipeSet> SwipeSets { get; set; }

        public virtual DbSet<SelectList> SelectLists { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
