using EfFirstStep.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            //GetCourseOfInstructor();

            //CountEnrolmentedCourseOfStudent();

            CountCourseEachDepartment();

            //CountStudentEachGrade();

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
            Console.WriteLine("{0,6} {1,18}\n\n", "InstructorName", "TotalCourse");
            foreach (var item in listStudentEnrollment)
            {
                var sb = string.Format("{0,-6} {1,18}\n",
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
                Console.WriteLine("{0,12} {1,16} {2,19}\n", "EnrollmentID", "Course", "DeparmentName");
                foreach (var e in item.Enrollments)
                {
                    var enroll = string.Format("{0,12} {1,16} {2,19}\n", e.EnrollmentID ,e.Course.Title, e.Course.Dapartment.DepartmentName);
                    Console.WriteLine(enroll);
                }
            }
        }

        /// <summary>
        /// Get all department +Instructor.FullName
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

    }
}
