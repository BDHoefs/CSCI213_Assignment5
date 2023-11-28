using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment5.Models;

public partial class MusicStoreContext : DbContext
{
    public MusicStoreContext()
    {
    }

    public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackOwnership> TrackOwnerships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ben\\source\\repos\\Assignment5\\Data\\MusicStore.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PK__Tracks__7A74F8E0C2AA3EE8");

            entity.Property(e => e.Artist).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<TrackOwnership>(entity =>
        {
            entity.HasKey(e => e.OwnershipId).HasName("PK__TrackOwn__A0D87189C23DEA8C");

            entity.ToTable("TrackOwnership");

            entity.HasOne(d => d.Track).WithMany(p => p.TrackOwnerships)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("FK_dbo.TrackOwnership_dbo.Tracks_TrackId");

            entity.HasOne(d => d.User).WithMany(p => p.TrackOwnerships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_dbo.TrackOwnership_dbo.Users_UserID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB519C67F");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
