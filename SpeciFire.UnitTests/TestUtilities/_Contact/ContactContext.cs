using Microsoft.EntityFrameworkCore;

namespace SpeciFire.UnitTests.TestUtilities._Contact
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>()
                .HasKey(c => c.Id);

            builder.Entity<Contact>().OwnsOne(c => c.Name);
            builder.Entity<Contact>().OwnsOne(c => c.Address);
        }
    }
}