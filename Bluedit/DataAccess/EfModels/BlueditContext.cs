using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bluedit.DataAccess.EfModels;

public partial class BlueditContext : DbContext
{
    public BlueditContext()
    {
    }

    public BlueditContext(DbContextOptions<BlueditContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Opinion> Opinions { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Bluedit;Trusted_Connection=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("answer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.ThreadId).HasColumnName("threadId");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("userId");

            entity.HasOne(d => d.Thread).WithMany(p => p.Answers)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_answer_user");

            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_answer_user1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Title)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Opinion>(entity =>
        {
            entity.ToTable("opinion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnswerId).HasColumnName("answerId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Like).HasColumnName("like");

            entity.HasOne(d => d.Answer).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opinion_answer1");

            entity.HasOne(d => d.Author).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opinion_answer");
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.ToTable("thread");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.RootAnswerId).HasColumnName("rootAnswerId");
            entity.Property(e => e.Title)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Threads)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_thread_category");

            entity.HasOne(d => d.RootAnswer).WithMany(p => p.Threads)
                .HasForeignKey(d => d.RootAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_thread_answer");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Mail)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
