using Microsoft.EntityFrameworkCore;

namespace Kalio.Entities
{
     
    public class KalioDbContext : DbContext
    {
        public KalioDbContext(DbContextOptions<KalioDbContext> options) : base(options) { }
    }
}
