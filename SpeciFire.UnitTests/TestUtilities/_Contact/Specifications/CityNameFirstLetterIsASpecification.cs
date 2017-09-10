using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.TestUtilities._Contact.Specifications
{
    public class CityNameFirstLetterIsASpecification : Specification<Contact>
    {
        public override Expression<Func<Contact, bool>> ToExpression() 
            => contact 
                => string.Equals(contact.Address.City[0].ToString(), "A", StringComparison.OrdinalIgnoreCase);
    }
}