using System.Threading.Tasks;
using request_scheduler_consumer.Data.Context;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;

namespace request_scheduler_consumer.Data.Repositories
{
    public class MauticFormRepository : IMauticFormRepository
    {
        RequestSchedulerContext _context;

        public MauticFormRepository(RequestSchedulerContext context)
        {
            _context = context;
        }

        public async Task Update(MauticForm mauticForm)
        {
            _context.MauticFormRequests.Update(mauticForm);

            await _context.SaveChangesAsync();
        }
    }
}
