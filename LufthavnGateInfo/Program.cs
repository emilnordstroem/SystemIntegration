
using System.Net;
using System.Text;
using System.Text.Json;
using LufthavnGateInfo;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(options =>
{
	options.Listen(IPAddress.Any, 5268, listenOptions =>
	{
		listenOptions.UseHttps();
	});
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Task.Run(async () =>
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

	Console.WriteLine(" [*] Waiting for messages.");

	var consumer = new AsyncEventingBasicConsumer(channel);
	consumer.ReceivedAsync += (model, ea) =>
	{
		var body = ea.Body.ToArray();
		var message = Encoding.UTF8.GetString(body);

		var gateInfo = JsonSerializer.Deserialize<GateInfo>(message);

		Console.WriteLine($" [x] Received {gateInfo}");
		return Task.CompletedTask;
	};

	await channel.BasicConsumeAsync(
		"hello",
		autoAck: true,
		consumer: consumer);

	Console.WriteLine(" Press [enter] to exit.");
	Console.ReadLine();
});

app.Run();