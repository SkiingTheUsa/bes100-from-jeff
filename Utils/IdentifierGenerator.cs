using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Utils
{
    public class IdentifierGenerator : IGenerateIds
    {

        public Guid GetEnrollmentId()
        {
            return Guid.NewGuid(); // go to the database, do whatever...
        }
    }
}
