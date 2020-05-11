﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SessionalAllocation.Data;

namespace SessionalAllocation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191029232711_AddFKTimeAvailability")]
    partial class AddFKTimeAvailability
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SessionalAllocation.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("SessionalAllocation.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AreYouStudying");

                    b.Property<string>("AustralianWorkRights");

                    b.Property<string>("CanvasTraining");

                    b.Property<string>("CitizenshipStudyStatus");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<int>("CurrentQualificationType");

                    b.Property<string>("CurrentStudyingQualification");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FSETSessionalInduction");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("LinkedInProfileUrl");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("NumberYearsWorkExperience");

                    b.Property<string>("OtherTraining");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode");

                    b.Property<string>("PreferredName");

                    b.Property<string>("PreviousTeachingExperience");

                    b.Property<string>("Publications");

                    b.Property<string>("Qualification")
                        .IsRequired();

                    b.Property<string>("QualificationName");

                    b.Property<string>("QulificationCompletionYear");

                    b.Property<byte[]>("ResumeContent");

                    b.Property<string>("ResumeFileName");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<bool>("StudyingAtSwinburne");

                    b.Property<string>("SwinburneID");

                    b.Property<string>("SwinburneSessionalInduction");

                    b.Property<string>("TutorTraining");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<bool>("UserFullyRegistered");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("VisaNumber");

                    b.Property<string>("VisaType");

                    b.Property<string>("WorkRights");

                    b.Property<string>("YoutubeUrl");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers","dbo");
                });

            modelBuilder.Entity("SessionalAllocation.Models.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Applications", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<int>("AppliedClass");

                    b.Property<bool>("Approved");

                    b.Property<int>("Id");

                    b.Property<int>("Preference");

                    b.Property<bool>("ProvisionallyAllocated");

                    b.HasKey("ApplicationId")
                        .HasName("PK__Applicat__C93A4C99FF7BBAB0");

                    b.HasIndex("Applicant");

                    b.HasIndex("AppliedClass");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Allocated");

                    b.Property<bool>("Approved");

                    b.Property<string>("ClassType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("DayOfWeek");

                    b.Property<TimeSpan>("EndTimeScheduled");

                    b.Property<string>("Location");

                    b.Property<DateTime>("StartDate");

                    b.Property<TimeSpan>("StartTimeScheduled");

                    b.Property<string>("StudyPeriod");

                    b.Property<string>("TutorAllocated")
                        .HasMaxLength(450);

                    b.Property<int>("UnitId");

                    b.Property<string>("Year");

                    b.Property<string>("roomDetails");

                    b.HasKey("Id");

                    b.HasIndex("TutorAllocated");

                    b.HasIndex("UnitId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentOwner");

                    b.Property<string>("Name")
                        .HasMaxLength(450);

                    b.Property<int>("School");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentOwner");

                    b.HasIndex("School");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Payrate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.Property<float>("Rate");

                    b.HasKey("Id");

                    b.ToTable("Payrate");
                });

            modelBuilder.Entity("SessionalAllocation.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Faculty");

                    b.Property<string>("Name")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("Faculty");

                    b.ToTable("School");
                });

            modelBuilder.Entity("SessionalAllocation.Models.TimeAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicantNavigationId");

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("FromTime");

                    b.Property<bool>("IsAllDay");

                    b.Property<bool>("IsAvailable");

                    b.Property<int>("WeekDay");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantNavigationId");

                    b.ToTable("TimeAvailability");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Department");

                    b.Property<string>("UnitCode");

                    b.Property<string>("UnitName")
                        .HasMaxLength(100);

                    b.Property<string>("UnitOwner")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("Department");

                    b.HasIndex("UnitOwner");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SessionalAllocation.Models.ApplicationUserRole", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SessionalAllocation.Models.Applications", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "ApplicantNavigation")
                        .WithMany("Applications")
                        .HasForeignKey("Applicant")
                        .HasConstraintName("FK_Applicant_ID_Identity");

                    b.HasOne("SessionalAllocation.Models.Class", "AppliedClassNavigation")
                        .WithMany("Applications")
                        .HasForeignKey("AppliedClass")
                        .HasConstraintName("FK_AppliedClass_ID");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Class", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "TutorAllocatedNavigation")
                        .WithMany("Class")
                        .HasForeignKey("TutorAllocated")
                        .HasConstraintName("FK_Tutor_Id");

                    b.HasOne("SessionalAllocation.Models.Unit", "Unit")
                        .WithMany("Class")
                        .HasForeignKey("UnitId")
                        .HasConstraintName("FK_Unit_ID");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Department", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "DepartmentOwnerNavigation")
                        .WithMany("Department")
                        .HasForeignKey("DepartmentOwner")
                        .HasConstraintName("FK_DepartmentOwner_ID");

                    b.HasOne("SessionalAllocation.Models.School", "SchoolNavigation")
                        .WithMany("Department")
                        .HasForeignKey("School")
                        .HasConstraintName("FK_School_Name");
                });

            modelBuilder.Entity("SessionalAllocation.Models.School", b =>
                {
                    b.HasOne("SessionalAllocation.Models.Faculty", "FacultyNavigation")
                        .WithMany("School")
                        .HasForeignKey("Faculty")
                        .HasConstraintName("FK_Faculty_Name");
                });

            modelBuilder.Entity("SessionalAllocation.Models.TimeAvailability", b =>
                {
                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "ApplicantNavigation")
                        .WithMany()
                        .HasForeignKey("ApplicantNavigationId");
                });

            modelBuilder.Entity("SessionalAllocation.Models.Unit", b =>
                {
                    b.HasOne("SessionalAllocation.Models.Department", "DepartmentNavigation")
                        .WithMany("Unit")
                        .HasForeignKey("Department")
                        .HasConstraintName("FK_Department_Name");

                    b.HasOne("SessionalAllocation.Models.ApplicationUser", "UnitOwnerNavigation")
                        .WithMany("Unit")
                        .HasForeignKey("UnitOwner")
                        .HasConstraintName("FK_UnitOwner_ID");
                });
#pragma warning restore 612, 618
        }
    }
}
