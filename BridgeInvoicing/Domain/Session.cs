using SQLite;
using System;
using BridgeInvoicing.Helpers;
using System.Collections.Generic;

namespace BridgeInvoicing.Domain
{
    public class Session : IValidatable
    {
        private List<string> validationErrors;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Horse { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }

        [Ignore]
        public Student Student { get; private set; }

        public List<string> ValidationErrors
        {
            get
            {
                return validationErrors;
            }
        }

        public void SetStudent(Student student)
        {
            Student = student;
            if (student.Id > 0)
                StudentId = student.Id;
        }

        public bool IsValid()
        {
            validationErrors = new List<string>();
            bool isValid = true;
            if (!Horse.CheckNullString())
            {
                validationErrors.Add("A horse name is required");
                isValid = false;
            }
            return isValid;
        }
    }
}
