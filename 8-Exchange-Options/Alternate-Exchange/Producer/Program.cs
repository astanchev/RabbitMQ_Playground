using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();


channel.ExchangeDeclare(
    exchange: "altexchange",
    type: ExchangeType.Fanout);

channel.ExchangeDeclare(
    exchange: "mainexchange",
    type: ExchangeType.Direct,
    arguments: new Dictionary<string, object>{
        {"alternate-exchange", "altexchange"}
});

var message = "This is my first Message";

var body = Encoding.UTF8.GetBytes(message);

//channel.BasicPublish("mainexchange", "test", null, body); //goes to main queue
channel.BasicPublish("mainexchange", "test2", null, body); // goes to alt queue because of the wrong key

Console.WriteLine($"Send message: {message}");