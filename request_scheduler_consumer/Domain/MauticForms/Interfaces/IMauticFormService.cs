using request_scheduler_consumer.Domain.MauticForms.Dtos;
using request_scheduler_consumer.Domain.MauticForms.Entities;

namespace request_scheduler_consumer.Domain.MauticForms.Interfaces
{
    public interface IMauticFormService
    {
        void Save(MauticFormRequestDto dto);

        void Send(MauticForm mauticForm);
    }
}
