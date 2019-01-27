

namespace KolejkatorApi.Models
{
	public class InfoModel
	{
		public InfoModel(string name, string author, string version)
		{
			ApplicationName = name;
			Author = author;
			Version = version;
		}
		public string ApplicationName { get; set; }
		public string Author { get; set; }
		public string Version { get; set; }
	}
}
