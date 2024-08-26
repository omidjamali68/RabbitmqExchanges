using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.ExchangeDeclare("order.fanout", ExchangeType.Fanout, true);

var body = Encoding.UTF8.GetBytes(
    $"Send shopping card basket to order service {DateTime.UtcNow}");

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(
    exchange: "order.fanout", routingKey:"", basicProperties: properties, body: body);