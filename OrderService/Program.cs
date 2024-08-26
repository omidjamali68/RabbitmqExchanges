﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

string queueName = "order.create";

channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, "order.fanout", "");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.ToArray());
    Console.WriteLine("Recieved Message= " + message);
    channel.BasicAck(args.DeliveryTag, true); 
};

channel.BasicConsume(queueName, false, consumer);
Console.ReadLine(); 