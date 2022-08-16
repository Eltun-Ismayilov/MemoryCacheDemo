using MemoryCacheDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoConteroller : ControllerBase
    {
        private readonly TestDataContext db;
        private readonly IMemoryCache memoryCache;

        public DemoConteroller(TestDataContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            this.memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Demo demo)
        {
            db.Demos.Add(demo);
            await db.SaveChangesAsync();
            return Ok(demo.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimsVm = await memoryCache.GetOrCreateAsync(12, async (x) =>
            {
                var data = await db.Demos.ToListAsync();
                return Ok(data);
            });

            return Ok(claimsVm);
        }
    }
}
