using Microsoft.EntityFrameworkCore;
using transactionalsystemdotnetangular.Domain.Entities;
using transactionalsystemdotnetangular.Infrastructure.Outbox;

namespace transactionalsystemdotnetangular.Infrastructure.Data
{
    public interface ITenantService
    {
        string GetCurrentTenantId();
    }

    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options) 
        {
            _tenantService = tenantService;
        }

        public DbSet<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular> EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngulars { get; set; } = null!;
        public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ReferenceCode).IsUnique();
                entity.HasQueryFilter(e => EF.Property<string>(e, "TenantId") == _tenantService.GetCurrentTenantId());
            });
            
            modelBuilder.Entity<OutboxMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ProcessedOn).HasFilter("[ProcessedOn] IS NULL");
            });
        }
    }
}
