using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User_service.Facades;
using User_service.Transformers;
using UserService.Models;

namespace UserService.BackgroundServices
{
    public class MessageHandler : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string queueName;
        private IServiceScopeFactory _scopeFactory;

        public MessageHandler(IServiceScopeFactory scopeFactory)
        {
            InitRabbitMQ();
            _scopeFactory = scopeFactory;
        }
        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "stable-rabbitmq.mg.svc", UserName = "user", Password = "PrI1cTao1G" };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            queueName = _channel.QueueDeclare().QueueName;
            _channel.ExchangeDeclare("user.exchange", ExchangeType.Fanout);
            _channel.QueueBind(queueName, "user.exchange", "");
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                UserCreationModel user = (UserCreationModel)JsonConvert.DeserializeObject(message, typeof(UserCreationModel));

                // handle the received message  
                HandleMessage(user);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queueName, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(UserCreationModel user)
        {
            using var scope = _scopeFactory.CreateScope();
            var facade = scope.ServiceProvider.GetRequiredService<UserFacade>();

            Debug.WriteLine(user.Email);
            facade.CreateUser(UserMapper.MapBusToDb(user));
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
