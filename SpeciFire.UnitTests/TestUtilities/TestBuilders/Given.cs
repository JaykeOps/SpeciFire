using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpeciFire.UnitTests.TestUtilities._Contact;

namespace SpeciFire.UnitTests.TestUtilities.TestBuilders
{
    internal sealed class Given
    {
        public static PropositionBuilder Proposition => new PropositionBuilder();

        public static PropositionSpecificationBuilder PropositionSpecification => new PropositionSpecificationBuilder();

        public static InitialSpecificationBuilder<IProposition> InitialPropositionSpecficiation 
            => new InitialSpecificationBuilder<IProposition>();

        
    }


 
}