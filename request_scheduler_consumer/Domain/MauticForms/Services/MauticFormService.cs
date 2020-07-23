using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;
using request_scheduler_consumer.Domain.MauticForms.Enums;
using request_scheduler_consumer.Generics.Http;
using request_scheduler_consumer.Generics.Http.Enums;
using request_scheduler_consumer.Domain.MauticForms.Dtos;
using Newtonsoft.Json;

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
                mauticForm.UpdateStatus(MauticFormStatus.Sent);
                mauticForm.SetUpdatedAt();
                _mauticFormRepository.Update(mauticForm);
            }
            catch
            {
                mauticForm.UpdateStatus(MauticFormStatus.Failed);
                mauticForm.SetUpdatedAt();

                _mauticFormRepository.Update(mauticForm);
            }
        }

        public void Save(MauticFormRequestDto dto)
        {
            if (dto.Id == 0)
            {
                string headers = JsonConvert.SerializeObject(dto.Headers);
                var formMautic = new MauticForm(dto.DestinyAddress, dto.HttpMethod, dto.ContentType, headers, dto.Body, dto.CronId);
                _mauticFormRepository.Save(formMautic);
            }
            else
            {
                var mauticForm = CreateMauticFormToUpdate(dto);

                _mauticFormRepository.Update(mauticForm);
            }
        }

        private MauticForm CreateMauticFormToUpdate(MauticFormRequestDto dto)
        {
            var mauticForm = _mauticFormRepository.GetById(dto.Id);

            mauticForm.UpdateDestinyAddress(dto.DestinyAddress);
            mauticForm.UpdateHttpMethod(dto.HttpMethod);
            mauticForm.UpdateContentType(dto.ContentType);
            string headers = JsonConvert.SerializeObject(dto.Headers);
            mauticForm.UpdateHeaders(headers);
            mauticForm.UpdateBody(dto.Body);
            mauticForm.UpdateStatus(dto.Status.Value);
            mauticForm.UpdateCronId(dto.CronId);
            mauticForm.SetUpdatedAt();

            return mauticForm;
        }
    }
}
