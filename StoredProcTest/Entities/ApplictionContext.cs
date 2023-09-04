using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcTest.OutputModel;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace StoredProcTest.Entities
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .ToTable("Student");
            modelBuilder.Entity<Student>()
                .Property(s => s.StudentId)
                .HasColumnName("StudentId");
            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Student>()
                .Property(s => s.Age)
                .IsRequired(false);

            modelBuilder.Entity<Classes>()
        .HasData(
            new Classes
            {
                ClassId = 1,
                ClassName = "English"
            },
            new Classes
            {

                ClassId = 2,
                ClassName = "Math"
            }
        );

            modelBuilder.Entity<Student>()
        .HasData(
            new Student
            {
                StudentId = Guid.NewGuid(),
                Name = "John Doe",
                Age = 30,
                ClassId = 1
            },
            new Student
            {
                StudentId = Guid.NewGuid(),
                Name = "Jane Doe",
                Age = 25,
                ClassId = 2
            }
        );
        }

        public async virtual Task<T> ExecuteReaderAsync<T>(Func<DbDataReader, T> mapEntities,
    string exec, params object[] parameters)
        {
            using (var conn = new SqlConnection(base.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand(exec, conn))
                {
                    conn.Open();
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            T data = mapEntities(reader);
                            return data;
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
