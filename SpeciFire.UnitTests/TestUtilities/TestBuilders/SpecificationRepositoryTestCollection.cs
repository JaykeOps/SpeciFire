using Xunit;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    [CollectionDefinition("Specification ToDb test collection")]
    public class SpecificationRepositoryTestCollection : ICollectionFixture<SqliteFixture>
    {
        
    }
}
