using Moq;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class PropositionBuilder
    {
        private readonly Mock<IProposition> testDouble = new Mock<IProposition>();


        public IProposition Build() => testDouble.Object;


        public PropositionBuilder Dummy() => this;


        public PropositionBuilder Stub(
            bool p = false, bool q = false, 
            bool r = false, bool s = false,
            bool t = false, bool u = false)
        {
            testDouble.SetupGet(x => x.P).Returns(p);
            testDouble.SetupGet(x => x.Q).Returns(q);
            testDouble.SetupGet(x => x.R).Returns(r);
            testDouble.SetupGet(x => x.S).Returns(s);
            testDouble.SetupGet(x => x.T).Returns(t);
            testDouble.SetupGet(x => x.U).Returns(u);

            return this;
        }
         
    }
}