using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpeciFire.UnitTests.TestUtilities._Contact;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    public class SqliteFixture
    {

        internal const string ConnectionString = "Data Source=file::memory:?cache=shared";

        internal readonly SqliteConnection TestDbConnection;

        internal readonly DbContextOptions TestContextOptions;


        public SqliteFixture()
        {
            TestDbConnection = new SqliteConnection(ConnectionString);
            TestContextOptions = new DbContextOptionsBuilder().UseSqlite(TestDbConnection).Options;
            SeedInMemoryDb();
        }

        private void SeedInMemoryDb()
        {
            using (var context = new ContactContext(TestContextOptions))
            {
                context.Database.OpenConnection();

                context.Database.EnsureCreated();
                context.Contacts.AddRange(GetContactData());
                context.SaveChanges();
            }
        }

        private IReadOnlyList<Contact> GetContactData()
        {
            List<Contact> testData;

            using (StreamReader file = File.OpenText(@"..\\..\\..\\TestUtilities\\ContactTestData.json"))
            {
                var serializer = new JsonSerializer();
                testData = (List<Contact>)serializer.Deserialize(file, typeof(List<Contact>));

            }

            return testData;
        }


        
    }
}
