using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SampleReview.Data.Repo {
    public class Repo<TContext, TDomain> : IGenRepo<TContext, TDomain>
            where TDomain : AnyDomainModel
            where TContext : IDbContext {
        public Repo(IDbContext context) {
            _context = (TContext) context;
            _dbSet = _context.Set<TDomain>();
        }

        protected TContext _context;
        protected DbSet<TDomain> _dbSet;
        protected IQueryable<TDomain> _query;
        protected IQueryable<object> _projectedQuery;
        protected QueryDetails _queryDetails;

        public QueryDetails Details {
            get {
                return _queryDetails;
            }
        }

        public virtual Repo<TContext, TDomain> Include(params string[] includedProperties) {
            _query = _query ?? _dbSet;
            foreach(string include in includedProperties) {
                _query = _query.Include(include);
            }
            return this;
        }
        public virtual Repo<TContext, TDomain> Query(
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {
            return Query<TDomain>(null, null, page, perPage, orderBy);
        }
        public virtual Repo<TContext, TDomain> Query(
                Expression<Func<TDomain, bool>> predicate,
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {
            return Query<TDomain>(null, predicate, page, perPage, orderBy);
        }
        public Repo<TContext, TDomain> Query<TResult>(
                Expression<Func<TDomain, TResult>> select = null,
                Expression<Func<TDomain, bool>> predicate = null,
                int page = 0,
                int perPage = 0,
                params string[] orderBy) {

            IQueryable<TDomain> query = _query ?? _dbSet;
            IOrderedQueryable<TResult> orderedQuery = null;
            IQueryable<TResult> projectedQuery = null;

            if (predicate != null) query = query.Where(predicate);
            projectedQuery = select!= null ? query.Select(select) : projectedQuery = (IQueryable<TResult>)query;
            
            if (orderBy != null) {
                foreach(string column in orderBy){
                    char firstChar = column.First();
                    string columnName = firstChar == '+' || firstChar == '-' ? column.Substring(1) : column;
                    Expression<Func<TDomain, object>> propertyExpression = (x)=> x.GetType().GetProperty(columnName).GetValue(x);
                    if (orderedQuery != null)
                        orderedQuery = firstChar == '-' ? orderedQuery.ThenByDescending(columnName) : orderedQuery.ThenBy(columnName);
                    else
                        orderedQuery = firstChar == '-' ?  projectedQuery.OrderByDescending(columnName) : projectedQuery.OrderBy(columnName);
                }
            }

            projectedQuery = (orderedQuery ?? projectedQuery);
            if (page > 0 && perPage > 0)
                projectedQuery = projectedQuery
                    .Skip((page - 1) * perPage)
                    .Take(perPage);
            _queryDetails = new QueryDetails {
                RecordsReturned = projectedQuery.Count(),
                TotalRecords = query.Count()
            };
            _query = query;
            _projectedQuery = (IQueryable<object>)projectedQuery;
            return this;
        }
        public virtual void Upsert(TDomain item) {
            if (item.HasEmptyId()) {
                _dbSet.Add(item);
            }
            else {
                _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public virtual TDomain Find(params object[] keyValues) {
            return _dbSet.Find(keyValues);
        }
        public Repo<TContext, TDomain> AsNoTracking() {
            _query = _query == null ? _dbSet.AsNoTracking() : _query.AsNoTracking();
            return this;
        }
        public IEnumerable<TDomain> Result() {
           return _query.ToList();
        }
        public IEnumerable<TResult> Result<TResult>() {
           return (IEnumerable<TResult>) _projectedQuery.ToList();
        }
    }
}
