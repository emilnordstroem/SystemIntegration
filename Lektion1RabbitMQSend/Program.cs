using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

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

/*
const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);
*/

// Lektion 2 - Opgave 1
var person = new Person("Emil", "Stoeve", 24, "emil@example.com");
var json = JsonSerializer.Serialize(person);
var body = Encoding.UTF8.GetBytes(json);


await channel.BasicPublishAsync(
	exchange: string.Empty,
	routingKey: "hello",
	body: body
	);
Console.WriteLine($" [x] Sent {json}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();