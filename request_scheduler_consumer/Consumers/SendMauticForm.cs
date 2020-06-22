using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;

namespace request_scheduler_consumer.Consumers
{
    public class SendMauticForm : ISendMauticForm
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        private IMauticFormService _mauticFormService;

        public SendMauticForm(IMauticFormService mauticFormService)
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            _mauticFormService = mauticFormService;

        }

        public void Register()
        {
            channel.QueueDeclare(queue: "mautic-form", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var mauticForm = JsonConvert.DeserializeObject<MauticForm>(message);
                _mauticFormService.Send(mauticForm);
            };
            channel.BasicConsume(queue: "mautic-form",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Deregister()
        {
            connection.Close();
        }
    }
}
