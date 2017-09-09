using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests
{
    public class NegationTests
    {
        [Fact]
        public void Given_IsPSpecification_When_IsPSpecificationIsNegated_And_ToExpressionIsCalled_Then_Expression_NegationPIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();


            var predicate = isPSpecification.NOT.ToExpression();


            predicate.ToString().Should().Be("x => Not(x.P)", "because they are equivalent");
        }
    }
}