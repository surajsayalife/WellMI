using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WellMI.CommonUtility;

namespace WellMI.Models;

public partial class WellMiContext : DbContext
{
    public WellMiContext ( DbContextOptions<WellMiContext> options )
        : base ( options )
    {
    }

    private ConnectionString _connectionString = new ConnectionString ();
    private string connectionStr;
    public WellMiContext ()
    {
    }
    public WellMiContext ( string environment )
    {
        if ( !string.IsNullOrEmpty ( environment ) && environment.ToLower () == ( "production" ) )
            connectionStr = _connectionString.LiveConnectionString;
        else if ( !string.IsNullOrEmpty ( environment ) && environment.ToLower () == ( "testing" ) )
            connectionStr = _connectionString.TestingConnectionString;
        else
            connectionStr = _connectionString.DefaultConnectionString;
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<EmailHistory> EmailHistories { get; set; }

    protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer ( "Server=192.168.29.3;Database=WellMI;Trusted_Connection=True;User ID=dbuser;Password=test#123;TrustServerCertificate=True;TrustServerCertificate=true;" );

    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<User> ( entity =>
        {
            entity.HasKey ( e => e.Id ).HasName ( "User_PK" );

            entity.ToTable ( "User" );

            entity.Property ( e => e.CreateDate ).HasColumnType ( "datetime" );
            entity.Property ( e => e.EmailId )
                .HasMaxLength ( 100 )
                .IsUnicode ( false );
            entity.Property ( e => e.FirstName )
                .HasMaxLength ( 100 )
                .IsUnicode ( false );
            entity.Property ( e => e.LastName )
                .HasMaxLength ( 100 )
                .IsUnicode ( false );
            entity.Property ( e => e.ModifyDate ).HasColumnType ( "datetime" );
            entity.Property ( e => e.Password )
                .HasMaxLength ( 100 )
                .IsUnicode ( false );
            entity.Property ( e => e.PhoneNumber )
                .HasMaxLength ( 100 )
                .IsUnicode ( false );
            entity.Property ( e => e.CreateDate ).HasColumnType ( "datetime" );
            entity.Property ( e => e.IsDeleted ).HasColumnType ( "bit" );
            entity.Property ( e => e.IsActive ).HasColumnType ( "bit" );
            entity.Property ( e => e.Gender ).HasColumnType ( "int" );
            entity.Property ( e => e.ParentId ).HasColumnType ( "int" );
        } );

        OnModelCreatingPartial ( modelBuilder );
    }

    partial void OnModelCreatingPartial ( ModelBuilder modelBuilder );
}
