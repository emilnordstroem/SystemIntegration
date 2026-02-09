using System.Text;
using Microsoft.AspNetCore.Mvc;
using Models;
using RabbitMQ.Client;

namespace Lektion2GateInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

	[ApiController]
	[Route("[controller]")]
	public class GateInfoController : ControllerBase
	{

		private ILogger<GateInfoController> _logger;

		public GateInfoController(ILogger<GateInfoController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "gateno")]
        public async Task<bool> Get([FromHeader] Airline airline, [FromHeader] int gateNumber)
        {
			_logger.LogInformation($"Received Request\nAirline: {airline} | Gate number: {gateNumber}");

            GateInfo gateInfo = new GateInfo
            {
                GateNumber = gateNumber,
                FlightNumber = airline.ToString().Substring(0, 2) + new Random().Next(1000, 9999)
            };

			// RabbitMQ Sender
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.QueueDeclareAsync(
				queue: "gateInfo",
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null
			);

			var message = $"GateNumber: {gateInfo.GateNumber}, FlightNumber: {gateInfo.FlightNumber}";
			var body = Encoding.UTF8.GetBytes(message);

			
			await channel.BasicPublishAsync(
				exchange: string.Empty,
				routingKey: "gateInfo",
				mandatory: true,
				body: body
			);

			_logger.LogInformation($" [x] Sent {message}");

			return true;
		}

        public enum Airline
		{
			SAS,
			KLM,
			NORWEGIAN
		}


	}

}
