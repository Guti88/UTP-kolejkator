using System;
using System.Collections.Generic;
using System.Net.Http;
using KolejkatorApi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace KolejkatorApi.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
		MySqlConnection connection = new MySqlConnection("server=kolejkomat01.mysql.database.azure.com;user id=gracjankatek@kolejkomat01;password = Kolejkomat01;persistsecurityinfo=True;database=kolejkadb");
		/// <summary>
		/// Metoda zwracjąca dane studenta
		/// GET
		/// Link: https://kolejkomatapi.azurewebsites.net/api/student/ {id}
		/// </summary>
		/// <param name="id">Numer legitymacji</param>
		/// <returns>     {"name": "Adamczewska","surname": "Anna","indexNumber": "00002","field": "Informatyka Stosowana", "isDaily": true,"isMaster": true}</returns>
		[HttpGet("{id}", Name = "GetStudent")]
		public List<StudentForGuzekModel> GetStudent(string id)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT student.Name, student.Surname, student.IndexNumber, field.Field_Name, field.isDaily,  field.isMaster From student left outer join field_has_student on field_has_student.Student_idStudent = student.IndexNumber inner join field on field.idField = field_has_student.Field_idField  where student.IndexNumber = '" + id + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<StudentForGuzekModel> studentList = new List<StudentForGuzekModel>();
			while (reader.Read())
			{
				string name = reader["Name"].ToString();
				string surname = reader["Surname"].ToString();
				string indexNumber= reader["IndexNumber"].ToString();
				string field = reader["Field_Name"].ToString();
				bool isDaily = true;
				bool isMaster = true;
				if (reader["isDaily"].ToString().Equals("0"))
				{
					isDaily = false;
				}
				if (reader["isMaster"].ToString().Equals("0"))
				{
					isMaster = false;
				}
				StudentForGuzekModel student = new StudentForGuzekModel(name, surname,indexNumber,field,isDaily,isMaster);
				studentList.Add(student);
			}
			connection.Close();
			return studentList;
		}

		/// <summary>
		/// Metoda dodająca studentów do kolejki.
		/// POST
		/// LINK: https://kolejkomatapi.azurewebsites.net/api/student/ {id}
		/// </summary>
		/// <param name="id">Numer kolejki do jakiej dodany ma być student</param>
		/// <param name="value">RFID studenta w formacie JSON : {"rfid": "test2"}</param>
		[HttpPost("{id}")]
		public HttpResponseMessage PostStudent(int id, [FromBody] StudentModel value)
		{
			connection.Open();
			string queue = "00000";
			HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
			string queueStatus = "Otwarta";
			MySqlCommand command = new MySqlCommand("SELECT student.IndexNumber FROM student WHERE student.RFID = '" + value.Rfid + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				queue = reader["IndexNumber"].ToString();
			}
			connection.Close();
			connection.Open();
			command = new MySqlCommand("SELECT queue.Status FROM QUEUE Where queue.idQueue ='" +id +"';", connection);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				queueStatus = reader["Status"].ToString();
			}
			connection.Close();
			if(queueStatus.ToLower().Equals("otwarta"))
			{
				connection.Open();
				command = new MySqlCommand("INSERT INTO queue_has_student (Queue_idQueue, Student_IndexNumber, Data) VALUES ('" + id + "', '" + queue + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "');", connection);
				reader = command.ExecuteReader();
				connection.Close();
				responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
			}
			return responseMessage;
		}
		/// <summary>
		/// Metoda usuwająca studenta z kolejki.
		/// Delete
		/// Link: https://kolejkomatapi.azurewebsites.net/api/student/ {id}
		/// </summary>
		/// <param name="id">Numer kolejki</param>
		/// <param name="value">Numer indeksu studenta w formacie JSON :{"IndexNumber": "00001"}</param>
		[HttpDelete("{id}")]
		public void DeleteStudent(int id, [FromBody] StudentModel value)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("DELETE FROM queue_has_student WHERE (Queue_idQueue = '" + id + "') and (Student_IndexNumber = '" + value.IndexNumber + "');", connection);
			MySqlDataReader reader = command.ExecuteReader();
			connection.Close();
		}
	}
}
