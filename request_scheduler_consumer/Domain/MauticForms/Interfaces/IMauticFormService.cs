using request_scheduler_consumer.Domain.MauticForms.Entities;

namespace request_scheduler_consumer.Domain.MauticForms.Interfaces
{
    public interface IMauticFormService
    {
        void Send(MauticForm mauticForm);
    }
}
