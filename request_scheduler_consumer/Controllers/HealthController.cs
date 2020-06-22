using Microsoft.AspNetCore.Mvc;

namespace request_scheduler_consumer.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return Ok(new { health = true });
        }
    }
}
