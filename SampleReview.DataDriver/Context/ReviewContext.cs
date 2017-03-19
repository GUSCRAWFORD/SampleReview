namespace SampleReview.DataDriver.Context {
    using System;
    using System.Data.Entity;
    using Data.Context;
    using Data.Domain;
    using System.Data.Entity.Infrastructure;

    public partial class ReviewContext : DbContext, IDbContext {
        public ReviewContext()
            : base("name=ReviewContext") {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<AnalyzedItem> AnalyzedItems { get; set; }

        public EntityState GetEntryState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        public void Reload<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).Reload();
        }

        public void SetEntryState<TEntity>(TEntity entity, EntityState toState) where TEntity : class
        {
            Entry(entity).State = toState;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.Reviewing)
                .WillCascadeOnDelete(false);
        }

        EntityState IDbContext.GetEntryState<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
