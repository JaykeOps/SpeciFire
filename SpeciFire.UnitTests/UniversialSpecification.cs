using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public sealed class UniversialSpecification<TSubject> : Specification<TSubject>, IUniversialSpecification<TSubject>
    {
        internal UniversialSpecification() { }


        public Specification<TSubject> OverrideWith(Specification<TSubject> specification)
        {
            if (this == UniversialSpecification)
                return specification;
            if (specification == this)
                return this;

            return new AndSpecification<TSubject>(this, specification);
        }


        public override Expression<Func<TSubject, bool>> ToExpression() => x => true;
    }
}
