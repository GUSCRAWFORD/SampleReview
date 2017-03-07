using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace SampleReview.Data.Context {
    public interface IDbContext : IDisposable{   
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity item) where TEntity : class;
        int SaveChanges();
    }
}
