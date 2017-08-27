using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public interface IInitializationSpecification<TSubject>
    {
        IInitializationSpecification<TSubject> Specify { get; }
        Specification<TSubject> SpecificationFrom(Specification<TSubject> specification);
        Expression<Func<TSubject, bool>> ToExpression();
    }
}