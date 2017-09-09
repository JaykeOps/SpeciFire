using Xunit;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    [CollectionDefinition("Specification repository test collection")]
    public class SpecificationRepositoryTestCollection : ICollectionFixture<SqliteFixture>
    {
        
    }
}
