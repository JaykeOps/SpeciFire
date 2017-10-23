using Moq;
using SpeciFire.UnitTests.TestUtilities._Contact.Specifications;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class BlankSpecificationBuilder<TSubject>
    {
        private IBlankSpecification<TSubject> blankSpecification;


        public IBlankSpecification<TSubject> Build() => blankSpecification;



        public BlankSpecificationBuilder<TSubject> Real()
        {
            blankSpecification = Specification<TSubject>.Blank;
            return this;
        }


        public BlankSpecificationBuilder<TSubject> Stub()
        {
            var stub = new Mock<IBlankSpecification<TSubject>>();

            stub.Setup(x => x.ToExpression()).Returns(x => true);

            stub.Setup(x => x.OverwriteWith(It.IsAny<Specification<TSubject>>()))
                .Returns<Specification<TSubject>>(x => x);

            blankSpecification = stub.Object;
            return this;
        }
    }
}