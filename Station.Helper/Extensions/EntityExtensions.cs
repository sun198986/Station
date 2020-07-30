using System;
using System.Linq;
using System.Linq.Expressions;

namespace Station.Helper.Extensions
{
    public static class EntityExtensions
    {
        public static Expression<Func<T, bool>> AsExpression<T>(this T t) where T : class
        {
            var properties = t.GetType().GetProperties();
            var param = Expression.Parameter(typeof(T));

            BinaryExpression filter = Expression.Equal(Expression.Constant(1), Expression.Constant(1));
            foreach (var rProperty in properties)
            {
                var key = rProperty.Name;
                var val = rProperty.GetValue(t);
                if(val == null)
                    continue;
                Expression left = Expression.Property(param, key);
                Expression right = Expression.Constant(val, val.GetType());
                Expression result=Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }) ?? throw new InvalidOperationException(), right);
                filter = Expression.And(filter, result);
            }

            return Expression.Lambda<Func<T, bool>>(filter, param);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}