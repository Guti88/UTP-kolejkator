using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorApi.Models
{
	public class StudentForGuzekModel
	{
		public StudentForGuzekModel(string name, string surname, string indexNumber, string field, bool isDaily, bool isMaster)
		{
			Name = name;
			Surname = surname;
			IndexNumber = indexNumber;
			Field = field;
			IsDaily = isDaily;
			IsMaster = isMaster;
		}
		public string Name { get; set; }
		public string Surname { get; set; }
		public string IndexNumber { get; set; }
		public string Field { get; set; }
		public bool IsDaily { get; set; }
		public bool IsMaster { get; set; }
	}
}
