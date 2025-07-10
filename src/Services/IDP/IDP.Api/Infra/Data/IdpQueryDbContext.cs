using IDP.Api.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace IDP.Api.Infra.Data
{
    public class IdpQueryDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public IdpQueryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("QueryDBConnection"));
        }
        public DbSet<User> Tbl_Users { get; set; }
    }
}
