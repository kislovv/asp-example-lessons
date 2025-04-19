﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServicesExample.Infrastructure.Database;

#nullable disable

namespace ServicesExample.Infrastructure.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("UserSequence");

            modelBuilder.Entity("EventStudent", b =>
                {
                    b.Property<Guid>("EventsId")
                        .HasColumnType("uuid")
                        .HasColumnName("events_id");

                    b.Property<long>("StudentsId")
                        .HasColumnType("bigint")
                        .HasColumnName("students_id");

                    b.HasKey("EventsId", "StudentsId")
                        .HasName("pk_event_student");

                    b.HasIndex("StudentsId")
                        .HasDatabaseName("ix_event_student_students_id");

                    b.ToTable("event_student", (string)null);
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("DateTimeOfEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time_of_end");

                    b.Property<DateTime>("DateTimeOfStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time_of_start");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("name");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("place");

                    b.Property<long>("Quota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(1L)
                        .HasColumnName("quota");

                    b.Property<long>("Score")
                        .HasColumnType("bigint")
                        .HasColumnName("score");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_events_author_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('\"UserSequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<long>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.HasIndex("Login");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.Author", b =>
                {
                    b.HasBaseType("ServicesExample.Domain.Entities.User");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.Student", b =>
                {
                    b.HasBaseType("ServicesExample.Domain.Entities.User");

                    b.Property<long>("Course")
                        .HasColumnType("bigint")
                        .HasColumnName("course");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("group");

                    b.Property<string>("Institute")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("institute");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name")
                        .HasAnnotation("MinimumLength", 3);

                    b.Property<long>("Score")
                        .HasColumnType("bigint")
                        .HasColumnName("score");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("EventStudent", b =>
                {
                    b.HasOne("ServicesExample.Domain.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_student_events_events_id");

                    b.HasOne("ServicesExample.Domain.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_student_students_students_id");
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.Event", b =>
                {
                    b.HasOne("ServicesExample.Domain.Entities.Author", "Author")
                        .WithMany("Events")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_events_authors_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ServicesExample.Domain.Entities.Author", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
