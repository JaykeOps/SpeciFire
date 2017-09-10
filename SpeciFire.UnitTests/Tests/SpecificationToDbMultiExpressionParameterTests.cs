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
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_ConjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_InvalidOperationExceptionShouldNotBeThrown()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                universalSpecification.OverrideWith(lastNameFirstLetterIsHSpecification)
                    .AND(cityNameFirstLetterIsHSpecification);



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                Action act = () => context.Contacts.Where(specification.ToExpression()
                    .Compile());


                act.ShouldNotThrow<InvalidOperationException>();

                /*
                 * Because it asserts that specifications can form conjunctions, with underlying expressions using different parameters.
                 * "In other words, it proves that a conjunction of specifications can contain predicates targeting different properties of a given subject.
                */
            }
        }


        [Fact]
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_DisjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_InvalidOperationExceptionShouldNotBeThrown()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                universalSpecification.OverrideWith(lastNameFirstLetterIsHSpecification)
                    .OR(cityNameFirstLetterIsHSpecification);



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                Action act = () => context.Contacts.Where(specification.ToExpression()
                    .Compile());


                act.ShouldNotThrow<InvalidOperationException>();

                /*
                 * Because it asserts that specifications can form disjunctions, with underlying expressions using different parameters.
                 * In other words, it proves that a disjunction of specifications can contain predicates targeting different properties of a given subject.
                */
            }
        }


        [Fact]
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_ConjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_AllContactsReturnedShouldHave_LastNamesOrCityNamesWithFirstLetter_H()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameAndCityNameFirstLetterIsHSpecification = universalSpecification
                .OverrideWith(lastNameFirstLetterIsHSpecification).AND(cityNameFirstLetterHSpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts
                    .Where(lastNameAndCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult
                .Should().OnlyContain(x => string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase))
                .And.Subject
                .Should().OnlyContain(x => string.Equals(x.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_DisjunctionOf_LastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_AllContactsReturnedShouldHave_LastNamesOrCityNamesWithFirstLetter_H()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameOrCityNameFirstLetterIsHSpecification = universalSpecification
                .OverrideWith(lastNameFirstLetterIsHSpecification).OR(cityNameFirstLetterIsHSpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts.Where(lastNameOrCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult.Should().OnlyContain(x =>
                string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase)
                ||
                string.Equals(x.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_ConjunctionOf_NegationLastNamesFirstLetterIsHAndCityNamesFirstLetterIsH_When_QueryingContactContext_Then_InvalidOperationExceptionShouldNotBeThrown()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                universalSpecification.OverrideWith(lastNameFirstLetterIsHSpecification.NOT)
                    .AND(cityNameFirstLetterIsHSpecification);



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                Action act = () => context.Contacts.Where(specification.ToExpression()
                    .Compile());


                act.ShouldNotThrow<InvalidOperationException>();
            }
        }
    }
}
 