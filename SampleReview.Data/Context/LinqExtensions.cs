using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Data.Context {
    public static class LinqExtensions {
        #region OrderBy, ThenBy, Descending
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return (IOrderedQueryable<T>)OrderBy((IQueryable)source, propertyName);
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return (IOrderedQueryable<T>)OrderByDescending((IQueryable)source, propertyName);
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return (IOrderedQueryable<T>)ThenBy((IOrderedQueryable)source, propertyName);
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return (IOrderedQueryable<T>)ThenByDescending((IOrderedQueryable)source, propertyName);
        }
        public static IOrderedQueryable OrderBy(this IQueryable source, string propertyName) {
            return ExpandedOrderExpression("OrderBy", source, propertyName);
        }
        public static IOrderedQueryable OrderByDescending (this IQueryable source, string propertyName) {
            return ExpandedOrderExpression("OrderByDescending", source, propertyName);
        }
        public static IOrderedQueryable ThenBy (this IOrderedQueryable source, string propertyName) {
            return ExpandedOrderExpression("ThenBy", source, propertyName);
        }
        public static IOrderedQueryable ThenByDescending (this IOrderedQueryable source, string propertyName) {
            return ExpandedOrderExpression("ThenByDescending", source, propertyName);
        }
        public static IOrderedQueryable ExpandedOrderExpression(string orderMethod, IQueryable source, string propertyName)
        {
            var x = Expression.Parameter(source.ElementType);
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            return (IOrderedQueryable) source.Provider.CreateQuery(
            Expression.Call(typeof(Queryable), orderMethod, new Type[] { source.ElementType, selector.Body.Type },
                    source.Expression, selector
                    ));
        }
        #endregion
    }
}
