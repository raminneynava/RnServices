using IDP.Api.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace IDP.Api.Infra.Data
{
    public class IdpCommandDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public IdpCommandDbContext(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("CommandDBConnection"));
        }
        public DbSet<User> Tbl_Users { get; set; }
        public DbSet<Outbox> Tbl_Outbox { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
