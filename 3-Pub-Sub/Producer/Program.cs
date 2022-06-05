using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "pubsub",
    type: ExchangeType.Fanout
);

var message = "Hello I want to broadcast this message!";

var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("pubsub", "", null, encodedMessage);

Console.WriteLine($"Published message: {message}");