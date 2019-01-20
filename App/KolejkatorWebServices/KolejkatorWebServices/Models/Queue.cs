using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorWebServices.Models
{
	public class Queue
	{
		public Queue(string id, string status)
		{
			idQueue = id;
			Status = status;
		}
		public string idQueue { get; set; }
		public string Status { get; set; }
	}
}
