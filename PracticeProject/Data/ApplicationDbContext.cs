using Microsoft.EntityFrameworkCore;
using PracticeProject.Models;


namespace PracticeProject.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grupa> Grupas { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<TextWork> TextWorks { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<LabWork> LabWorks { get; set; }
        public DbSet<CourseGrupa> CourseGrupas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //modelBuilder.Entity<CourseGrupa>().HasNoKey();
            modelBuilder.Entity<CourseGrupa>().HasKey(pc => new { pc.IdCourse, pc.IdGrupa });
            modelBuilder.Entity<CourseGrupa>().HasOne(p => p.Course).WithMany(pc => pc.courseGrupas).HasForeignKey(p => p.IdCourse);
            modelBuilder.Entity<CourseGrupa>().HasOne(p => p.Grupa).WithMany(pc => pc.courseGrupas).HasForeignKey(c => c.IdGrupa);

            modelBuilder.Entity<Grupa>().HasKey(g => new { g.Code });
            modelBuilder.Entity<HomeWork>().HasNoKey();
            modelBuilder.Entity<LabWork>().HasNoKey();
            modelBuilder.Entity<TextWork>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
