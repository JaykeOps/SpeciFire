using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public abstract class Specification<TSubject>
    {
        public static readonly IInitialSpecification<TSubject> Initialize = new InitialSpecification<TSubject>();


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
}