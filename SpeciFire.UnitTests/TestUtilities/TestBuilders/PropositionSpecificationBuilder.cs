using Moq;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    public class PropositionSpecificationBuilder
    {
        private readonly Mock<Specification<IProposition>> testDouble = new Mock<Specification<IProposition>>();


        public Specification<IProposition> Build() => testDouble.Object;


        public PropositionSpecificationBuilder IsPStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.P);
            return this;
        }

        public PropositionSpecificationBuilder IsQStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.Q);
            return this;
        }

        public PropositionSpecificationBuilder IsRStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.R);
            return this;
        }

        public PropositionSpecificationBuilder IsSStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.S);
            return this;
        }
    }
}