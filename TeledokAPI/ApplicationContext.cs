using Microsoft.EntityFrameworkCore;
using System;
using TeledokAPI.Models;

namespace TeledokAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи "один ко многим"
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Founders)
                .WithOne(f => f.Client)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // При удалении клиента удаляются его учредители
        }
    }
}
