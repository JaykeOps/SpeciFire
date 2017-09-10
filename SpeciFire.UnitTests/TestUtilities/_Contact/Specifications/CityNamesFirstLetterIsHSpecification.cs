using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.TestUtilities._Contact.Specifications
{
    public class CityNamesFirstLetterIsHSpecification : Specification<Contact>
    {
        public override Expression<Func<Contact, bool>> ToExpression() 
            => contact 
                => string.Equals(contact.Address.City[0].ToString(), "H", StringComparison.OrdinalIgnoreCase);
    }
}