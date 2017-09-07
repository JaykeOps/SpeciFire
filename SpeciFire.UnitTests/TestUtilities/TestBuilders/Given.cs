namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    public class Given
    {
        public static PropositionBuilder Proposition => new PropositionBuilder();

        public static PropositionSpecificationBuilder PropositionSpecification => new PropositionSpecificationBuilder();
    }
}