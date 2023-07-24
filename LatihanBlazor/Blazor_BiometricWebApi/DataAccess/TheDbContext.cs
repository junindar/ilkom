using Blazor_BiometricWebApi.DataAccess.Models;
using Fido2NetLib.Development;
using Microsoft.EntityFrameworkCore;


namespace Blazor_BiometricWebApi.DataAccess
{
    public class TheDbContext : DbContext
    {
        public TheDbContext(DbContextOptions<TheDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
