using System;
using System.Linq.Expressions;
using SpeciFire.UnitTests.ExpressionUtilities;

namespace SpeciFire.UnitTests
{
    internal sealed class AndSpecification<TSubject> : Specification<TSubject> {

        private readonly Specification<TSubject> left;
        private readonly Specification<TSubject> right;


        public AndSpecification(Specification<TSubject> left, Specification<TSubject> right)
        {
            this.left = left;
            this.right = right;
        }


        public override Expression<Func<TSubject, bool>> ToExpression()
        {
            var leftExpression = left.ToExpression();
            var rightExpression = right.ToExpression();

            return leftExpression.And(rightExpression);
        }
    }
}
