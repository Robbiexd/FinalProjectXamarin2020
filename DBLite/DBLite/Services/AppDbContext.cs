using DBLite.Models;
using DBLite.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLite.Services
{
    public class AppDbContext : DbContext, IDataStore<Student>
    {
        private string _dbPath;

        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 1, Name = "P1", Argb = Color.Red.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 2, Name = "P2", Argb = Color.Pink.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 3, Name = "P3", Argb = Color.DarkRed.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 4, Name = "P4", Argb = Color.Salmon.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 5, Name = "L1", Argb = Color.Blue.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 6, Name = "L2", Argb = Color.LightBlue.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 7, Name = "L3", Argb = Color.Navy.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 8, Name = "L4", Argb = Color.DarkBlue.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 9, Name = "E1A", Argb = Color.Green.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 10, Name = "E1B", Argb = Color.LightGreen.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 11, Name = "E1C", Argb = Color.DarkGreen.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 12, Name = "E2A", Argb = Color.Khaki.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 13, Name = "E2B", Argb = Color.DarkKhaki.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = 14, Name = "E2C", Argb = Color.Brown.ToArgb() });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 1, Firstname = "Adam", Lastname = "Antl", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 2, Firstname = "Bořivoj", Lastname = "Bonifác", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 3, Firstname = "Ctirad", Lastname = "Cvach", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 4, Firstname = "Daniela", Lastname = "Datlová", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 5, Firstname = "Eva", Lastname = "Eidamová", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 6, Firstname = "Filip", Lastname = "Fiala", ClassroomId = 3 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 7, Firstname = "Gisele", Lastname = "Grayová", ClassroomId = 10 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 8, Firstname = "Henrieta", Lastname = "Hozáková", ClassroomId = 8 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 9, Firstname = "Igor", Lastname = "Ivančák", ClassroomId = 2 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 10, Firstname = "Jiří", Lastname = "Janata", ClassroomId = 2 });
            modelBuilder.Entity<Student>().HasData(new Student { Id = 11, Firstname = "Karel", Lastname = "Kozák", ClassroomId = 1 });
        }

        public async Task<bool> AddItemAsync(Student item)
        {
            try
            {
                await Students.AddAsync(item);
                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Student item)
        {
            try
            {
                Students.Update(item);
                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                var student = await Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student != null)
                {
                    Students.Remove(student);
                }

                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Student> GetItemAsync(int id)
        {
            var student = await Students.Include(s => s.Classroom).FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            return student;
        }

        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            var allStudents = await Students.Include(s => s.Classroom).OrderBy(s => s.Lastname).ToListAsync();
            return allStudents;
        }

        public async Task<bool> AddClassroomAsync(Classroom item)
        {
            try
            {
                await Classrooms.AddAsync(item);
                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateClassroomAsync(Classroom item)
        {
            try
            {
                Classrooms.Update(item);
                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteClassroomAsync(int id)
        {
            try
            {
                var classroom = await Classrooms.FirstOrDefaultAsync(s => s.Id == id);
                if (classroom != null)
                {
                    Classrooms.Remove(classroom);
                }

                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Classroom> GetClassroomAsync(int id)
        {
            var classroom = await Classrooms.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            return classroom;
        }

        public async Task<IEnumerable<Classroom>> GetClassroomsAsync(bool forceRefresh = false)
        {
            var allClassrooms = await Classrooms.ToListAsync();
            return allClassrooms;
        }
    }
}
