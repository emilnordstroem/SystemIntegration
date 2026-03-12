using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

public class Producer
{
	static async Task Main(string[] args)
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.ExchangeDeclareAsync(
			exchange: "logs",
			type: ExchangeType.Topic
		);

		Console.WriteLine("OrderService ready. Press ENTER to place an order.");
		Console.ReadLine();

		var order = new { OrderId = 1, Product = "Lenovo Thinkpad", Quantity = 1 };
		var message = JsonSerializer.Serialize(order);
		var body = Encoding.UTF8.GetBytes(message);

		await channel.BasicPublishAsync(
			exchange: "logs",
			routingKey: "order_placed",
			body: body
		);
		Console.WriteLine($"Published: {message}");

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
	}
}
