using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests
{
    public class InitialSpecificationTests
    {

        [Fact]
        public void Given_InitialSpecification_When_CallingToExpression_Then_AlwaysTrueExpressionShouldBeReturned()
        {
            var initialSpecificationSut = Specification<IProposition>.Initialize;

            initialSpecificationSut.ToExpression().ToString()
                .Should().Be("x => True", "because they are equivalent");

        }

        [Fact]
        public void Given_PIsTrueSpecification_When_SpecifiedByInitialSpecification_And_ToExpressionIsCalled_Then_TheExpressionOfSpecificationPIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPSpecificationStub().Build();
            var initialSpecificationSut = Specification<IProposition>.Initialize;


            var predicate = initialSpecificationSut.Specify.SpecificationFrom(isPSpecification).ToExpression();

            predicate.ToString().Should().Be(isPSpecification.ToExpression().ToString(), "because they are equivalent");

        }

    }
}
