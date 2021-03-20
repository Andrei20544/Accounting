namespace Accounting
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Apartaments> Apartaments { get; set; }
        public virtual DbSet<Dealings> Dealings { get; set; }
        public virtual DbSet<Realtors> Realtors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartaments>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
