using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    internal sealed class InitialSpecification<TSubject> : Specification<TSubject>, IInitialSpecification<TSubject>
    {
        internal InitialSpecification() { }


        public IInitialSpecification<TSubject> Specify => this;


        public Specification<TSubject> From(Specification<TSubject> specification)
        {
            if (this == Initialize)
                return specification;
            if (specification == this)
                return this;

            return new AndSpecification<TSubject>(this, specification);
        }


        public override Expression<Func<TSubject, bool>> ToExpression() => x => true;
    }
}
