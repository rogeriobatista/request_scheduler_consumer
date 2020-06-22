﻿using System.Threading.Tasks;
using request_scheduler_consumer.Domain.MauticForms.Entities;

namespace request_scheduler_consumer.Domain.MauticForms.Interfaces
{
    public interface IMauticFormRepository
    {
        Task Update(MauticForm model);

    }
}
