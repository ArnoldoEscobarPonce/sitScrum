using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sitScrum.util
{
    public class GuidGenerator : IGuidGenerator
    {
        private Guid guid;
        public GuidGenerator()
        {
            guid = Guid.NewGuid();
        }

        public string getGuid()
        {
            return guid.ToString();
        }

    }
}
