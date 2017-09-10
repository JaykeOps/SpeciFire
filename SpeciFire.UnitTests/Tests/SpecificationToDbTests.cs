﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;
using SpeciFire.UnitTests.TestUtilities._Contact;
using Xunit;

namespace SpeciFire.UnitTests.Tests
{
    [Collection("Specification ToDb test collection")]
    public class SpecificationToDbTests
    {
        private SqliteFixture fixture;


        public SpecificationToDbTests(SqliteFixture fixture) => this.fixture = fixture;


        [Fact]
        public void Given_SeededContactContext_And_InitialSpecification_When_QueryingContactContext_AllContactsShouldBeReturned()
        {
            var initialSpecification = Given.InitialSpecification<Contact>().Build();
            int contactSetCount;
            int queryResultCount;

            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactSetCount = context.Contacts.Count();
            }


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                queryResultCount = context.Contacts.Where(initialSpecification.ToExpression().Compile()).Count();
            }


            queryResultCount.Should().Be(contactSetCount).And.Subject.Should().NotBe(0);
        }


        [Fact]
        public void Given_SeededContactContext_And_MiamiCitySpecificationUponInitialSpecification_When_ContactsContextIsQueried_Then_AllContactsReturnedShouldHaveCityValue_Miami()
        {
            var miamiCitySpecification = Given.ContactSpecification.MiamiCity().Build();
            var initialSpecification = Given.InitialSpecification<Contact>().Build();
            miamiCitySpecification = initialSpecification.Specify.From(miamiCitySpecification);

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
        public void Given_SeededContactContext_And_LastNameFirstLetteIsA_And_CityNameFirstletterIsA_UponInitialSpecification_AllContactsReturnedShouldHave_LastNameAndCityNameWithFirstLetterA()
        {
            var initialSpecification = Given.InitialSpecification<Contact>().Build();

            var lastNameFirstLetterASpecification =
                Given.ContactSpecification.LastNameFirstLetterIsASpecification().Build();

            var cityNameFirstLetterASpecification =
                Given.ContactSpecification.CityNameFirstLetterIsASpecification().Build();

            var lastNameAndCityNameFirstLetterIsASpecification = initialSpecification.Specify
                .From(lastNameFirstLetterASpecification).AND(cityNameFirstLetterASpecification);

            IReadOnlyList<Contact> contactsResult;



            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactsResult = context.Contacts
                    .Where(lastNameAndCityNameFirstLetterIsASpecification.ToExpression().Compile()).ToList();
            }


            contactsResult
                .Should().OnlyContain(x => string.Equals(x.Name.LastName[0].ToString(), "A", StringComparison.OrdinalIgnoreCase))
                .And.Subject
                .Should().OnlyContain(x => string.Equals(x.Address.City[0].ToString(), "A", StringComparison.OrdinalIgnoreCase));
        }
    }
}