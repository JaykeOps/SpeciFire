using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    internal sealed class OrSpecification<TSubject> : Specification<TSubject>
    {
        private readonly Specification<TSubject> left;
        private readonly Specification<TSubject> right;
        

        public OrSpecification(Specification<TSubject> left, Specification<TSubject> right)
        {
            this.left = left;
            this.right = right;
        }


        public override Expression<Func<TSubject, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();

            BinaryExpression disjunction = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSubject, bool>>(disjunction, leftExpression.Parameters.Single());
        }
    }
}