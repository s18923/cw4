using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudents(string id)
        {
            //var list = new List<Student>();
            var list = new List<SemesterData>();

            id = "1234";

            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18923;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                //com.CommandText = "select * from Student where indexNumber = @id";
                com.CommandText = @"select s.IndexNumber, s.FirstName, s.LastName, st.Name, e.Semester, e.StartDate 
                        from Student s join Enrollment e on s.IdEnrollment = e.IdEnrollment join Studies st on e.IdStudy = st.IdStudy
                        where s.IndexNumber = @id ";
                //var idParametr = new SqlParameter("id", SqlDbType.VarChar);
                //idParametr.Value = id;
                //com.Parameters.Add(idParametr);
                com.Parameters.AddWithValue("id", id);

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    //var st = new Student();

                    var sd = new SemesterData();
                    sd.IndexNumber = dr["IndexNumber"].ToString();
                    sd.FirstName = dr["FirstName"].ToString();
                    sd.LastName = dr["LastName"].ToString();
                    sd.Name = dr["Name"].ToString();
                    sd.StartDate =(DateTime) dr["StartDate"];
                    sd.Semester = (int)dr["Semester"];                    
                    list.Add(sd);
                }
            }
            
            
            return Ok(list);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetStudent(int id)
        //{
        //    if (id == 1)
        //    {
        //        return Ok("Kowalski");
        //    }
        //    else if (id == 2)
        //    {
        //        return Ok("Malinowski");
        //    }
        //    return NotFound("Nie znaleziono studenta");
        //}

        [HttpPost]

        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Aktualizacja ukończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}