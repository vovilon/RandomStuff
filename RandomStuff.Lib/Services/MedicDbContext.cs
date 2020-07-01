using RandomStuff.Lib.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RandomStuff.Lib.Services
{
    internal class SpecialityConfiguation : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.HasData(new[] 
            {
                new Speciality{ Id = 1, name = "Терапевт" },
                new Speciality{ Id = 2, name = "Зоопевт" },
                new Speciality{ Id = 3, name = "Хирург" }
            });
        }
    }

    public class MedicDbContext : DbContext
    {

        public MedicDbContext(DbContextOptions<MedicDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpecialityConfiguation());
        }


        public DbSet<Healer> Healers { get; set; }


        public DbSet<Victim> Victims { get; set; }


        public DbSet<Execution> Executions { get; set; }

        public DbSet<Speciality> Specialities { get; set; }
    }

    
}
