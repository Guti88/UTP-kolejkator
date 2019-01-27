using KolejkatorApi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace KolejkatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueInfoController : ControllerBase
    {
		MySqlConnection connection = new MySqlConnection("server=kolejkomat01.mysql.database.azure.com;user id=gracjankatek@kolejkomat01;password = Kolejkomat01;persistsecurityinfo=True;database=kolejkadb");

		/// <summary>
		/// Metoda zwracająca statusy wszystkich kolejek
		/// GET
		/// https://kolejkomatapi.azurewebsites.net/api/queueinfo
		/// </summary>
		/// <returns>    {"idQueue": "1","status": "Otwarta"},{"idQueue": "2","status": "zamknieta"},{"idQueue": "3","status": "Wstrzymana" }</returns>
		[HttpGet]
		public List<QueueModel> GetQueue()
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("SELECT * FROM QUEUE", connection);
			MySqlDataReader reader = command.ExecuteReader();
			List<QueueModel> kolejka = new List<QueueModel>();
			while (reader.Read())
			{
				string id = reader["idQueue"].ToString();
				string status = reader["Status"].ToString();
				QueueModel queue = new QueueModel(id, status);
				kolejka.Add(queue);
			}
			connection.Close();
			return kolejka;
		}


		/// <summary>
		/// Metoda odpowiedzialna za edycję statusu kolejki
		/// PUT
		/// Link: https://kolejkomatapi.azurewebsites.net/api/queueinfo
		/// </summary>
		/// <param name="value">{"idQueue":"2","Status": "zamknieta"}</param>
		[HttpPut]
		public void PutQueue([FromBody]QueueModel value)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand("UPDATE queue SET queue.Status = '" + value.Status + "' Where queue.idQueue = '" + value.idQueue + "';", connection);
			MySqlDataReader reader = command.ExecuteReader();
			connection.Close();
		}
	}
}
