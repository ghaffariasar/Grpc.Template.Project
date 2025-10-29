using GrpcServerProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcServerProject.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Seed();
        }


        public DbSet<Student> Students { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("Grpc_DB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasMany(c => c.PhoneNumbers).WithOne().OnDelete(DeleteBehavior.Cascade);
        }



        /// <summary>
        /// Fake Data
        /// </summary>
        private void Seed()
        {
            if (Students.Any())
                return; 

            var rnd = new Random();
            var students = new List<Student>();

            for (var i = 1; i <= 20; i++)
            {
                var student = new Student
                {
                    Id = i,
                    StudentNumber = $"Std{i}",
                    FirstName = $"Student FirstName {i}",
                    LastName = $"Student LastName {i}",
                    Description = $"Student Description {i}",
                    PhoneNumbers = i % 2 == 0 ? new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            Id = i,
                            Value = rnd.Next(10000000, 99999999).ToString()
                        }
                    } : new List<PhoneNumber>()
                };

                students.Add(student);
            }

            Students.AddRange(students);
            SaveChanges();
        }
    }
}
