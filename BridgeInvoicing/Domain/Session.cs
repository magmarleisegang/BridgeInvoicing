using SQLite;
using System;

namespace BridgeInvoicing.Domain
{
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Horse { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }

        [Ignore]
        public Student Student { get; private set; }

        public void SetStudent(Student student)
        {
            Student = student;
            if (student.Id > 0)
                StudentId = student.Id;
        }
    }
}
