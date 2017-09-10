using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using Xunit;

namespace SpeciFire.UnitTests
{
    public class NestedSpecificationCompositionTests
    {
        [Fact]
        public void Given_UniversalSpecification_FollowedBy_PIsTrueSpecification_QIsTrueSpecification_And_RIsTrueSpecification_When_SpecifiedAsADisjunctionOf_IsPAndIsQ_Or_IsR_When_CallingToExpression_Then_Expression_IsPAndIsQ_Or_IsR_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRStub().Build();
            var universalSpecification = Given.UniversialSpecificationStub<IProposition>().Build();


            var predicate = universalSpecification.OverrideWith(isPSpecification).AND(isQSpecification)
                .OR(isRSpecification).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) OrElse x.R)");
        }

        [Fact]
        public void Given_UniversalSpecification_FollowedBy_PIsTrueSpecification_QIsTrueSpecification_RIsTrueSpecification_And_SIsTrueSpecification_When_SpecifedAsAConjunctionOf_IsPAndIsQ_And_IsROrIsS_When_CallingToExpression_Then_Expression_IsPAndIsQ_And_IsRAndIsS_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRStub().Build();
            var isSSpecification = Given.PropositionSpecification.IsSStub().Build();
            var universalSpecification = Given.UniversialSpecificationStub<IProposition>().Build();


            var predicate = universalSpecification.OverrideWith(isPSpecification).AND(isQSpecification)
                .AND(isRSpecification.OR(isSSpecification)).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) AndAlso (x.R OrElse x.S))");
        }
    }
}