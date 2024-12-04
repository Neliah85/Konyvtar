﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Konyvtar.Models;

public partial class KonyvtarakContext : DbContext
{
    public KonyvtarakContext()
    {
    }

    public KonyvtarakContext(DbContextOptions<KonyvtarakContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Konyvtarak> Konyvtaraks { get; set; }

    public virtual DbSet<Megyek> Megyeks { get; set; }

    public virtual DbSet<Telepulesek> Telepuleseks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=konyvtarak;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Konyvtarak>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("konyvtarak");

            entity.HasIndex(e => e.Irsz, "irsz");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cim)
                .HasMaxLength(100)
                .HasColumnName("cim");
            entity.Property(e => e.Irsz)
                .HasColumnType("int(11)")
                .HasColumnName("irsz");
            entity.Property(e => e.KonyvtarNev)
                .HasMaxLength(100)
                .HasColumnName("konyvtarNev");

            entity.HasOne(d => d.IrszNavigation).WithMany(p => p.Konyvtaraks)
                .HasPrincipalKey(p => p.Irsz)
                .HasForeignKey(d => d.Irsz)
                .HasConstraintName("konyvtarak_ibfk_1");
        });

        modelBuilder.Entity<Megyek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("megyek");

            entity.HasIndex(e => e.MegyeNev, "megyeNev").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.MegyeNev)
                .HasMaxLength(100)
                .HasColumnName("megyeNev");
        });

        modelBuilder.Entity<Telepulesek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("telepulesek");

            entity.HasIndex(e => e.Irsz, "irsz").IsUnique();

            entity.HasIndex(e => e.MegyeId, "megyeId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Irsz)
                .HasColumnType("int(11)")
                .HasColumnName("irsz");
            entity.Property(e => e.MegyeId)
                .HasColumnType("int(11)")
                .HasColumnName("megyeId");
            entity.Property(e => e.TelepNev)
                .HasMaxLength(100)
                .HasColumnName("telepNev");

            entity.HasOne(d => d.Megye).WithMany(p => p.Telepuleseks)
                .HasForeignKey(d => d.MegyeId)
                .HasConstraintName("telepulesek_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
