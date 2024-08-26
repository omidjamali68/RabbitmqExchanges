using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare("order.create", true, false, false, null);
string message = $"Send shopping card basket to order service {DateTime.UtcNow}";
var body = Encoding.UTF8.GetBytes(message);

var properties = channel.CreateBasicProperties();
properties.Persistent = true;
// exchange:"" => Direct exchange
channel.BasicPublish(exchange:"", routingKey:"order.create", basicProperties: properties, body: body);