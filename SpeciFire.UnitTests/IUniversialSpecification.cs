using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public interface IUniversialSpecification<TSubject>
    {
        Specification<TSubject> OverrideWith(Specification<TSubject> specification);
        Expression<Func<TSubject, bool>> ToExpression();
    }
}