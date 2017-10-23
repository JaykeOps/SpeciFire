using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class SimpleSpecificationCompositionTests
    {
        [Fact]
        public void CanFormConjunction()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build(); ;


            var predicate = blankSpecification.OverwriteWith(isPSpecification).And(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P AndAlso x.Q)");
        }


        [Fact]
        public void CanFormDisjunction()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build(); ;


            var predicate = blankSpecification.OverwriteWith(isPSpecification).Or(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P OrElse x.Q)");

        }
    }
}