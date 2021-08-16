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

            //CountCourseEachDepartment();

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
            Console.WriteLine("{0,6} {1,15}\n", "Grade", "TotalStudent");
            foreach (var item in listGradeOfEnrollment)
            {
                //var sb = string.Format("{0,-6} {1,15}\n",
                //item.Grade, item.TotalStudent);
                //Console.WriteLine(sb);
                Console.WriteLine($"{item.Grade,-6} {item.TotalStudent,15}\n");
            }
        }

        /// <summary>
        /// Count course for each department
        /// </summary>
        private static void CountCourseEachDepartment()
        {
            using var db = new DataSubContext();
            var listCourseOfDepartment = db.Dapartments.Include(x => x.Courses)
                .Select(x=>new DepartmentModel()
                {
                    DepartmentName=x.DepartmentName,
                    TotalCourse=x.Courses.Count
            });
            Console.WriteLine("{0,6} {1,15}\n", "DapartmentName", "Totalcourse");
            foreach (var item in listCourseOfDepartment)
            {
                Console.WriteLine($"{item.DepartmentName,-6} {item.TotalCourse,15}\n");
            }
        }

        /// <summary>
        /// Get all student + count enrollment course
        /// </summary>
        private static void CountEnrolmentedCourseOfStudent()
        {
            using var db = new DataSubContext();
            var listStudentEnrollment = db.Students.Include(x => x.Enrollments)
                .Select(x => new StudentModel()
                { 
                TotalEnrollment=x.Enrollments.Count()
                });
            Console.WriteLine("{0,10} {1,20}\n", "InstructorName", "TotalCourse");
            foreach (var item in listStudentEnrollment)
            {
                Console.WriteLine($"{item.Fullname,10} {item.TotalEnrollment,20}\n");
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
                Console.WriteLine($"{item.Fullname,-6}\n");
                Console.WriteLine("{0,10} {1,15}\n", "CourseID", "CourseTitle");
                foreach (var ca in item.CourseAssignments)
                {
                    Console.WriteLine($"{ca.CourseID,10} {ca.Course.Title,15}\n\n");
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
            foreach (var item in listStudentEnrollmentCourse)
            {
                Console.WriteLine("{0,-6}\n", "StudentName");
                Console.WriteLine($"{item.Fullname,-6}\n");
                Console.WriteLine("{0,10} {1,20} {2,30}\n", "EnrollmentID", "CourseName", "DeparmentName");
                foreach (var e in item.Enrollments)
                {
                    Console.WriteLine($"{e.EnrollmentID,10} {e.Course.Title,20} {e.Course.Dapartment.DepartmentName,30}\n\n");
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
            Console.WriteLine("{0,6} {1,15}\n", "DepartmentName", "InstructorName");
            foreach (var item in listDepartmentInstructor)
            {
                Console.WriteLine($"{item.DepartmentName,-6} {item?.Instructor?.Fullname,23}\n");
            }
        }

        /// <summary>
        /// Get all instructor with location
        /// </summary>
        private static void InstructorWithLocation()
        {
            using var db = new DataSubContext();
            List<Instructor> listIntructorWithLocation = db.Instructors.Include(x => x.OfficeAssignment).ToList();
            Console.WriteLine("{0,20} {1,25}\n", "InstructorName", "Location");
            foreach (var item in listIntructorWithLocation)
            {
                Console.WriteLine($"{item.Fullname,20} {item?.OfficeAssignment?.LocationIn,25}\n");
            }
        }

    }
}
