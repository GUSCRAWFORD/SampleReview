namespace SampleReview.DataDriver.Context {

    using System.Data.Entity;
    using Data.Context;
    using Data.Domain;

    public partial class ReviewContext : DbContext, IDbContext {
        public ReviewContext()
            : base("name=ReviewContext") {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<AnalyzedItem> AnalyzedItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.Reviewing)
                .WillCascadeOnDelete(false);
        }
    }
}
