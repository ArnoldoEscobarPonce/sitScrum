using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sitScrum.util
{
    public class GuidGenerator_old : IGuidGenerator_old
    {
        private Guid guid;
        public GuidGenerator_old()
        {
            guid = Guid.NewGuid();
        }

        public string getGuid()
        {
            return guid.ToString();
        }

    }
}
