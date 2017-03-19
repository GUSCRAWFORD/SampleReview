using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace SampleReview.Data.Context {
    public interface IDbContext : IDisposable{   
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetEntryState<TEntity>(TEntity entity, EntityState toState) where TEntity : class;
        EntityState GetEntryState<TEntity>(TEntity entity) where TEntity : class;
        void Reload<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
