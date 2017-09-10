using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class SimpleSpecificationCompositionTests
    {
        [Fact]
        public void Given_PIsTrueSpecification_And_QIsTrueSpecification_When_SpecifiedAsConjunction_And_ToExpressionIsCalled_Then_AConjunctionOfIsPAndIsQSpecificationsIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();


            var predicate = initialSpecificationSut.Specify.From(isPSpecification).AND(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P AndAlso x.Q)", "because they are equivalent");
        }


        [Fact]
        public void Given_PIsTrueSpecification_And_QIsTrueSpecification_When_SpecifedAsDisjunction_And_ToExpressionIsCalled_Then_ADisjunctionOfIsPAndIsQSpecificationsIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();


            var predicate = initialSpecificationSut.Specify.From(isPSpecification).OR(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P OrElse x.Q)", "because they are equivalent");

        }
    }
}