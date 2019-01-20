using System;
using System.Collections.Generic;
using KolejkatorWebServices.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace KolejkatorWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
		MySqlConnection connection = new MySqlConnection("server=kolejkomat01.mysql.database.azure.com;user id=gracjankatek@kolejkomat01;password = Kolejkomat01;persistsecurityinfo=True;database=kolejkadb");
		// GET: api/Student/5
		[HttpGet("{id}", Name = "GetStudent")]
        public List<Student> GetStudent(string id)
        {
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT student.Name ,student.Surname FROM student WHERE student.IndexNumber = '"+id+"';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<Student> queueList = new List<Student>();
			while (reader.Read())
			{
				string name = reader["Name"].ToString();
				string surname = reader["Surname"].ToString();
				Student student = new Student(name,surname);
				queueList.Add(student);
			}
			connection.Close();
			return queueList;
        }

		// POST: api/Student
		[HttpPost("{id}")]
		public void PostStudent(int id,[FromBody] Student value)
        {
			connection.Open();
			string queue = "104816";
			MySqlCommand command = new MySqlCommand("SELECT student.IndexNumber FROM student WHERE student.RFID = '" + value.Rfid + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				 queue = reader["IndexNumber"].ToString();
			}
			connection.Close();
			connection.Open();
			command = new MySqlCommand("INSERT INTO queue_has_student (Queue_idQueue, Student_IndexNumber, Data) VALUES ('" + id + "', '" + queue +"', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") +"');", connection);
			reader = command.ExecuteReader();
			connection.Close();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
        public void DeleteStudent(int id, [FromBody] Student value)
        {
			connection.Open();
			MySqlCommand command = new MySqlCommand("DELETE FROM queue_has_student WHERE (Queue_idQueue = '" + id + "') and (Student_IndexNumber = '" + value.IndexNumber + "');", connection);
			MySqlDataReader reader = command.ExecuteReader();
			connection.Close();
		}
	}
}
