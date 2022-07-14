using Microsoft.EntityFrameworkCore;
using UnReaL.Models;

namespace UnReaL.Database
{
    public class UnReaLContext : DbContext
    {
        public UnReaLContext(DbContextOptions options) : base(options) { }

        public DbSet<ShortURL> ShortURLs { get; set; }
    }
}
