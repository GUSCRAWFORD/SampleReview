﻿using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Data.Repo {
    public interface IRepo<TContext, TDomain>
            where TDomain : AnyDomainModel
            where TContext : IDbContext {

        IRepo<TContext, TDomainChild> ToRepo<TDomainChild>() where TDomainChild : TDomain;
        IRepo<TContext, TDomain> Include(params string[] includedProperties);

        IRepo<TContext, TDomain> AsNoTracking();

        IRepo<TContext, TDomain> Query(
                int page = 0,
                int perPage = 0,
                params string[] orderBy);

        IRepo<TContext, TDomain> Query(
                Expression<Func<TDomain, bool>> predicate,
                int page = 0,
                int perPage = 0,
                params string[] orderBy);

        IRepo<TContext, TDomain> Query<TResult>(
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
