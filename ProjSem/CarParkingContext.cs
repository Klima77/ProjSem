using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjSem;

public partial class CarParkingContext : DbContext
{
    public CarParkingContext()
    {
    }

    public CarParkingContext(DbContextOptions<CarParkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bilety> Bileties { get; set; }

    public virtual DbSet<Historium> Historia { get; set; }

    public virtual DbSet<Pojazdy> Pojazdies { get; set; }

    public virtual DbSet<RodzajeBiletow> RodzajeBiletows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CA584AP;Initial Catalog=carParking;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bilety>(entity =>
        {
            entity.HasKey(e => e.IdBiletu).HasName("PK__Bilety__0C560F2986894483");

            entity.ToTable("Bilety");

            entity.Property(e => e.IdBiletu).HasColumnName("ID_Biletu");
            entity.Property(e => e.DataWaznosci)
                .HasColumnType("datetime")
                .HasColumnName("Data_Waznosci");
            entity.Property(e => e.DataZakupu)
                .HasColumnType("datetime")
                .HasColumnName("Data_Zakupu");
            entity.Property(e => e.NrRejestracyjny)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Nr_Rejestracyjny");
            entity.Property(e => e.RodzajBiletu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Rodzaj_Biletu");

            entity.HasOne(d => d.NrRejestracyjnyNavigation).WithMany(p => p.Bileties)
                .HasForeignKey(d => d.NrRejestracyjny)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bilety__Nr_Rejes__286302EC");

            entity.HasOne(d => d.RodzajBiletuNavigation).WithMany(p => p.Bileties)
                .HasForeignKey(d => d.RodzajBiletu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bilety__Rodzaj_B__29572725");
        });

        modelBuilder.Entity<Historium>(entity =>
        {
            entity.HasKey(e => e.IdHistorii).HasName("PK__Historia__369F15B8138F3DC3");

            entity.Property(e => e.IdHistorii).HasColumnName("ID_Historii");
            entity.Property(e => e.DataRejestracji)
                .HasColumnType("datetime")
                .HasColumnName("Data_Rejestracji");
            entity.Property(e => e.NrRejestracyjny)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Nr_Rejestracyjny");

            entity.HasOne(d => d.NrRejestracyjnyNavigation).WithMany(p => p.Historia)
                .HasForeignKey(d => d.NrRejestracyjny)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historia__Nr_Rej__2C3393D0");
        });

        modelBuilder.Entity<Pojazdy>(entity =>
        {
            entity.HasKey(e => e.NrRejestracyjny).HasName("PK__Pojazdy__370BB17D64A5B8EC");

            entity.ToTable("Pojazdy");

            entity.Property(e => e.NrRejestracyjny)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Nr_Rejestracyjny");
        });

        modelBuilder.Entity<RodzajeBiletow>(entity =>
        {
            entity.HasKey(e => e.Nazwa).HasName("PK__Rodzaje___602223FE233704B7");

            entity.ToTable("Rodzaje_Biletow");

            entity.Property(e => e.Nazwa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CzasParkowania).HasColumnName("Czas_Parkowania");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
