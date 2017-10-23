using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public abstract class Specification<TSubject>
    {
        public static readonly BlankSpecification<TSubject> Blank = new BlankSpecification<TSubject>();


        public Specification<TSubject> Not => new NotSpecification<TSubject>(this);


        public bool IsSatisfiedBySubject(TSubject subject)
        {
            Func<TSubject, bool> predicate = ToExpression().Compile();
            return predicate(subject);
        }


        public abstract Expression<Func<TSubject, bool>> ToExpression();


        public Specification<TSubject> And(Specification<TSubject> specification) => new AndSpecification<TSubject>(this, specification);

        public Specification<TSubject> Or(Specification<TSubject> specification) =>
            new OrSpecification<TSubject>(this, specification);
    }
}