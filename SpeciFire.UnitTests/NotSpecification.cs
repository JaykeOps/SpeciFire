using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public sealed class NotSpecification<TSubject> : Specification<TSubject>
    {
        private readonly Specification<TSubject> specification;


        public NotSpecification(Specification<TSubject> specification) => this.specification = specification;


        public override Expression<Func<TSubject, bool>> ToExpression()
        {
            var expression = specification.ToExpression();
            var negatedExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TSubject, bool>>(negatedExpression, expression.Parameters.Single());
        }
    }
}