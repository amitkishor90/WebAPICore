using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.DatabaseModels;

public partial class CurdApiContext : DbContext
{
    public CurdApiContext()
    {
    }

    public CurdApiContext(DbContextOptions<CurdApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
             
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07A342D091");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentGuid, "UQ__Departme__82B00A36D8BB392E").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07178178F1");

            entity.HasIndex(e => e.Emailid, "UQ__Employee__7EDA1EE6E6F49B21").IsUnique();

            entity.HasIndex(e => e.Guid, "UQ__Employee__A2B5777DF0AAF3F5").IsUnique();

            entity.HasIndex(e => e.PenCardNo, "UQ__Employee__AEB7813BE8740A69").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DateIns)
                .HasColumnType("datetime")
                .HasColumnName("Date_ins");
            entity.Property(e => e.DateUpdate)
                .HasColumnType("datetime")
                .HasColumnName("Date_Update");
            entity.Property(e => e.Emailid)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PenCardNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__31EC6D26");

            entity.HasOne(d => d.Gender).WithMany(p => p.Employees)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__Employees__Gende__30F848ED");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gender__3214EC078E23196C");

            entity.ToTable("Gender");

            entity.HasIndex(e => e.GenderGuid, "UQ__Gender__58291F104D737266").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
