using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfFirstStep.Persistence
{
    public class DataSubContext:DbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Dapartment> Dapartments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DINHNHUNG\SQLEXPRESS01;Database=db_trying;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Instructor>(
                eb =>
                {
                    eb.ToTable("Instructor").HasKey(i => i.InstructorID);
                    eb.HasOne(i => i.OfficeAssignment).WithOne(o => o.Instructor).HasForeignKey<OfficeAssignment>(i => i.InstructorID);
                    //eb.Property(i =>new { i.InstructorID, i.LastName,i.FirstMidName, i.HireDate})
                    //.HasColumnName("instructor_id","last_name","first_mid_name","hire_date");
                    eb.Property(i => i.InstructorID).HasColumnName("instructor_id");
                    eb.Property(i => i.LastName).HasColumnName("last_name");
                    eb.Property(i => i.FirstMidName).HasColumnName("first_mid_name");
                    eb.Property(i => i.HireDate).HasColumnName("hire_date");
                });


            modelBuilder.Entity<OfficeAssignment>(
                eb =>
                {
                    eb.ToTable("OfficeAssignment").HasKey(o => o.InstructorID);
                    eb.Property(o => o.InstructorID).HasColumnName("instructor_id");
                    eb.Property(o => o.LocationIn).HasColumnName("location");
                });

            modelBuilder.Entity<Dapartment>(
                eb => {
                    eb.ToTable("Department").HasKey(d => d.DepartmentID);
                    eb.HasOne(d => d.Instructor).WithMany().HasForeignKey(d => d.InstructorID);
                    eb.Property(d => d.DepartmentID).HasColumnName("department_id");
                    eb.Property(d => d.DepartmentName).HasColumnName("department_name");
                    eb.Property(d => d.Budget).HasColumnName("budget");
                    eb.Property(d => d.StartDate).HasColumnName("start_date");
                    eb.Property(d => d.InstructorID).HasColumnName("instructor_id");
                });

            modelBuilder.Entity<Course>(
                eb =>
                {
                    eb.ToTable("Course").HasKey(c => c.CourseID);
                    eb.HasOne(c => c.Dapartment).WithMany(d => d.Courses).HasForeignKey(c => c.DepartmentID).HasPrincipalKey(d => d.DepartmentID);
                    eb.Property(c => c.CourseID).HasColumnName("course_id");
                    eb.Property(c => c.Title).HasColumnName("title");
                    eb.Property(c => c.Credits).HasColumnName("credits");
                    eb.Property(c => c.DepartmentID).HasColumnName("department_id");
                });

            modelBuilder.Entity<CourseAssignment>(
                eb => {
                    eb.ToTable("CourseAssignment").HasKey(ca => ca.CourseID);
                    eb.HasOne(ca => ca.Instructor).WithMany(i => i.CourseAssignments).HasForeignKey(ca => ca.InstructorID).HasPrincipalKey(i => i.InstructorID);
                    eb.HasOne(ca => ca.Course).WithMany(c => c.CourseAssignments).HasForeignKey(ca => ca.CourseID).HasPrincipalKey(c => c.CourseID);
                    eb.Property(ca => ca.CourseID).HasColumnName("course_id");
                    eb.Property(ca => ca.InstructorID).HasColumnName("instructor_id");
                });

            modelBuilder.Entity<Student>(
                eb =>
                {
                    eb.ToTable("Student").HasKey(s => s.StudentID);
                    eb.Property(s => s.StudentID).HasColumnName("student_id");
                    eb.Property(s => s.LastName).HasColumnName("last_name");
                    eb.Property(s => s.FirstMidName).HasColumnName("first_mid_name");
                    eb.Property(s => s.EnrollmentDate).HasColumnName("enrollment_date");
                });

            modelBuilder.Entity<Enrollment>(
                eb =>
                {
                    eb.ToTable("Enrollment").HasKey(e => e.EnrollmentID);
                    eb.HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseID).HasPrincipalKey(c => c.CourseID);
                    eb.HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentID).HasPrincipalKey(s => s.StudentID);
                    eb.Property(e => e.EnrollmentID).HasColumnName("enrollment_id");
                    eb.Property(e => e.CourseID).HasColumnName("course_id");
                    eb.Property(e => e.StudentID).HasColumnName("student_id");
                    eb.Property(e => e.Grade).HasColumnName("grade");
                });
        }
    }
}
