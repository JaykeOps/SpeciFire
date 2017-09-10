using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.ExpressionUtilities
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression,
            Expression<Func<T, bool>> rightExpression)
        {

            ParameterExpression parameter = leftExpression.Parameters[0];

            SimpleExpressionVisitor visitor =
                new SimpleExpressionVisitor {Map = {[rightExpression.Parameters[0]] = parameter}};

            Expression body = Expression.AndAlso(leftExpression.Body, visitor.Visit(rightExpression.Body));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExpression,
            Expression<Func<T, bool>> rightExpression)
        {

            ParameterExpression parameter = leftExpression.Parameters[0];

            SimpleExpressionVisitor visitor =
                new SimpleExpressionVisitor {Map = {[rightExpression.Parameters[0]] = parameter}};

            Expression body = Expression.OrElse(leftExpression.Body, visitor.Visit(rightExpression.Body));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }


    }
}

    

    
