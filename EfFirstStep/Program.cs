using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EfFirstStep
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            //InstructorWithLocation();

            //GetDepartmentWithInstructor();

            //GetStudentEmrolmentCourseDepartment();

            GetCourseOfInstructor();

            //CountEnrolmentedCourseOfStudent();

            //CountStudentEachGrade()

            //CountStudentOfEachGrade();

        }
        /// <summary>
        /// Count student for each grade
        /// </summary>
        private static void CountStudentOfEachGrade()
        {
            using var db = new DataSubContext();
            var listGradeOfEnrollment = db.Enrollments
                .GroupBy(x => x.Grade)
                .Select(x => new StudentModel()
                {
                    Grade = x.Key,
                    TotalStudent = x.Count(),
                });
            Console.WriteLine("{0,6} {1,15}\n\n", "Grade", "TotalStudent");
            foreach (var item in listGradeOfEnrollment)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.Grade, item.TotalStudent);
                Console.WriteLine(sb);
            }
        }

        /// <summary>
        /// Count course for each department
        /// </summary>
        private static void CountCourseEachDepartment()
        {
            using var db = new DataSubContext();
            var listCourseOfDepartment = db.Dapartments.Include(x => x.Courses).ToList();
            Console.WriteLine("{0,6} {1,15}\n\n", "DapartmentName", "Totalcourse");
            foreach (var item in listCourseOfDepartment)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.DepartmentName, item.Courses.Count);
                Console.WriteLine(sb);
            }
        }

        /// <summary>
        /// Get all student + count enrollment course has grade
        /// </summary>
        private static void CountStudentEachGrade()
        {
            using var db = new DataSubContext();
            var ListGradeHasStudent = db.Enrollments.Include(x => x.Student);
            Console.WriteLine("{0,6} {1,15}\n\n", "Grade", "TotalStudent");
            foreach (var item in ListGradeHasStudent)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.Grade, item.Student);
                Console.WriteLine(sb);
            }
        }

        /// <summary>
        /// Get all student + count enrollment course
        /// </summary>
        private static void CountEnrolmentedCourseOfStudent()
        {
            using var db = new DataSubContext();
            var listStudentEnrollment = db.Students.Include(x => x.Enrollments).ToList();
            Console.WriteLine("{0,6} {1,15}\n\n", "InstructorName", "TotalCourse");
            foreach (var item in listStudentEnrollment)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.Fullname, item.Enrollments.Count);
                Console.WriteLine(sb);
            }
        }

        /// <summary>
        /// Get all courses of instructor
        /// </summary>
        private static void GetCourseOfInstructor()
        {
            using var db = new DataSubContext();
            var listCourseOfInstructor = db.Instructors.Include(x => x.CourseAssignments).ThenInclude(x=>x.Course).ToList();
            Console.WriteLine("{0,6}\n\n", "InstructorName");
            foreach (var item in listCourseOfInstructor)
            {
                var sb = string.Format("{0,-6}\n",
                item.Fullname);
                Console.WriteLine(sb);
                Console.WriteLine("{0,12} {1,16}\n\n", "CourseID", "CourseTitle");
                foreach (var ca in item.CourseAssignments)
                {
                    var courseAss = string.Format("{0,12} {1,16}\n", ca.CourseID,ca.Course.Title);
                    Console.WriteLine(courseAss);
                }
            }
        }

        /// <summary>
        /// Get all Student + Enrollment + CourseName + DepartmentName
        /// </summary>
        private static void GetStudentEmrolmentCourseDepartment()
        {
            using var db = new DataSubContext();
            List<Student> listStudentEnrollmentCourse = db.Students.Include(x => x.Enrollments)
                .ThenInclude(y => y.Course)
                .ThenInclude(z=>z.Dapartment)
                .ToList();
            Console.WriteLine("{0,-6}\n\n", "StudentName");
            foreach (var item in listStudentEnrollmentCourse)
            {
                var sb = string.Format("{0,-6}\n",
                item.Fullname);
                Console.WriteLine(sb);
                Console.WriteLine("{0,12} {1,16} {2,19}\n\n", "EnrollmentID", "Course", "DeparmentName");
                foreach (var e in item.Enrollments)
                {
                    var enroll = string.Format("{0,12} {1,16} {2,19}\n", e.EnrollmentID ,e.Course.Title, e.Course.Dapartment.DepartmentName);
                    Console.WriteLine(enroll);
                }
            }
        }

        /// <summary>
        /// Get all instructor with location
        /// </summary>
        private static void InstructorWithLocation()
        {
            using var db = new DataSubContext();
            List<Instructor> listIntructorWithLocation = db.Instructors.Include(x => x.OfficeAssignment).ToList();
            Console.WriteLine("{0,6} {1,15}\n\n", "InstructorName", "Location");
            foreach (var item in listIntructorWithLocation)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.Fullname, item?.OfficeAssignment?.LocationIn);
                Console.WriteLine(sb);
            }
        }

        /// <summary>
        /// Get all department +Instructor.LastName
        /// </summary>
        private static void GetDepartmentWithInstructor()
        {
            using var db = new DataSubContext();
            List<Dapartment> listDepartmentInstructor = db.Dapartments.Include(x => x.Instructor).ToList();
            Console.WriteLine("{0,6} {1,15}\n\n", "DepartmentName", "InstructorName");
            foreach (var item in listDepartmentInstructor)
            {
                var sb = string.Format("{0,-6} {1,15}\n",
                item.DepartmentName, item.Instructor.Fullname);
                Console.WriteLine(sb);
            }
        }
    }
    public class DataSubContext : DbContext
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

    public class Instructor
    {
        public int InstructorID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime? HireDate { get; set; }
        public List<CourseAssignment> CourseAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }
        public string Fullname => LastName.Trim() + " " + FirstMidName.Trim();
    }

    public class OfficeAssignment
    {
        public int InstructorID { get; set; }
        public string LocationIn { get; set; }
        public Instructor Instructor { get; set; }
    }

    public class Dapartment
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int? InstructorID { get; set; }
        public Instructor Instructor { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int? Credits { get; set; }
        public int DepartmentID { get; set; }
        public Dapartment Dapartment { get; set; }
        public List<CourseAssignment> CourseAssignments { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }

    public class CourseAssignment
    {
        public int CourseID { get; set; }
        public int InstructorID { get; set; }
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }

    }

    public class Student
    {
        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public string Fullname => LastName.Trim() + " " + FirstMidName.Trim();
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public string Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
    public class DepartmentModel
    {
        public string DepartmentName { get; set; }
        public int TotalStudent { get; set; }
        public int TotalCourse { get; set; }
    }
    public class StudentModel
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public List<Enrollment> Enrollments { get; set; }
        public string Grade { get; set; }
        public int TotalStudent { get; set; }
        public int EnrollmentCount { get; set; }
    }
}
