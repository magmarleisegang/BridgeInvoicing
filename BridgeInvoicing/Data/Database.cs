using BridgeInvoicing.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BridgeInvoicing.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Student>().Wait();
            _database.CreateTableAsync<Session>().Wait();
        }

        public Task<Student> GetStudentById(int id)
        {
            var student = _database.Table<Student>().Where(s => s.Id == id).FirstOrDefaultAsync();
            return student;
        }

        public Task<List<Student>> SearchStudent(string name)
        {
            var list = _database.Table<Student>().Where(s => s.Name.Contains(name));
            return list.ToListAsync();
        }

        public Task<int> SaveStudent(Student student)
        {
            if (student.Id == 0)
            {
                return _database.InsertAsync(student);
            }
            else
            {
                return _database.UpdateAsync(student);
            }
        }

        public async Task<List<ViewModels.Horse>> SearchHorseName(string name)
        {
            var results = await _database.QueryAsync<ViewModels.Horse>(
                "select DISTINCT Horse as [Name] from [Session] where Horse like '%' || @name || '%' ORDER BY [Name]", name);
            return results;
        }

        public async Task<int> AddSession(Session session)
        {            
            return await _database.InsertAsync(session);
        }

        public Task<List<Session>> GetAllSessions(DateTime from, DateTime to, int? studentId)
        {
            var asyncQueryable = _database.Table<Session>().Where(s => s.Date >= from && s.Date <= to);
            if (studentId.HasValue)
                asyncQueryable = asyncQueryable.Where(s => s.StudentId == studentId);
            return asyncQueryable.ToListAsync();
        }

//        public Task<List<Session>> GetAllSessionsWithStudent(DateTime from, DateTime to, int? studentId)
//        {
//            var results = _database.QueryAsync<Session>(@"
//select DISTINCT Horse as [Name] from [Session] 
//where Horse like @name || '%'", name);

//            var asyncQueryable = _database.Table<Session>().Where(s => s.Date >= from && s.Date <= to);
//            if (studentId.HasValue)
//                asyncQueryable = asyncQueryable.Where(s => s.StudentId == studentId);

//            return asyncQueryable.ToListAsync();
//        }
    }
}
