using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Helpers
{
    public interface IPermissionChecker
    {
        bool FileAccessPermission();
    }
}
