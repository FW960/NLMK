﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLMK.DBContext;

#nullable disable

namespace NLMK.Migrations
{
    [DbContext(typeof(ProjectsDbContext))]
    [Migration("20221019104848_AddedProperties")]
    partial class AddedProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NLMK.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LinkedDocumentCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ProjectId")
                        .HasName("Id");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("NLMK.Models.ProjectObject", b =>
                {
                    b.Property<int>("ObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObjectId"), 1L, 1);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LinkedDocuments")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectObjectObjectId")
                        .HasColumnType("int");

                    b.Property<int?>("RelatedObjectId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Stage")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WorkingHoursStandard")
                        .HasColumnType("int");

                    b.HasKey("ObjectId")
                        .HasName("ObjectId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectObjectObjectId");

                    b.ToTable("objects", (string)null);
                });

            modelBuilder.Entity("NLMK.Models.ProjectObject", b =>
                {
                    b.HasOne("NLMK.Models.Project", null)
                        .WithMany("ChildObjects")
                        .HasForeignKey("ProjectId");

                    b.HasOne("NLMK.Models.ProjectObject", null)
                        .WithMany("ChildObjects")
                        .HasForeignKey("ProjectObjectObjectId");
                });

            modelBuilder.Entity("NLMK.Models.Project", b =>
                {
                    b.Navigation("ChildObjects");
                });

            modelBuilder.Entity("NLMK.Models.ProjectObject", b =>
                {
                    b.Navigation("ChildObjects");
                });
#pragma warning restore 612, 618
        }
    }
}