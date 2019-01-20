using Microsoft.EntityFrameworkCore;

namespace KolejkatorWebServices.Models
{
	public class KolejkomatContext : DbContext
	{
		public KolejkomatContext(DbContextOptions<KolejkomatContext> options)
						: base(options)
		{
		}
		public DbSet<Queue> QueueItems { get; set; }
	}
}
