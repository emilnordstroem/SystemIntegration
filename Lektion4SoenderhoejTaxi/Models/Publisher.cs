using System.Text;
using RabbitMQ.Client;

namespace Lektion4SoenderhoejTaxi.Models
{
	public class Publisher
	{
		public async void PublishBestilling(Bestilling bestilling)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.ExchangeDeclareAsync(
				exchange: "logs", // exhange to use
				type: ExchangeType.Fanout // type of exchange
			);

			var message = bestilling.ToString();
			var body = Encoding.UTF8.GetBytes(message);

			await channel.BasicPublishAsync(
				exchange: "logs",
				routingKey: string.Empty, // default because its ignored using Fanout
				body: body
			);
			Console.WriteLine($" [x] Sent {message.ToString()}");

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
	}
}
