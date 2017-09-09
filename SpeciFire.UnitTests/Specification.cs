using System;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;

namespace SpeciFire.UnitTests
{
    public abstract class Specification<TSubject>
    {
        public static readonly IInitialSpecification<TSubject> Initialize = new InitialSpecification<TSubject>();


        public Specification<TSubject> NOT => new NOTSpecification<TSubject>(this);


        public bool IsSatisfiedBySubject(TSubject subject)
        {
            Func<TSubject, bool> predicate = ToExpression().Compile();
            return predicate(subject);
        }


        public abstract Expression<Func<TSubject, bool>> ToExpression();


        public Specification<TSubject> AND(Specification<TSubject> specification) => new AndSpecification<TSubject>(this, specification);

        public Specification<TSubject> OR(Specification<TSubject> specification) =>
            new OrSpecification<TSubject>(this, specification);
    }

    public sealed class NOTSpecification<TSubject> : Specification<TSubject>
    {
        private readonly Specification<TSubject> specification;


        public NOTSpecification(Specification<TSubject> specification) => this.specification = specification;


        public override Expression<Func<TSubject, bool>> ToExpression()
        {
            var expression = specification.ToExpression();
            var negatedExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TSubject, bool>>(negatedExpression, expression.Parameters.Single());
        }
    }
}