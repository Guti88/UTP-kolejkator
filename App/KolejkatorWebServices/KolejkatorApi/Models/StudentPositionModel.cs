using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorApi.Models
{
	public class StudentPositionModel
	{
		public StudentPositionModel(string queue, string position)
		{
			Queue = queue;
			Position = position;
		}
		public string Queue { get; set; }
		public string Position { get; set; }
	}
}
