namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class Given
    {
        public static PropositionBuilder Proposition => new PropositionBuilder();

        public static PropositionSpecificationBuilder PropositionSpecification => new PropositionSpecificationBuilder();

        public static UniversialSpecificationBuilder<TSubject> UniversialSpecificationStub<TSubject>() 
            => new UniversialSpecificationBuilder<TSubject>();

        public static ContactSpecificationBuilder ContactSpecification => new ContactSpecificationBuilder();
    }
}