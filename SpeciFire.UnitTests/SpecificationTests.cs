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


        [Fact]
        public void Given_PIsTrueSpecification_And_QIsTrueSpecification_When_SpecifiedAsConjunction_And_ToExpressionIsCalled_Then_AConjunctionOfIsPAndIsQSpecificationsIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPSpecificationStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQSpecificationStub().Build();
            var initialSpecificationSut = Specification<IProposition>.Initialize;


            var predicate = initialSpecificationSut.Specify.SpecificationFrom(isPSpecification).AND(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P AndAlso x.Q)", "because they are equivalent");
        }


        [Fact]
        public void Given_PIsTrueSpecification_And_QIsTrueSpecification_When_SpecifedAsDisjunction_And_ToExpressionIsCalled_Then_ADisjunctionOfIsPAndIsQSpecificationsIsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPSpecificationStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQSpecificationStub().Build();
            var initialSpecificationSut = Specification<IProposition>.Initialize;


            var predicate = initialSpecificationSut.Specify.SpecificationFrom(isPSpecification).OR(isQSpecification)
                .ToExpression();


            predicate.ToString().Should().Be("x => (x.P OrElse x.Q)", "because they are equivalent");

        }

        [Fact]
        public void Given_PIsTrueSpecification_QIsTrueSpecification_And_RIsTrueSpecification_When_SpecifiedAsADisjunctionOf_IsPAndIsQ_Or_IsR_When_CallingToExpression_Then_Expression_IsPAndIsQ_Or_IsR_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPSpecificationStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQSpecificationStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRSpecificationStub().Build();
            var initialSpecificationSut = Specification<IProposition>.Initialize;


            var predicate = initialSpecificationSut.Specify.SpecificationFrom(isPSpecification).AND(isQSpecification)
                .OR(isRSpecification).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) OrElse x.R)", "because they are equivalent");
        }

        [Fact]
        public void Given_PIsTrueSpecification_QIsTrueSpecification_RIsTrueSpecification_And_SIsTrueSpecification_When_SpecifedAsAConjunctionOf_IsPAndIsQ_And_IsROrIsS_When_CallingToExpression_Then_Expression_IsPAndIsQ_And_IsRAndIsS_IsReturned()
        {
            var isPSpecification = Given.PropositionSpecification.IsPSpecificationStub().Build();
            var isQSpecification = Given.PropositionSpecification.IsQSpecificationStub().Build();
            var isRSpecification = Given.PropositionSpecification.IsRSpecificationStub().Build();
            var isSSpecification = Given.PropositionSpecification.IsSSpecificationStub().Build();
            var initialSpecificationSut = Specification<IProposition>.Initialize;


            var predicate = initialSpecificationSut.Specify.SpecificationFrom(isPSpecification).AND(isQSpecification)
                .AND(isRSpecification.OR(isSSpecification)).ToExpression();


            predicate.ToString().Should().Be("x => ((x.P AndAlso x.Q) AndAlso (x.R OrElse x.S))",
                "because they are equivalent");
        }

    }
}
