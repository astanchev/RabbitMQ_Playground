using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(
            queue: "request-queue",
            exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    Console.WriteLine($"Received Request: {ea.BasicProperties.CorrelationId}");

    var replayMessage = $"This is your replay {ea.BasicProperties.CorrelationId}";

    var body = Encoding.UTF8.GetBytes(replayMessage);

    channel.BasicPublish(
        exchange: "",
        routingKey: ea.BasicProperties.ReplyTo,
        basicProperties: null,
        body: body
    );
};

channel.BasicConsume(
    queue: "request-queue",
    autoAck: true,
    consumer: consumer);

Console.ReadKey();