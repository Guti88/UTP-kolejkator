using KolejkatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KolejkatorApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InfoController : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		// GET api/values
		[HttpGet]
		public ActionResult<InfoModel> Get()
		{	
			return new InfoModel("KolejkomatApi","inż. Gracjan Kątek","1.5.10.0");
		}
	}
}
