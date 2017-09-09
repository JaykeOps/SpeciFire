using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    [Collection("Specification repository test collection")]
    public class SpecificationRepositoryTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Given_RepositoryIsInstantiatedWithContactContext_And_FindByIdIsCalledGivenId_Then_ContactWithGivenIdIsReturned(int id)
        {

            
        }
    }
}