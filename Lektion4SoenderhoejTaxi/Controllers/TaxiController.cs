using Microsoft.AspNetCore.Mvc;
using Lektion4SoenderhoejTaxi.Models;

namespace Lektion4SoenderhoejTaxi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TaxiController : Controller
	{
		[HttpPost]
		public string Index(Bestilling bestilling)
		{
			Publisher.PublishBestilling(bestilling);
			return $"Bestilling modtaget: {bestilling.Print()}";
		}
	}
}