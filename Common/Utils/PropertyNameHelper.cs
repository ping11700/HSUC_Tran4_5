using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Utils
{
    public class PropertyNameHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> expr)
        {
            var bodyExpr = expr.Body as System.Linq.Expressions.MemberExpression;
            if (bodyExpr == null)
            {
                throw new ArgumentException("Expression must be a MemberExpression!", "expr");
            }
            var propInfo = bodyExpr.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException("Expression must be a PropertyExpression!", "expr");
            }
            return propInfo.Name;
        }
    }
}
