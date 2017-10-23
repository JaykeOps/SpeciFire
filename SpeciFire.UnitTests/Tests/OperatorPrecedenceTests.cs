using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests
{
    public class OperatorPrecedenceTests
    {
        [Fact]
        public void CanRespectOperatorPrecedenceGivenConjunctionFollowedByDisjunction()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build(); ;


            var predicate = blankSpecification.OverwriteWith(isPSpecification).And(isQSpecification)
                .Or(isRSpecification).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) OrElse x.R)");
        }

        [Fact]
        public void CanRespectOperatorPrecedenceGivenConjunctionFollowedByConjunctionWithNestedDisjunction()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRStub().Build();
            var isSSpecification = Given.PropositionSpecification.IsSStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build();


            var predicate = blankSpecification.OverwriteWith(isPSpecification).And(isQSpecification)
                .And(isRSpecification.Or(isSSpecification)).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) AndAlso (x.R OrElse x.S))");
        }
    }
}