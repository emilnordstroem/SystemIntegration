using System.Diagnostics;
using Lektion12HttpClientPokemon.Models;
using Lektion12HttpClientPokemon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lektion12HttpClientPokemon.Controllers
{
	public class HomeController : Controller
	{
		private readonly PokemonService _service;
		public HomeController(PokemonService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.Pokemon = await _service.GetPokemonSpecies();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
