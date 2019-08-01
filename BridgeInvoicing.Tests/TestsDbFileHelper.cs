using BridgeInvoicing.Helpers;
using System;

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
