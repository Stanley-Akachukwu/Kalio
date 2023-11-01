using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Kalio.Entities
{
    public class KalioIdentityDbContext : IdentityDbContext
    {
        public KalioIdentityDbContext(DbContextOptions<KalioIdentityDbContext> options) : base(options) { }
    }
}
