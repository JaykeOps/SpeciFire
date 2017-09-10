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
    public class SpecificationToDbMultiExpressionParameterTests
    {
        private readonly SqliteFixture fixture;


        public SpecificationToDbMultiExpressionParameterTests(SqliteFixture fixture) => this.fixture = fixture;


        [Fact]
        public void Given_SeededContactContext_And_InitialSpecification_FollowedBy_ConjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_AllContactsReturnedShouldHave_LastNamesOrCityNamesWithFirstLetter_H()
        {
            var initialSpecification = Given.InitialSpecification<Contact>().Build();

            var lastNameFirstLetterASpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterASpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameAndCityNameFirstLetterIsASpecification = initialSpecification.Specify
                .From(lastNameFirstLetterASpecification).AND(cityNameFirstLetterASpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts
                    .Where(lastNameAndCityNameFirstLetterIsASpecification.ToExpression().Compile()).ToList();
            }


            contactsResult
                .Should().OnlyContain(x => string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase))
                .And.Subject
                .Should().OnlyContain(x => string.Equals(x.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase),
                "because it asserts that specifications can form conjunctions, with underlying expressions using different parameters." +
                "In other words, it proves that a conjunction of specifications can contain predicates targeting different properties of a subject");
        }

        [Fact]
        public void Given_SeededContactContext_And_InitialSpecification_FollowedBy_DisjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_AllContactsReturnedShouldHave_LastNamesOrCityNamesWithFirstLetter_H()
        {
            var initialSpecification = Given.InitialSpecification<Contact>().Build();

            var lastNameFirstLetterASpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterASpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameOrCityNameFirstLetterIsHSpecification = initialSpecification.Specify
                .From(lastNameFirstLetterASpecification).OR(cityNameFirstLetterASpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts.Where(lastNameOrCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult.Should().OnlyContain(x =>
                string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase)
                ||
                string.Equals(x.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase),
                "because it asserts that specifications can form disjunctions, with underlying expressions using different parameters." +
                "In other words, it proves that a disjunction of specifications can contain predicates targeting different properties of a subject.");
        }


        
    }
}