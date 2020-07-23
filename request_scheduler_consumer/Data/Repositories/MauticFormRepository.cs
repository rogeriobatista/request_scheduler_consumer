using System.Linq;
using System.Threading.Tasks;
using request_scheduler_consumer.Data.Context;
using request_scheduler_consumer.Domain.MauticForms.Entities;
using request_scheduler_consumer.Domain.MauticForms.Interfaces;

namespace request_scheduler_consumer.Data.Repositories
{
    public class MauticFormRepository : IMauticFormRepository
    {
        public MauticForm GetById(long id)
        {
            using (var _context = new RequestSchedulerContext())
                return _context.MauticFormRequests.FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(MauticForm mauticForm)
        {
            using (var _context = new RequestSchedulerContext())
            {
                _context.MauticFormRequests.Update(mauticForm);

                await _context.SaveChangesAsync();
            }
        }

        public void Save(MauticForm mauticForm)
        {
            using (var _context = new RequestSchedulerContext())
            {
                _context.MauticFormRequests.Add(mauticForm);

                _context.SaveChanges();
            }
        }
    }
}
