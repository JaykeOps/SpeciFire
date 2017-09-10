using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class UniversalSpecificationTests
    {

        [Fact]
        public void Given_UniversalSpecification_When_CallingToExpression_Then_AlwaysTrueExpressionShouldBeReturned()
        {
            var universalSpecificationSut = Specification<IProposition>.UniversialSpecification;

            universalSpecificationSut.ToExpression().ToString()
                .Should().Be("x => True");

        }

        [Fact]
        public void Given_PIsTrueSpecification_When_SpecifiedByUniversalSpecification_And_ToExpressionIsCalled_Then_TheExpressionOfSpecificationPIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var universalSpecification = Specification<IProposition>.UniversialSpecification;


            var predicate = universalSpecification.OverrideWith(isPSpecification).ToExpression();

            predicate.ToString().Should().Be("x => x.P");

        }

    }
}
