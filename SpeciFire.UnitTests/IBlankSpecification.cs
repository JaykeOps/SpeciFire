using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests
{
    public interface IBlankSpecification<TSubject>
    {
        Specification<TSubject> OverwriteWith(Specification<TSubject> specification);
        Expression<Func<TSubject, bool>> ToExpression();
    }
}