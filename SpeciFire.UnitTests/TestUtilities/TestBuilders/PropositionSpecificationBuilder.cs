using System;
using System.Linq.Expressions;
using Moq;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    public class PropositionSpecificationBuilder
    {
        private readonly Mock<Specification<IProposition>> testDouble = new Mock<Specification<IProposition>>();


        public Specification<IProposition> Build() => testDouble.Object;


        public PropositionSpecificationBuilder IsPSpecificationStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.P);
            return this;
        }

        public PropositionSpecificationBuilder IsQSpecificationStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.Q);
            return this;
        }

        public PropositionSpecificationBuilder IsRSpecificationStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.R);
            return this;
        }

        public PropositionSpecificationBuilder IsSSpecificationStub()
        {
            testDouble.Setup(x => x.ToExpression()).Returns(x => x.S);
            return this;
        }
    }
}