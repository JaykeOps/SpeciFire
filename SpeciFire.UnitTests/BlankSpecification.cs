using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public sealed class BlankSpecification<TSubject> : Specification<TSubject>, IBlankSpecification<TSubject>
    {
        internal BlankSpecification() { }


        public Specification<TSubject> OverwriteWith(Specification<TSubject> specification)
        {
            if (this == Blank)
                return specification;
            if (specification == this)
                return this;

            return new AndSpecification<TSubject>(this, specification);
        }


        public override Expression<Func<TSubject, bool>> ToExpression() => x => true;
    }
}
