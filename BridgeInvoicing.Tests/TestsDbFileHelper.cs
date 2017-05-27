using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Tests
{
    internal class TestsDbFileHelper : IDbFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine(Environment.CurrentDirectory, filename);
        }
    }
}
