using System;
using System.Linq.Expressions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;

namespace SpeciFire.UnitTests
{
    public class IsQSpecification : Specification<IProposition>
    {
        public override Expression<Func<IProposition, bool>> ToExpression() => x => x.Q;
    }
}