using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorApi.Models
{
	public class QueueModel
	{
		public QueueModel(string id, string status)
		{
			idQueue = id;
			Status = status;
		}
		public string idQueue { get; set; }
		public string Status { get; set; }
	}
}
