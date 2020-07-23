using System;
using request_scheduler_consumer.Domain.MauticForms.Enums;
using request_scheduler_consumer.Generics.Http.Enums;

namespace request_scheduler_consumer.Domain.MauticForms.Entities
{
    public class MauticForm
    {
        public long Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public string DestinyAddress { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public string ContentType { get; private set; }

        public string Headers { get; private set; }

        public string Body { get; private set; }

        public MauticFormStatus Status { get; private set; }

        public string CronId { get; private set; }

        protected MauticForm() { }

        public MauticForm(string destinyAddress, HttpMethod httpMethod, string contentType, string headers, string body, string cronId)
        {
            CreatedAt = DateTime.Now;
            Status = MauticFormStatus.Pending;
            DestinyAddress = destinyAddress;
            HttpMethod = httpMethod;
            ContentType = contentType;
            Headers = headers;
            Body = body;
            CronId = cronId;
        }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }

        public void UpdateDestinyAddress(string destinyAddress)
        {
            DestinyAddress = destinyAddress;
        }

        public void UpdateHttpMethod(HttpMethod httpMethod)
        {
            HttpMethod = httpMethod;
        }

        public void UpdateContentType(string contentType)
        {
            ContentType = contentType;
        }

        public void UpdateHeaders(string headers)
        {
            Headers = headers;
        }

        public void UpdateBody(string body)
        {
            Body = body;
        }

        public void UpdateStatus(MauticFormStatus status)
        {
            Status = status;
        }

        public void UpdateCronId(string cronId)
        {
            CronId = cronId;
        }
    }
}
