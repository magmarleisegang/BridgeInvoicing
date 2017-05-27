using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Domain
{
    internal class SessionDateComparer : IComparer<Session>
    {
        public int Compare(Session x, Session y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
