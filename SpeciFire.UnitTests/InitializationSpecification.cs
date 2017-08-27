using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    internal sealed class InitializationSpecification<TSubject> : Specification<TSubject>, IInitializationSpecification<TSubject>
    {
        internal InitializationSpecification() { }


        public IInitializationSpecification<TSubject> Specify => this;


        public Specification<TSubject> SpecificationFrom(Specification<TSubject> specification)
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
