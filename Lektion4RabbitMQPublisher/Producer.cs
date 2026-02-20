using System.Text;
using RabbitMQ.Client;

public class Producer
{
	static async Task Main(string[] args)
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.ExchangeDeclareAsync(
			exchange: "logs", // exhange to use
			type: ExchangeType.Fanout // type of exchange
		);

		var message = GetMessage(args);
		var body = Encoding.UTF8.GetBytes(message);

		await channel.BasicPublishAsync(
			exchange: "logs", 
			routingKey: string.Empty, // default because its ignored using Fanout
			body: body
		);
		Console.WriteLine($" [x] Sent {message}");

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();

		static string GetMessage(string[] args)
		{
			return ((args.Length > 0) ? string.Join(" ", args) : "info: Hello World!");
		}
	}
}
