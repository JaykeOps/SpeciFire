using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class BlankSpecificationTests
    {

        [Fact]
        public void ExpressionShouldBeTrueForAll()
        {
            var blankSpecificationSut = Given.BlankSpecification<IProposition>().Real().Build();

            blankSpecificationSut.ToExpression().ToString()
                .Should().Be("x => True");

        }

        [Fact]
        public void CanBeOverridenBySpecification()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var blankSpecificationSut = Given.BlankSpecification<IProposition>().Real().Build();


            var predicate = blankSpecificationSut.OverwriteWith(isPSpecification).ToExpression();

            predicate.ToString().Should().Be("x => x.P");

        }

    }
}
