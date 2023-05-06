using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class VotingsystemContext : DbContext
{
    public VotingsystemContext()
    {
    }

    public VotingsystemContext(DbContextOptions<VotingsystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var connectionString = configuration.GetConnectionString("votingsystem");

            if (connectionString != null)
            {
                optionsBuilder.UseMySQL(connectionString);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("candidate");

            entity.HasIndex(e => e.Dpi, "dpi").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("dpi");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Party)
                .HasMaxLength(60)
                .HasColumnName("party");
            entity.Property(e => e.Proposal)
                .HasColumnType("text")
                .HasColumnName("proposal");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("person");

            entity.HasIndex(e => e.Dpi, "dpi").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("dpi");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status1).HasColumnName("status");
            entity.Property(e => e.TableName)
                .HasMaxLength(60)
                .HasColumnName("table_name");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vote");

            entity.HasIndex(e => e.CandidateId, "candidate_id");

            entity.HasIndex(e => e.PersonId, "person_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Vote1).HasColumnName("vote");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Votes)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vote_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Votes)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vote_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
