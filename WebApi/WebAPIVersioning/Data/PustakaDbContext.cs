﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIVersioning.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebAPIVersioning.Data
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
