using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KolejkatorApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace KolejkatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
		MySqlConnection connection = new MySqlConnection("server=kolejkomatdb.mysql.database.azure.com;user id=KolejkomatUser@kolejkomatdb;password = utpSerVer5;persistsecurityinfo=True;database=kolejkadb");

		/// <summary>
		/// Metoda odpowiedzialna za zwracanie listy studentów w podanej kolejce
		/// GET
		/// Link: http://utpkolejka.azurewebsites.net/api/queue/ {id}
		/// </summary>
		/// <param name="id">Numer kolejki</param>
		/// <returns>{"00001","00002"}</returns>
		[HttpGet("{id}")]
		public List<string> GetQueue(int id)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT queue_has_student.Student_IndexNumber FROM queue_has_student where queue_has_student.Queue_idQueue = '" + id + "'order by queue_has_student.Data;", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<string> kolejka = new List<string>();
			while(reader.Read())
			{
				string status = reader["Student_IndexNumber"].ToString();
				kolejka.Add(status);
			}
			connection.Close();
			return kolejka;
		}
		/// <summary>
		/// Metoda odpowiedzialna za zwracanie pozycji studenta na liście 
		/// GET
		/// Link:http://utpkolejka.azurewebsites.net/api/queue/
		/// </summary>
		/// <param name="student">Numer indeksu studenta w formacie JSON : {"IndexNumber": "00001"}</param>
		/// <returns>  {"queue": "1","position": "1" }</returns>
		[HttpGet]
		public List<StudentPositionModel> GetQueue([FromBody] StudentModel student)
		{

			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT queue_has_student.Queue_idQueue FROM queue_has_student WHERE queue_has_student.Student_IndexNumber ='" + student.IndexNumber + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<string> queueList = new List<string>();
			List<string> indexNumberList = new List<string>();
			List<StudentPositionModel> studentList = new List<StudentPositionModel>();
			while (reader.Read())
			{
				string queue1 = reader["Queue_idQueue"].ToString();
				queueList.Add(queue1);
			}
			connection.Close();
			foreach(string queue in queueList)
			{
				connection.Open();
				command = new MySqlCommand("SELECT queue_has_student.Student_IndexNumber FROM queue_has_student WHERE queue_has_student.Queue_idQueue ='" + queue + "' order by queue_has_student.Data", connection);
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					string indexNumber = reader["Student_IndexNumber"].ToString();
					indexNumberList.Add(indexNumber);
				}
				studentList.Add(new StudentPositionModel(queue, (indexNumberList.IndexOf(student.IndexNumber)+1).ToString()));
				connection.Close();
				indexNumberList.Clear();
			}
			return studentList;
		}

	}
}
