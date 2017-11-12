using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.Domain
{
    public interface IValidatable
    {
         bool IsValid();
        List<string> ValidationErrors { get; }
    }
}
