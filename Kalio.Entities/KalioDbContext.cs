using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalio.Entities
{
     
    public class KalioDbContext : DbContext
    {
        public KalioDbContext(DbContextOptions<KalioDbContext> options) : base(options) { }
    }
}
