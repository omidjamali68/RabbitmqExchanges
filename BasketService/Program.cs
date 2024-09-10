using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.ExchangeDeclare("order.topic", ExchangeType.Topic, true);

var body = Encoding.UTF8.GetBytes(
    $"Send shopping card basket to order service {DateTime.UtcNow}");

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(
    exchange: "order.topic", routingKey:"order.cancel", basicProperties: properties, body: body);