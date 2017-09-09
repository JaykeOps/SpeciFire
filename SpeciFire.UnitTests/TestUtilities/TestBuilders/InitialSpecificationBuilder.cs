namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class InitialSpecificationBuilder<TSubject>
    {
        public IInitialSpecification<TSubject> Build() => Specification<TSubject>.Initialize;
    }
}