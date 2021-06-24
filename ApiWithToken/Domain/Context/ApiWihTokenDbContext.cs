using System;
using ApiWithToken.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiWithToken.Domain.Context
{
    public partial class ApiWihTokenDbContext : DbContext
    {
        public ApiWihTokenDbContext()
        {
        }

        public ApiWihTokenDbContext(DbContextOptions<ApiWihTokenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
