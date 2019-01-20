using KolejkatorWebServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace KolejkatorWebServices.Controllers
{
	[Route("api/queue")]
	[ApiController]
	public class QueueController : ControllerBase
	{
		MySqlConnection connection = new MySqlConnection("server=kolejkomat01.mysql.database.azure.com;user id=gracjankatek@kolejkomat01;password = Kolejkomat01;persistsecurityinfo=True;database=kolejkadb");
		// GET: api/Queue
		[HttpGet]
		public List<Queue> GetQueue()
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT * FROM QUEUE", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<Queue> kolejka = new List<Queue>();
			while(reader.Read())
			{
				string id = reader["idQueue"].ToString();
				string status = reader["Status"].ToString();
				Queue queue = new Queue(id, status);
				kolejka.Add(queue);
			}
			connection.Close();
				return kolejka;
		}

		[HttpGet("{id}", Name = "GetQueue")]
		public List<string> GetQueue(string id)
		{

			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT queue_has_student.Queue_idQueue FROM queue_has_student WHERE queue_has_student.Student_IndexNumber ='" + id + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<string> queueList = new List<string>();
			while (reader.Read())
			{
				string queue1 = reader["Queue_idQueue"].ToString();
				queueList.Add(queue1);
			}
			connection.Close();
			return queueList;
		}

		// PUT: api/Queue
		[HttpPut("{value}")]
		public void PutQueue([FromBody]Queue value)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("UPDATE queue SET queue.Status = '" + value.Status+ "' Where queue.idQueue = '" + value.idQueue+";", connection);
			MySqlDataReader reader = command.ExecuteReader();
			connection.Close();
		}
	}
}
