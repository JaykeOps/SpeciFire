using Xunit;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    [CollectionDefinition("Specification ToDbContext test collection")]
    public class SeededContactContextCollection : ICollectionFixture<SqliteFixture>
    {
        
    }
}
