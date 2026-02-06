
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace LufthavnGateInfo.Controllers
{
    public class GateInfoController : Controller
    {

        public enum Airline
        {
            SAS,
            KLM,
            NORWEGIAN
        }

        [HttpGet(Name = "gateno")]
        public async Task<bool> Get([FromQuery] Airline airline, [FromQuery] int gateNumber)
        {
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.QueueDeclareAsync(
				queue: "hello",
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null
			);

			string flightNumber = $"{airline.ToString()}{new Random().Next(100, 999)}";

			GateInfo gateInfo = new GateInfo(gateNumber, flightNumber);
			var json = JsonSerializer.Serialize(gateInfo);
			var body = Encoding.UTF8.GetBytes(json);

			await channel.BasicPublishAsync(
				exchange: string.Empty,
				routingKey: "hello",
				body: body
				);
			Console.WriteLine($" [x] Sent {json}");

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();

			return true;
		}

	}
}
