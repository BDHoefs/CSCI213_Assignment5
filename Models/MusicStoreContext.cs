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

    public virtual DbSet<Musician> Musicians { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ben\\source\\repos\\Assignment5\\Data\\MusicStore.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musician>(entity =>
        {
            entity.HasKey(e => e.MusicianId).HasName("PK__Musician__4904FC9A2A6BB35F");

            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__tmp_ms_x__12E3D697D91EBB36");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Musician).WithMany(p => p.Songs)
                .HasForeignKey(d => d.MusicianId)
                .HasConstraintName("FK_dbo.Songs_dbo.Musicians_MusicianId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
