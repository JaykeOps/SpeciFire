namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class Given
    {
        public static PropositionBuilder Proposition => new PropositionBuilder();

        public static PropositionSpecificationBuilder PropositionSpecification => new PropositionSpecificationBuilder();

        public static BlankSpecificationBuilder<TSubject> BlankSpecification<TSubject>() 
            => new BlankSpecificationBuilder<TSubject>();

        public static ContactSpecificationBuilder ContactSpecification => new ContactSpecificationBuilder();
    }
}