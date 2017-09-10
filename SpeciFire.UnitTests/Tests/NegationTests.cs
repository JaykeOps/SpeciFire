using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class NegationTests
    {
        [Fact]
        public void Given_IsPSpecification_When_IsPSpecificationIsNegated_And_ToExpressionIsCalled_Then_Expression_NegationIsP_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();


            var predicate = isPSpecification.NOT.ToExpression();


            predicate.ToString().Should().Be("x => Not(x.P)");
        }


        [Fact]
        public void Given_IsPSpecification_And_IsQSpecification_When_BothAreNegatedSeparately_And_SpecifiedAsAConjunction_When_ToExpressionIsCalled_Then_Expression_NegationIsPAndNegationIsQ_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();

            var predicate = initialSpecificationSut.Specify.From(isPSpecification.NOT).AND(isQSpecification.NOT)
                .ToExpression();


            predicate.ToString().Should().Be("x => (Not(x.P) AndAlso Not(x.Q))");
        }


        [Fact]
        public void Given_IsPSpecification_And_IsQSpecification_When_SpecifiedAsAConjunction_And_ConjunctionIsNegated_When_ToExpressionIsCalled_Then_Expression_Negation_IsPAndIsQ_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var initialSpecificationSut = Given.InitialSpecification<IProposition>().Build();


            var predicate = initialSpecificationSut.Specify.From(isPSpecification).AND(isQSpecification).NOT
                .ToExpression();


            predicate.ToString().Should().Be("x => Not((x.P AndAlso x.Q))");
        }
    }
}