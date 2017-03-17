using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SampleReview.Data.Repo {
    public class Repo<TContext, TDomain> : IRepo<TContext, TDomain>
            where TDomain : AnyDomainModel
            where TContext : IDbContext {
        public Repo(TContext context) {
            this.context = context;
            this.context = context;
            dbSet = context.Set<TDomain>();
        }

        protected TContext context;
        protected DbSet<TDomain> dbSet;
        protected IQueryable<TDomain> query;
        protected IQueryable<object> projectedQuery;
        protected QueryDetails queryDetails;

        public QueryDetails Details {
            get {
                return queryDetails;
            }
        }

        public virtual IRepo<TContext, TDomain> Include(params string[] includedProperties) {
            query = dbSet;
            foreach(string include in includedProperties) {
                query = query.Include(include);
            }
            return this;
        }
        public virtual IRepo<TContext, TDomain> Query(
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {
            return Query<TDomain>(null, null, page, perPage, orderBy);
        }
        public virtual IRepo<TContext, TDomain> Query(
                Expression<Func<TDomain, bool>> predicate,
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {
            return Query<TDomain>(null, predicate, page, perPage, orderBy);
        }
        public IRepo<TContext, TDomain> Query<TResult>(
                Expression<Func<TDomain, TResult>> select = null,
                Expression<Func<TDomain, bool>> predicate = null,
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {
            query = query ?? dbSet;
            IOrderedQueryable<TResult> orderedQuery = null;
            IQueryable<TResult> projectedQuery = null;
            if (predicate != null) query = query.Where(predicate);
            projectedQuery = select!= null ? query.Select(select) : (IQueryable<TResult>)query;
            

            foreach(string column in orderBy){
                char firstChar = column.First();
                string columnName = firstChar == '+' || firstChar == '-' ? column.Substring(1) : column;
                if (columnName != "this") {
                    if (orderedQuery != null)
                        orderedQuery = firstChar == '-' ? orderedQuery.ThenByDescending(columnName.ToPascal()) : orderedQuery.ThenBy(columnName.ToPascal());
                    else
                        orderedQuery = firstChar == '-' ?  projectedQuery.OrderByDescending(columnName.ToPascal()) : projectedQuery.OrderBy(columnName.ToPascal());
                }
                else {
                    orderedQuery = firstChar == '-' ? projectedQuery.OrderByDescending(x=>x) : projectedQuery.OrderBy(x=> x);
                }
            }

            projectedQuery = orderedQuery;
            if (page > 0 && perPage > 0)
                projectedQuery = projectedQuery
                    .Skip((page - 1) * perPage)
                    .Take(perPage);
            queryDetails = new QueryDetails {
                RecordsReturned = projectedQuery.Count(),
                TotalRecords = query.Count()
            };
            this.projectedQuery = (IQueryable<object>)projectedQuery;
            return this;
        }

        public virtual void Upsert(TDomain item) {
            if (item.HasEmptyId()) {
                dbSet.Add(item);
            }
            else {
                dbSet.Attach(item);
                context.Entry(item).State = EntityState.Modified;
            }
        }

        public IRepo<TContext, TDomainChild> ToRepo<TDomainChild>() where TDomainChild : TDomain {
            return new Repo<TContext, TDomainChild>(context);
        }
        public virtual TDomain Find(params object[] keyValues) {
            var single = dbSet.Find(keyValues);
            context.Entry(single).Reload();
            return single;
        }

        public IRepo<TContext, TDomain> AsNoTracking() {
            query = query == null ? dbSet.AsNoTracking() : query.AsNoTracking();
            return this;
        }
        public IEnumerable<TDomain> Result() {
           return ((IEnumerable<TDomain>) projectedQuery ?? query).ToList();
        }

        public IEnumerable<TResult> Result<TResult>() {
           return (IEnumerable<TResult>) projectedQuery.ToList();
        }
    }
}
