using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Data.Repo {
    public interface IGenRepo<TContext, TDomain>
            where TDomain : AnyDomainModel
            where TContext : IDbContext {

        Repo<TContext, TDomain> Include(params string[] includedProperties);

        Repo<TContext, TDomain> AsNoTracking();

        Repo<TContext, TDomain> Query(
                int page = 0,
                int perPage = 0,
                params string[] orderBy);

        Repo<TContext, TDomain> Query(
                Expression<Func<TDomain, bool>> predicate,
                int page = 0,
                int perPage = 0,
                params string[] orderBy);

        Repo<TContext, TDomain> Query<TResult>(
                Expression<Func<TDomain, TResult>> select,
                Expression<Func<TDomain, bool>> predicate,
                int page = 0,
                int perPage = 0,
                params string[] orderBy);

        TDomain Find(params object[] keyValues);

        void Upsert(TDomain item);

        QueryDetails Details { get; }

        IEnumerable<TResult> Result<TResult>();
        IEnumerable<TDomain> Result();
    }
    public class QueryDetails {
        public int RecordsReturned { get; set; }
        public int TotalRecords { get; set; }
    }
}
