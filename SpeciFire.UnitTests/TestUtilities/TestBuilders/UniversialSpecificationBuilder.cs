using Moq;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class UniversialSpecificationBuilder<TSubject>
    {
        public IUniversialSpecification<TSubject> Build()
        {
            var universalSpecification = new Mock<IUniversialSpecification<TSubject>>();

            universalSpecification.Setup(x => x.ToExpression()).Returns(x => true);

            universalSpecification.Setup(x => x.OverrideWith(It.IsAny<Specification<TSubject>>()))
                .Returns<Specification<TSubject>>(x => x);

            return universalSpecification.Object;
        }
    }
}