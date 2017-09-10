using System;
using System.Linq.Expressions;

namespace SpeciFire.UnitTests.TestUtilities._Contact.Specifications
{
    public class LastNameFirstLetterIsASpecification : Specification<Contact>
    {
        public override Expression<Func<Contact, bool>> ToExpression() 
            => contact 
                => string.Equals(contact.Name.LastName[0].ToString(), "A", StringComparison.OrdinalIgnoreCase);
    }
}