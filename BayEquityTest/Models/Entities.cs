namespace BayEquityTest.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<LoanOfficer> LoanOfficers { get; set; }
        public virtual DbSet<Pipeline> Pipelines { get; set; }
        public virtual DbSet<Purpose> Purposes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pipeline>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purpose>()
                .HasMany(e => e.Pipelines)
                .WithRequired(e => e.Purpose)
                .WillCascadeOnDelete(false);
        }
    }
}
