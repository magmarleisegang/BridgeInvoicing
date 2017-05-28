using BridgeInvoicing.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeInvoicing.ViewModels
{
    public class SessionListGroup : List<Session>
    {
        public string Title { get; set; }
        public string ShortName { get; set; } //will be used for jump lists
        public string Subtitle { get; set; }
        public SessionListGroup(Student student, List<Session> sessions)
        {
            Title = student.Name;
            ShortName = student.Name;

            var list = sessions.ToList();
            list.Sort(new SessionDateComparer());
            this.AddRange(list);
        }
    }
}
