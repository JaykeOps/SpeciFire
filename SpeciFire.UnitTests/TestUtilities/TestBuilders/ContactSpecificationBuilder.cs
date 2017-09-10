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

        public ContactSpecificationBuilder LastNamesFirstLetterIsHSpecification()
        {
            specification = new LastNamesFirstLetterIsHSpecification();
            return this;
        }

        public ContactSpecificationBuilder CityNamesFirstLetterIsHSpecification()
        {
            specification = new CityNamesFirstLetterIsHSpecification();
            return this;
        }

    }
}