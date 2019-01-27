using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolejkatorApi.Models
{
	public class StudentModel
	{
		public StudentModel(string name, string surname)
		{
			Name = name;
			Surname = surname;
		}
		public int idStudent { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string IndexNumber { get; set; }
		public string Rfid { get; set; }
		public string Date { get; set; }
	}
}
