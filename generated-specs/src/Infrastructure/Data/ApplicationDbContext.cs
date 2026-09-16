using Microsoft.EntityFrameworkCore;
using transactionalsystemdotnetangular.Domain.Entities;
using transactionalsystemdotnetangular.Infrastructure.Outbox;

namespace transactionalsystemdotnetangular.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EjecucindeSagayManejodeComandosCQRSenNET8conAngularAggregate> EjecucindeSagayManejodeComandosCQRSenNET8conAngulars { get; set; } = null!;
        public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<EjecucindeSagayManejodeComandosCQRSenNET8conAngularAggregate>(entity =>
            {
                entity.HasKey(e => e.Id);
                // Additional configurations can be added here
            });
            
            modelBuilder.Entity<OutboxMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
