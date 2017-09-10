using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.TestUtilities._Contact.Specifications
{
    public class MiamiCitySpecification : Specification<Contact>
    {
        public override Expression<Func<Contact, bool>> ToExpression()
            => contact
                => string.Equals(contact.Address.City, "Miami", StringComparison.OrdinalIgnoreCase);
    }
}