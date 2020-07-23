using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using request_scheduler_consumer.Domain.MauticForms.Dtos;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;

namespace request_scheduler_consumer.Consumers
{
    public class SaveMauticForm : ISaveMauticForm
    {
        private ConnectionFactory _factory { get; set; }
        private IConnection _connection { get; set; }
        private IModel _channel { get; set; }

        private IMauticFormService _mauticFormService;

        public SaveMauticForm(IMauticFormService mauticFormService)
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _mauticFormService = mauticFormService;

        }

        public void Register()
        {
            _channel.QueueDeclare(queue: "mautic-form-to-save", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var mauticForm = JsonConvert.DeserializeObject<MauticFormRequestDto>(message);
                _mauticFormService.Save(mauticForm);
            };
            _channel.BasicConsume(queue: "mautic-form-to-save",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Deregister()
        {
            _connection.Close();
        }
    }
}
