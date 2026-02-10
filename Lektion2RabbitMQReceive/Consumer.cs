using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Consumer
{
    static async Task Main(string[] args)
    {
		var factory = new ConnectionFactory { HostName = "localhost" };
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(
			queue: "task_queue", // task_queue / gateInfo
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);
		/* 
			Using message acknowledgments and BasicQosAsync you can set up a work queue. 
			The durability options let the tasks survive even if RabbitMQ is restarted. 
		*/
		await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

		Console.WriteLine(" [*] Waiting for messages.");

		var consumer = new AsyncEventingBasicConsumer(channel);
		consumer.ReceivedAsync += async (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			Console.WriteLine($" [x] Received {message}");

			int dots = message.Split('.').Length - 1;
			await Task.Delay(dots * 1000);

			Console.WriteLine(" [x] Done");

			// here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
			await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
		};

		await channel.BasicConsumeAsync(
			"task_queue", // task_queue / gateInfo
			autoAck: false,
			consumer: consumer);

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
	}
}