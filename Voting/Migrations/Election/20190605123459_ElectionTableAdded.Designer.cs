﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Voting.Models.DbContexts;

namespace Voting.Migrations.Election
{
    [DbContext(typeof(ElectionDbContext))]
    [Migration("20190605123459_ElectionTableAdded")]
    partial class ElectionTableAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Voting.Models.AlreadyVoted", b =>
                {
                    b.Property<string>("Student")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Student");

                    b.ToTable("AlreadyVoted");
                });

            modelBuilder.Entity("Voting.Models.Candidate", b =>
                {
                    b.Property<int>("CanId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CandidateName")
                        .HasMaxLength(20);

                    b.Property<int?>("CategoryCatId");

                    b.Property<string>("Photo");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("Year");

                    b.HasKey("CanId");

                    b.HasIndex("CategoryCatId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Voting.Models.Category", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasMaxLength(60);

                    b.Property<Guid?>("ElectionId");

                    b.Property<byte[]>("TimeSamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("CatId");

                    b.HasIndex("ElectionId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Voting.Models.ElectionState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateClosed");

                    b.Property<string>("Description")
                        .HasMaxLength(30);

                    b.Property<bool>("Ongoing");

                    b.HasKey("Id");

                    b.ToTable("Election");
                });

            modelBuilder.Entity("Voting.Models.Votes", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("CandidateId");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("VoteCount");

                    b.HasKey("CategoryId", "CandidateId");

                    b.HasIndex("CandidateId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Voting.Models.Candidate", b =>
                {
                    b.HasOne("Voting.Models.Category", "Category")
                        .WithMany("Candidates")
                        .HasForeignKey("CategoryCatId");
                });

            modelBuilder.Entity("Voting.Models.Category", b =>
                {
                    b.HasOne("Voting.Models.ElectionState", "Election")
                        .WithMany("Categories")
                        .HasForeignKey("ElectionId");
                });

            modelBuilder.Entity("Voting.Models.Votes", b =>
                {
                    b.HasOne("Voting.Models.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Voting.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
