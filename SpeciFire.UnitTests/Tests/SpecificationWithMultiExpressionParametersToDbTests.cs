using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using SpeciFire.UnitTests.TestUtilities._Contact;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    [Collection("Specification ToDbContext test collection")]
    public class SpecificationWithMultiExpressionParametersToDbTests
    {
        private readonly SqliteFixture fixture;


        public SpecificationWithMultiExpressionParametersToDbTests(SqliteFixture fixture) => this.fixture = fixture;


        [Fact]
        public void DoesNotThrowInvalidOperationExceptionWhileQueryingDbSetGivenConjunction()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                blankSpecification.OverwriteWith(lastNameFirstLetterIsHSpecification)
                    .And(cityNameFirstLetterIsHSpecification);



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
        public void DoesNotThrowInvalidOperationExceptionWhileQueryingDbSetGivenDisjunction()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                blankSpecification.OverwriteWith(lastNameFirstLetterIsHSpecification)
                    .Or(cityNameFirstLetterIsHSpecification);



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
        public void DoesNotThrowInvalidOperationExceptionWhileQueryingDbSetGivenConjunctionWithNegation()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var specification =
                blankSpecification.OverwriteWith(lastNameFirstLetterIsHSpecification.Not)
                    .And(cityNameFirstLetterIsHSpecification);



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                Action act = () => context.Contacts.Where(specification.ToExpression()
                    .Compile());


                act.ShouldNotThrow<InvalidOperationException>();
            }
        }



        [Fact]
        public void CanGetContactsWhereLastNameAndCityFirstLetterIsH()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameAndCityNameFirstLetterIsHSpecification = blankSpecification
                .OverwriteWith(lastNameFirstLetterIsHSpecification).And(cityNameFirstLetterHSpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts
                    .Where(lastNameAndCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult
                .Should().OnlyContain(x => string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase))
                .And
                .Subject
                .Should().OnlyContain(x => string.Equals(x.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }


        [Fact]
        public void CanGetContactsWhereLastNameOrCityFirstLetterIsH()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameOrCityNameFirstLetterIsHSpecification = blankSpecification
                .OverwriteWith(lastNameFirstLetterIsHSpecification).Or(cityNameFirstLetterIsHSpecification)
                .And(lastNameFirstLetterIsHSpecification
                    .And(cityNameFirstLetterIsHSpecification).Not);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts.Where(lastNameOrCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult.Should().OnlyContain(contact =>
                    string.Equals(contact.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase)
                    ||
                    string.Equals(contact.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }


        [Fact]
        public void CanGetContactsWhereLastNameXorCityFirstLetterIsH()
        {
            var blankSpecification = Given.BlankSpecification<Contact>().Real().Build();

            var lastNameFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var cityNameFirstLetterIsHSpecification =
                Given.ContactSpecification.CityNamesFirstLetterIsHSpecification().Build();

            var lastNameOrCityNameFirstLetterIsHSpecification = blankSpecification
                .OverwriteWith(lastNameFirstLetterIsHSpecification).Or(cityNameFirstLetterIsHSpecification)
                .And(lastNameFirstLetterIsHSpecification
                    .And(cityNameFirstLetterIsHSpecification).Not);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts.Where(lastNameOrCityNameFirstLetterIsHSpecification.ToExpression().Compile()).ToList();
            }


            contactsResult.Should().OnlyContain(contact =>
                string.Equals(contact.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase)
                ||
                string.Equals(contact.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase))
                .And
                .Subject
                .Should().NotContain(contact => string.Equals(contact.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase)
                && 
                string.Equals(contact.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }


        
    }
}
 