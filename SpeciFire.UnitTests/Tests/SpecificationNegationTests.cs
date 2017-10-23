using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    public class SpecificationNegationTests
    {
        [Fact]
        public void CanBeNegated()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();


            var predicate = isPSpecification.Not.ToExpression();


            predicate.ToString().Should().Be("x => Not(x.P)");
        }


        [Fact]
        public void CanNegateSpecificationsInConjunctionSeparately()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build();


            var predicate = blankSpecification.OverwriteWith(isPSpecification.Not).And(isQSpecification.Not)
                .ToExpression();


            predicate.ToString().Should().Be("x => (Not(x.P) AndAlso Not(x.Q))");
        }


        [Fact]
        public void CanNegateConjunction()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var blankSpecification = Given.BlankSpecification<IProposition>().Stub().Build();


            var predicate = blankSpecification.OverwriteWith(isPSpecification).And(isQSpecification.Not).Not
                .ToExpression();


            predicate.ToString().Should().Be("x => Not((x.P AndAlso Not(x.Q)))");
        }
    }
}