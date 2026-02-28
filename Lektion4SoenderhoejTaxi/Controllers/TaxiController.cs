using Microsoft.AspNetCore.Mvc;
using Lektion4SoenderhoejTaxi.Models;

namespace Lektion4SoenderhoejTaxi.Controllers
{ 
	[ApiController]
	[Route("[controller]")]
	public class TaxiController : Controller
	{
		private Publisher publisher = new Publisher();
		private int _id = 1;
		private List<Bestilling> _bestillinger = new List<Bestilling>();

		[HttpPost]
		public string Index(Bestilling bestilling)
		{
			bestilling.Id = _id++;
			_bestillinger.Add(bestilling);
			publisher.PublishBestilling(bestilling);
			return $"Bestilling modtaget: {bestilling.ToString()}";
		}
	}
}