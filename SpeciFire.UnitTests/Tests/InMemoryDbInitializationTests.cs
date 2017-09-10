using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using SpeciFire.UnitTests.TestUtilities._Contact;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    [Collection("Specification ToDb test collection")]
    public class InMemoryDbInitializationTests
    {
        private SqliteFixture fixture;

        public InMemoryDbInitializationTests(SqliteFixture fixture) => this.fixture = fixture;

        [Fact]
        public void Given_SeededContactContext_Then_ContactsSetShouldNotBeEmptyOrNull()
        {
            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                context.Database.EnsureCreated();
                context.Contacts.Should().NotBeNullOrEmpty();
            }
        }

    }
}