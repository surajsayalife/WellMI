using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WellMI.Models;

public partial class WellMiDevContext : DbContext
{
    public WellMiDevContext()
    {
    }

    public WellMiDevContext(DbContextOptions<WellMiDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User");

            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            //entity.Property(e => e.Token)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
