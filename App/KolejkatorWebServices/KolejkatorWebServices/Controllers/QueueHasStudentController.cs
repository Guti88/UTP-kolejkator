using System.Collections.Generic;
using KolejkatorWebServices.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace KolejkatorWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueHasStudentController : ControllerBase
    {
		MySqlConnection connection = new MySqlConnection("server=kolejkomat01.mysql.database.azure.com;user id=gracjankatek@kolejkomat01;password = Kolejkomat01;persistsecurityinfo=True;database=kolejkadb");
		// GET: api/QueueHasStudent
		[HttpGet]
        public List<string> Get()
        {
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT queue_has_student.Queue_idQueue, COUNT(queue_has_student.Queue_idQueue) AS Queue FROM queue_has_student group by  queue_has_student.Queue_idQueue", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<string> queueList = new List<string>();
			while (reader.Read())
			{
				string queue1 = reader["Queue_idQueue"].ToString();
				string queue2 = reader["Queue"].ToString();
				queueList.Add(queue1);
				queueList.Add(queue2);
			}
			connection.Close();
			return queueList;
		}

        // GET: api/QueueHasStudent/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id, [FromBody] Student value)
        {
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT queue_has_student.Student_IndexNumber FROM queue_has_student WHERE queue_has_student.Queue_idQueue ='" + id + "' order by queue_has_student.Data", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<string> queueList = new List<string>();
			while (reader.Read())
			{
				string queue1 = reader["Student_IndexNumber"].ToString();
				queueList.Add(queue1);
			}
			connection.Close();
			return queueList.IndexOf(value.IndexNumber).ToString();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
