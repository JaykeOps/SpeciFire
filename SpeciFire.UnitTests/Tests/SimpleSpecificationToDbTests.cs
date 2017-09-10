﻿using System;
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
        public void Given_SeededContactContext_And_universalSpecification_When_QueryingContactContext_AllContactsShouldBeReturned()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();
            int contactSetCount;
            int queryResultCount;

            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactSetCount = context.Contacts.Count();
            }


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                queryResultCount = context.Contacts.Where(universalSpecification.ToExpression().Compile()).Count();
            }


            queryResultCount.Should().Be(contactSetCount).And.Subject.Should().NotBe(0);
        }


        [Fact]
        public void Given_SeededContactContext_And_UniversalSpecification_FollowedBy_MiamiCitySpecification_When_QueryingContactContext_Then_AllContactsReturnedShouldHaveCityValue_Miami()
        {
            var miamiCitySpecification = Given.ContactSpecification.MiamiCity().Build();
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();
            miamiCitySpecification = universalSpecification.OverrideWith(miamiCitySpecification);

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
        public void Given_SeededConactContext_And_UniversalSpecification_FollowedBy_NegationLastNamesFirstLetterIsHSpecification_When_QueryingContactContext_Then_NoContactsReturnedShouldContainALastNameWithFirstLetter_H()
        {
            var universalSpecification = Given.UniversialSpecificationStub<Contact>().Build();
            var lastNamesFirstLetterIsHSpecification =
                Given.ContactSpecification.LastNamesFirstLetterIsHSpecification().Build();

            var lastNameFirstLetterIsNotHSpecification =
                universalSpecification.OverrideWith(lastNamesFirstLetterIsHSpecification.NOT);

            IReadOnlyList<Contact> contactResult;


            using (var context = new ContactContext(fixture.TestContextOptions))
            {
                contactResult = context.Contacts.Where(lastNameFirstLetterIsNotHSpecification.ToExpression().Compile())
                    .ToList();
            }


            contactResult.Should().OnlyContain(x =>
                !string.Equals(x.Name.LastName[0].ToString(), "H", StringComparison.OrdinalIgnoreCase));
        }
        
    }
}