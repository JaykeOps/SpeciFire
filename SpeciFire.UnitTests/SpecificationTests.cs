using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests
{
    public class SpecificationTests
    {
        [Fact]
        public void Given_InitialSpecification_When_CallingToExpression_Then_AlwaysTrueExpressionShouldBeReturned()
        {
            var initializationSut = Specification<IProposition>.Initialize;

            initializationSut.ToExpression().ToString()
                .Should().Be("x => True", "because they are equivalent");

        }


        [Fact]
        public void Given_PIsTrueSpecification_When_SpecifiedByInitializationSpecification_When_ToExpressionIsCalled_Then_TheExpressionOfSpecificationPIsReturned()
        {
            var leftSpecificationSut = new IsPSpecification();
            var initializationSut = Specification<IProposition>.Initialize;


            var predicate = initializationSut.Specify.SpecificationFrom(leftSpecificationSut).ToExpression();

            predicate.ToString().Should().Be(leftSpecificationSut.ToExpression().ToString(), "because they are equivalent");

        }


    }
}
