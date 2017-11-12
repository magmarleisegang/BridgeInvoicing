using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BridgeInvoicing.Helpers;

namespace BridgeInvoicing.Domain
{
    public class Student : IValidatable
    {
        private bool isChanged;
        private List<string> validationErrors;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<string> ValidationErrors
        {
            get
            {
                return validationErrors;
            }
        }

        public bool IsValid()
        {
            validationErrors = new List<string>();
            bool isValid = true;
            if (!Name.CheckNullString())
            {
                validationErrors.Add("Student name is required");
                isValid = false;
            }
            if (!Email.CheckNullString())
            {
                validationErrors.Add("Student email is required");
                isValid = false;
            }
            if (!Phone.CheckNullString())
            {
                validationErrors.Add("Student phone number is required");
                isValid = false;
            }
            return isValid;
        }

        internal bool IsNew()
        {
            return Id == 0;
        }

        internal void SetEmail(string text)
        {
            Email = text;
            isChanged = true;
        }

        internal void SetPhone(string text)
        {
            Phone = text;
            isChanged = true;
        }

        internal bool IsChanged()
        {
            return isChanged;
        }

    }
}
