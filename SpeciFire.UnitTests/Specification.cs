using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public abstract class Specification<TSubject>
    {
        public static readonly UniversialSpecification<TSubject> UniversialSpecification = new UniversialSpecification<TSubject>();


        public Specification<TSubject> NOT => new NotSpecification<TSubject>(this);


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