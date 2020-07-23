using System;
using System.Collections.Generic;
using request_scheduler_consumer.Domain.MauticForms.Enums;
using request_scheduler_consumer.Generics.Http.Enums;

namespace request_scheduler_consumer.Domain.MauticForms.Dtos
{
    public class MauticFormRequestDto
    {
        public long Id { get; set; }

        public string DestinyAddress { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public string ContentType { get; set; }

        public List<MauticFormHeaderDto> Headers { get; set; }

        public string Body { get; set; }

        public MauticFormStatus? Status { get; set; }

        public string CronId { get; set; }
    }
}
