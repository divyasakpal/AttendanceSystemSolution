using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Data.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AttendanceSystem.Data
{
    public class DataContext : DbContext
    {
        // public DataContext(DbContextOptions<DataContext> options) : base(options)
        //{

        //}
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("AppDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasKey(e => e.AttendanceId);

            modelBuilder.Entity<Attendance>()
                .Property(e => e.AttendanceId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Employee>()
               .HasKey(e => e.Id);
        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}

