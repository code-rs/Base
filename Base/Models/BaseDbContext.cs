using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Base.Models;

namespace Base.Models
{
    public class BaseDbContext : IdentityDbContext<Usuario>
    {

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {

        }

        public DbSet<Base.Models.Rol> Rol { get; set; }

    }
}
