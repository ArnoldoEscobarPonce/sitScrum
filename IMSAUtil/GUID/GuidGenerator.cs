using IMSAUtilInterface.GUID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSAUtil.GUID
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
