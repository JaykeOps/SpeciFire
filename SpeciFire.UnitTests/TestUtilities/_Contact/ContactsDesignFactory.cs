using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SpeciFire.UnitTests.TestUtilities.TestBuilders;

namespace SpeciFire.UnitTests.TestUtilities._Contact
{
    public class ContactsDesignFactory : IDesignTimeDbContextFactory<ContactContext>
    {
        public ContactContext CreateDbContext(string[] args) => 
            new ContactContext(new DbContextOptionsBuilder()
                .UseSqlite("Data Source=file::memory:?cache=shared").Options);
    }
}