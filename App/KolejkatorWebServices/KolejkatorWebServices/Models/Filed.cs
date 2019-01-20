using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorWebServices.Models
{
	public class Filed
	{
		public int idField { get; set; }
		public string Name { get; set; }
		public int IsDaily { get; set; }
		public int IsMaster { get; set; }
	}
}
