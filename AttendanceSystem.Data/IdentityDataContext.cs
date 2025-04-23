using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AttendanceSystem.Data.DomainEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AttendanceSystem.Data
{

    public class IdentityDataContext :IdentityDbContext
    {
        // public DataContext(DbContextOptions<DataContext> options) : base(options)
        //{

        //}
        protected readonly IConfiguration Configuration;

        public IdentityDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("AuthDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           var  readerId = "e47e815d-0c3c-4e07-8750-c6d0c3bf197e";
           var  writerId = "bb847d9f-adfb-48db-92ec-b44d1e53c2ac";
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id= readerId,
                    Name ="Reader",
                    NormalizedName= "READER",
                    ConcurrencyStamp= readerId
                },
                new IdentityRole()
                {
                     Id= writerId,
                    Name ="writer",
                    NormalizedName="WRITER",
                    ConcurrencyStamp= writerId
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
           // modelBuilder.Entity<IdentityUser>();
          
    }

    }
}
