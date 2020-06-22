using System;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;
using request_scheduler_consumer.Domain.MauticForms.Enums;
using request_scheduler_consumer.Generics.Http;
using request_scheduler_consumer.Generics.Http.Enums;

namespace request_scheduler_consumer.Domain.MauticForms.Services
{
    public class MauticFormService : IMauticFormService
    {
        private readonly IMauticFormRepository _mauticFormRepository;

        public MauticFormService(IMauticFormRepository mauticFormRepository)
        {
            _mauticFormRepository = mauticFormRepository;
        }

        public void Send(MauticForm mauticForm)
        {
            var client = new Client(mauticForm.DestinyAddress, mauticForm.ContentType, mauticForm.Headers, mauticForm.Body);
            try
            {
                if (mauticForm.HttpMethod == HttpMethod.Post)
                {
                    client.Post();
                }
                else
                {
                    client.Get();
                }
            }
            catch
            {
                mauticForm.UpdateStatus(MauticFormStatus.Failed);
                mauticForm.SetUpdatedAt();

                _mauticFormRepository.Update(mauticForm);
            }
        }
    }
}
