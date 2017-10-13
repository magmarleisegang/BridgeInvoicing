using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BridgeInvoicing.Effects
{
    public class ClearEntryEffect : RoutingEffect
    {
        public event EventHandler ClearAction;
        public ClearEntryEffect() : base("Effects.ClearEntryEffect")
        {
        }

        public void OnClearAction(Element element, EventArgs args)
        {
            if (ClearAction != null)
            {
                ClearAction.Invoke(element, args);
            }
        }
    }
}
