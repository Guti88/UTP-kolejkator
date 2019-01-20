using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorWebServices.Models
{
	public class QueueHasStudent
	{
		public QueueHasStudent(int queue, int position)
		{
			Queue = queue;
			Position = position;
		}
		public int Queue { get; set; }
		public int Position { get; set; }
	}
}
