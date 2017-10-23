using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using SpeciFire.UnitTests.TestUtilities._Contact;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    [Collection("Specification ToDbContext test collection")]
    public class SimpleSpecificationToDbTests
    {
        private readonly SqliteFixture fixture;


        public SimpleSpecificationToDbTests(SqliteFixture fixture) => this.fixture = fixture;


        [Fact]
        public void CanGetAllContacts()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Stub().Build();
            int contactSetCount;
            int queryResultCount;

            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactSetCount = context.Contacts.Count();
            }


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                queryResultCount = context.Contacts.Where(blankSpecification.ToExpression().Compile()).Count();
            }


            queryResultCount.Should().Be(contactSetCount).And.Subject.Should().NotBe(0);
        }


        [Fact]
        public void CanGetAllContactsInMiami()
        {
            var miamiCitySpecification = Given.ContactSpecification.MiamiCity().Build();
            var blankSpecification = Given.BlankSpecification<Contact>().Stub().Build();
            miamiCitySpecification = blankSpecification.OverwriteWith(miamiCitySpecification);

            IReadOnlyList<Contact> contactsResult;


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts.Where(miamiCitySpecification.ToExpression().Compile()).ToList();
            }


            contactsResult.Should()
                .OnlyContain(x => string.Equals(x.Address.City, "Miami", StringComparison.OrdinalIgnoreCase))
                .And.Subject.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public void CanGetAllContactsWhereLastNameFirstLetterIsNotH()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Stub().Build();
            var lastNamesFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var lastNamesFirstLetterIsNotHSpecification =
                blankSpecification.OverwriteWith(lastNamesFirstLetterIsHSpecification.Not);

            IReadOnlyList<Contact> contactResult;


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactResult = context.Contacts.Where(lastNamesFirstLetterIsNotHSpecification.ToExpression().Compile())
                    .ToList();
            }


            contactResult.Should().OnlyContain(x =>
                !string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }
        
    }
}