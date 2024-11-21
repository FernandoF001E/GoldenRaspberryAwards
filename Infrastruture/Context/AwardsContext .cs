using Entity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Context
{
    public class AwardsContext : DbContext
    {
        public DbSet<Award> Awards { get; set; }

        public AwardsContext(DbContextOptions<AwardsContext> options) : base(options)
        {
            Awards = Set<Award>();
        }
    }
}
