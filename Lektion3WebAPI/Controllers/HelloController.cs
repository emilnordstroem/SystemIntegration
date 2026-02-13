using Lektion3WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lektion3WebAPI.Controllers
{

	[ApiController]
    [Route("[controller]")]
	public class HelloController : ControllerBase
    {
		private readonly IGreetingService _greetingService;

		public HelloController(IGreetingService greetingService)
		{
			_greetingService = greetingService;
		}

        [HttpGet]
        public string Get()
        {
            return "Hello from my first controller";
        }

		[HttpGet("{name}")]
		public string Get(string name)
		{
			return _greetingService.CreateCreeting(name);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Person person)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok($"Received Person: {person.ToString()}");
		}
	}
}
