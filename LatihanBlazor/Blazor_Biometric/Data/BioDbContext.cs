using System.Collections.Generic;
using Blazor_Biometric.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Biometric.Data
{
    public class BioDbContext : DbContext
    {
        public BioDbContext(DbContextOptions<BioDbContext> options) : base(options)
        {

        }
       
        public DbSet<User> Users { get; set; }

    }
}
