using ControlEstacionamientoBackend.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace ControlEstacionamientoBackend.Data
{
    public class EstacionamientoContext : DbContext
    {
        public EstacionamientoContext(DbContextOptions<EstacionamientoContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Auto> Autos{ get; set; }
        public DbSet<Plaza> Plazas{ get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // unique 
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Auto>().HasIndex(c => c.Patente).IsUnique();

            modelBuilder.Entity<Plaza>().HasIndex(c => c.NumeroPlaza).IsUnique();

            modelBuilder.Entity<Usuario>().HasIndex(c => c.Username).IsUnique();

            // val defectos

            modelBuilder.Entity<Plaza>().Property(p => p.Estado).HasDefaultValue("Libre");

            modelBuilder.Entity<Auto>()
            .HasOne(a => a.Cliente)
            .WithMany()  
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plaza>()
            .HasOne(p => p.Cliente)
            .WithMany() 
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        }

        public void SeedData()
        {
            if (!Usuarios.Any())
            {
                var passHash = BCrypt.Net.BCrypt.HashPassword("admin");

                var admin = new Usuario
                {
                    Username = "admin",
                    Password = passHash
                };
                Usuarios.Add(admin);
                SaveChanges();
            }  
        }
    }
}
