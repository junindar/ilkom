using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilteringWebApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilteringWebApi.Data
{
    public class PustakaDbContext : DbContext
    {
        public PustakaDbContext(DbContextOptions<PustakaDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
    
    }
}
