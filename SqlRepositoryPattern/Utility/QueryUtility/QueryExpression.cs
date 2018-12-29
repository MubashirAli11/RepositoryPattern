using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Utility.QueryUtility
{
    public class QueryExpression : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public QueryExpression(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }

        public static Expression<Func<T, bool>> CreateAndExpression<T>(
        Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            QueryExpression leftVisitor;
            QueryExpression rightVisitor;
            Expression left;
            Expression right;
            ParameterExpression parameter;

            parameter = Expression.Parameter(typeof(T));
            leftVisitor = new QueryExpression(exp1.Parameters[0], parameter);
            left = leftVisitor.Visit(exp1.Body);

            rightVisitor = new QueryExpression(exp2.Parameters[0], parameter);
            right = rightVisitor.Visit(exp2.Body);

            return Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(left, right), parameter);
        }

    }
}
