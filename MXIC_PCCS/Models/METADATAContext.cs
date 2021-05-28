namespace MXIC_PCCS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class METADATAContext : DbContext
    {
        public METADATAContext()
            : base("name=METADATA")
        {
        }

        public virtual DbSet<FAC_ATTENDLIST> FAC_ATTENDLIST { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FAC_ATTENDLIST>()
                .Property(e => e.INSURANCE_APPLICANT)
                .IsFixedLength();
        }
    }
}
