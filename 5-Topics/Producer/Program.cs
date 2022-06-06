using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();


channel.ExchangeDeclare(
    exchange: "mytopicexchange",
    ExchangeType.Topic
);

var userPaymentsMessage = "An european user paid for something!";

var userPaymentsBody = Encoding.UTF8.GetBytes(userPaymentsMessage);

channel.BasicPublish(
    exchange: "mytopicexchange",
    routingKey: "user.europe.payments",
    basicProperties: null,
    body: userPaymentsBody
);

Console.WriteLine($"Published message: {userPaymentsMessage}");


var businessOrderMessage = "An european business ordered goods!";

var businessOrderBody = Encoding.UTF8.GetBytes(businessOrderMessage);

channel.BasicPublish(
    exchange: "mytopicexchange",
    routingKey: "business.europe.orders",
    basicProperties: null,
    body: businessOrderBody
);

Console.WriteLine($"Published message: {businessOrderMessage}");