using SpeciFire.UnitTests.TestUtilities._Contact;
using SpeciFire.UnitTests.TestUtilities._Contact.Specifications;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    public class ContactSpecificationBuilder
    {
        private Specification<Contact> specification;


        public Specification<Contact> Build() => specification;


        public ContactSpecificationBuilder MiamiCity()
        {
            specification = new MiamiCitySpecification();
            return this;
        }

        public ContactSpecificationBuilder LastNameFirstLetterIsASpecification()
        {
            specification = new LastNameFirstLetterIsASpecification();
            return this;
        }

        public ContactSpecificationBuilder CityNameFirstLetterIsASpecification()
        {
            specification = new CityNameFirstLetterIsASpecification();
            return this;
        }

    }
}