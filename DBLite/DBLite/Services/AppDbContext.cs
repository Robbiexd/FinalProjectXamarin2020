using DBLite.Models;
using DBLite.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "P1", Name = "P1", Argb = Color.Red.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "P2", Name = "P2", Argb = Color.Pink.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "P3", Name = "P3", Argb = Color.DarkRed.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "P4", Name = "P4", Argb = Color.Salmon.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "L1", Name = "L1", Argb = Color.Blue.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "L2", Name = "L2", Argb = Color.LightBlue.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "L3", Name = "L3", Argb = Color.Navy.ToArgb() });
            modelBuilder.Entity<Classroom>().HasData(new Classroom { Id = "L4", Name = "L4", Argb = Color.DarkBlue.ToArgb() });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "micbart", Firstname = "Michal", Lastname = "Barth", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "jirbiel", Firstname = "Jiří", Lastname = "Bielik", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "marcele", Firstname = "Martin", Lastname = "Čeleda", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "robhak", Firstname = "Robert", Lastname = "Hák", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "marhonz", Firstname = "Martin", Lastname = "Honzátko", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "pethora", Firstname = "Petr", Lastname = "Horák", ClassroomId = "P3" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "adaanto", Firstname = "Adam", Lastname = "Antoš", ClassroomId = "P2" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "marbaum", Firstname = "Marek", Lastname = "Baumann", ClassroomId = "P2" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "olibene", Firstname = "Oliver", Lastname = "Beneš", ClassroomId = "P2" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "ondbedn", Firstname = "Ondřej", Lastname = "Bednář", ClassroomId = "P2" });
            modelBuilder.Entity<Student>().HasData(new Student { Id = "matandr", Firstname = "Matěj", Lastname = "Andráško", ClassroomId = "P1" });
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

        public async Task<bool> DeleteItemAsync(string id)
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

        public async Task<Student> GetItemAsync(string id)
        {
            var student = await Students.Include(s => s.Classroom).FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            return student;
        }

        public async Task<IEnumerable<Student>> GetItemsAsync(bool forceRefresh = false)
        {
            var allStudents = await Students.Include(s => s.Classroom).ToListAsync();
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

        public async Task<bool> DeleteClassroomAsync(string id)
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

        public async Task<Classroom> GetClassroomAsync(string id)
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
