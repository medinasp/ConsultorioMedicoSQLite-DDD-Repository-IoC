using ConsultorioMedico.Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedico.Infra.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Prontuarios> Prontuarios { get; set; }
        public DbSet<CadMedicos> CadMedicos { get; set; }
        public DbSet<CadPacientes> CadPacientes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextBase).Assembly);
        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextBase).Assembly);

            modelBuilder.Entity<Prontuarios>()
                .HasIndex(p => new { p.MedicoId, p.PacienteId })
                .IsUnique();

            modelBuilder.Entity<Prontuarios>()
                .HasOne(p => p.Medico)
                .WithMany()
                .HasForeignKey(p => p.MedicoId);

            modelBuilder.Entity<Prontuarios>()
                .HasOne(p => p.Paciente)
                .WithMany()
                .HasForeignKey(p => p.PacienteId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
