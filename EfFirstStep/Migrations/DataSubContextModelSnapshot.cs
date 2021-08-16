﻿// <auto-generated />
using System;
using EfFirstStep;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EfFirstStep.Migrations
{
    [DbContext(typeof(DataSubContext))]
    partial class DataSubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EfFirstStep.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("course_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Credits")
                        .HasColumnType("int")
                        .HasColumnName("credits");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int")
                        .HasColumnName("department_id");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("EfFirstStep.CourseAssignment", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("int")
                        .HasColumnName("course_id");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int")
                        .HasColumnName("instructor_id");

                    b.HasKey("CourseID");

                    b.HasIndex("InstructorID");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("EfFirstStep.Dapartment", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("department_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Budget")
                        .HasColumnType("int")
                        .HasColumnName("budget");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("department_name");

                    b.Property<int?>("InstructorID")
                        .HasColumnType("int")
                        .HasColumnName("instructor_id");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.HasKey("DepartmentID");

                    b.HasIndex("InstructorID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("EfFirstStep.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("enrollment_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID")
                        .HasColumnType("int")
                        .HasColumnName("course_id");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("grade");

                    b.Property<int>("StudentID")
                        .HasColumnType("int")
                        .HasColumnName("student_id");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("EfFirstStep.Instructor", b =>
                {
                    b.Property<int>("InstructorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("instructor_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstMidName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_mid_name");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("hire_date");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.HasKey("InstructorID");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("EfFirstStep.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID")
                        .HasColumnType("int")
                        .HasColumnName("instructor_id");

                    b.Property<string>("LocationIn")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("location");

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("EfFirstStep.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("student_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EnrollmentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("enrollment_date");

                    b.Property<string>("FirstMidName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_mid_name");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("EfFirstStep.Course", b =>
                {
                    b.HasOne("EfFirstStep.Dapartment", "Dapartment")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dapartment");
                });

            modelBuilder.Entity("EfFirstStep.CourseAssignment", b =>
                {
                    b.HasOne("EfFirstStep.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EfFirstStep.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("EfFirstStep.Dapartment", b =>
                {
                    b.HasOne("EfFirstStep.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorID");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("EfFirstStep.Enrollment", b =>
                {
                    b.HasOne("EfFirstStep.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EfFirstStep.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EfFirstStep.OfficeAssignment", b =>
                {
                    b.HasOne("EfFirstStep.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("EfFirstStep.OfficeAssignment", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("EfFirstStep.Course", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("EfFirstStep.Dapartment", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EfFirstStep.Instructor", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("OfficeAssignment");
                });

            modelBuilder.Entity("EfFirstStep.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
