﻿using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;

namespace request_scheduler_consumer.Consumers
{
    public class SendMauticForm : ISendMauticForm
    {
        private ConnectionFactory _factory { get; set; }
        private IConnection _connection { get; set; }
        private IModel _channel { get; set; }

        private IMauticFormService _mauticFormService;

        public SendMauticForm(IMauticFormService mauticFormService)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _mauticFormService = mauticFormService;

        }

        public void Register()
        {
            _channel.QueueDeclare(queue: "mautic-form", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var mauticForm = JsonConvert.DeserializeObject<MauticForm>(message);
                _mauticFormService.Send(mauticForm);
            };
            _channel.BasicConsume(queue: "mautic-form",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Deregister()
        {
            _connection.Close();
        }
    }
}
