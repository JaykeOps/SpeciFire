using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class InitialSpecificationTests
    {

        [Fact]
        public void Given_InitialSpecification_When_CallingToExpression_Then_AlwaysTrueExpressionShouldBeReturned()
        {
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();

            initialSpecificationSut.ToExpression().ToString()
                .Should().Be("x => True", "because they are equivalent");

        }

        [Fact]
        public void Given_PIsTrueSpecification_When_SpecifiedByInitialSpecification_And_ToExpressionIsCalled_Then_TheExpressionOfSpecificationPIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();


            var predicate = initialSpecificationSut.Specify.From(isPSpecification).ToExpression();

            predicate.ToString().Should().Be(isPSpecification.ToExpression().ToString(), "because they are equivalent");

        }

    }
}
