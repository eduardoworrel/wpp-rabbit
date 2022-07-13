using Microsoft.EntityFrameworkCore;
using System;
using Model;
namespace Infra
{
    public class MyDbContext : DbContext
    {
        public DbSet<UsuarioAdmin>? UsuarioAdmin { get; set; }
        public DbSet<UsuarioNormal>? UsuarioNormal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=wppUsuarios;user=root;password=qwerty",
            new MySqlServerVersion(new Version(8, 0, 27)));
        }
    }
}