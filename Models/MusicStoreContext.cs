using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Assignment5.Models;

public partial class MusicStoreContext : DbContext
{
    private IConfiguration Configuration;
    public MusicStoreContext()
    {
    }

    public MusicStoreContext(DbContextOptions<MusicStoreContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Musician> Musicians { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0749F62458");

            entity.ToTable("CartItem");

            entity.HasOne(d => d.Song).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__SongId__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__UserId__6FE99F9F");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E7EB0894C");

            entity.Property(e => e.GenreName).HasMaxLength(50);
        });

        modelBuilder.Entity<Musician>(entity =>
        {
            entity.HasKey(e => e.MusicianId).HasName("PK__Musician__4904FC9A2A6BB35F");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("PK__tmp_ms_x__12E3D697D91EBB36");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_dbo.Songs_dbo.Genres_GenreId");

            entity.HasOne(d => d.Musician).WithMany(p => p.Songs)
                .HasForeignKey(d => d.MusicianId)
                .HasConstraintName("FK_dbo.Songs_dbo.Musicians_MusicianId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C9F1D0290");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
