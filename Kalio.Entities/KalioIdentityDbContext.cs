using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalio.Entities
{
    public class KalioIdentityDbContext : IdentityDbContext
    {
        public KalioIdentityDbContext(DbContextOptions<KalioIdentityDbContext> options) : base(options) { }
    }
}
