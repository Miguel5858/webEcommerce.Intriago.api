using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using WebApiPerson.Dtos;
using WebApiPerson.MQ;

namespace WebApiPerson.Services.MQ
{
    public class RabbitMQListenerService : BackgroundService
    {
        private readonly ILogger<RabbitMQListenerService> _logger;
        private readonly RabbitMQSettings _rabbitMQSettings;
        private IConnection _connection;
        private IModel _channel;
        private readonly string NombreCola = "pagoDeposito";

        public RabbitMQListenerService(ILogger<RabbitMQListenerService> logger, IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _logger = logger;
            _rabbitMQSettings = rabbitMQSettings.Value;

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.Hostname,
                Port = _rabbitMQSettings.Port,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password,
                VirtualHost = _rabbitMQSettings.VirtualHost
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: NombreCola,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //var cuentas = JsonSerializer.Deserialize<List<PagoDto>>(message);
                var cuentas = JsonSerializer.Deserialize<PagoDto>(message);

                _logger.LogInformation("Mensaje recibido: {Message}", message);

            };

            _channel.BasicConsume(queue: NombreCola,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

    }
}
