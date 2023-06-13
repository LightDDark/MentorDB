﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(MentorDataContext))]
    partial class MentorDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AllDay")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsRepeat")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<bool>("Settled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Mission");
                });

            modelBuilder.Entity("Domain.Priv.DayString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.ToTable("DayString");
                });

            modelBuilder.Entity("Domain.Priv.HourString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.ToTable("HourString");
                });

            modelBuilder.Entity("Domain.Priv.MissionRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.ToTable("MissionRank");
                });

            modelBuilder.Entity("Domain.ScheduleSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndHour")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxHoursPerDay")
                        .HasColumnType("int");

                    b.Property<int>("MinGap")
                        .HasColumnType("int");

                    b.Property<int>("MinTimeFrame")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartHour")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ScheduleSetting");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Mission", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany("Missions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Priv.DayString", b =>
                {
                    b.HasOne("Domain.Mission", null)
                        .WithMany("OptionalDays")
                        .HasForeignKey("MissionId");
                });

            modelBuilder.Entity("Domain.Priv.HourString", b =>
                {
                    b.HasOne("Domain.Mission", null)
                        .WithMany("OptionalHours")
                        .HasForeignKey("MissionId");
                });

            modelBuilder.Entity("Domain.Priv.MissionRank", b =>
                {
                    b.HasOne("Domain.Mission", null)
                        .WithMany("RankListHistory")
                        .HasForeignKey("MissionId");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.ScheduleSetting", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Domain.Mission", b =>
                {
                    b.Navigation("OptionalDays");

                    b.Navigation("OptionalHours");

                    b.Navigation("RankListHistory");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Missions");
                });
#pragma warning restore 612, 618
        }
    }
}
