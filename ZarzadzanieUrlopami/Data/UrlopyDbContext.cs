using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZarzadzanieUrlopami.Models;

namespace ZarzadzanieUrlopami.Data;

public partial class UrlopyDbContext : DbContext
{
    public UrlopyDbContext()
    {
    }

    public UrlopyDbContext(DbContextOptions<UrlopyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DniRobocze> DniRoboczes { get; set; }

    public virtual DbSet<DostepneUrlopyRoczne> DostepneUrlopyRocznes { get; set; }

    public virtual DbSet<Pracownicy> Pracownicies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusUrlopu> StatusUrlopus { get; set; }

    public virtual DbSet<TypyStatusow> TypyStatusows { get; set; }

    public virtual DbSet<TypyUrlopow> TypyUrlopows { get; set; }

    public virtual DbSet<TypyZwolnien> TypyZwolniens { get; set; }

    public virtual DbSet<Urlopy> Urlopies { get; set; }

    public virtual DbSet<WystawcyZwolnien> WystawcyZwolniens { get; set; }

    public virtual DbSet<Zastepstwa> Zastepstwas { get; set; }

    public virtual DbSet<Zmiany> Zmianies { get; set; }

    public virtual DbSet<ZwolnieniaLekarskie> ZwolnieniaLekarskies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DniRobocze>(entity =>
        {
            entity.HasKey(e => e.IdDnia).HasName("PK__Dni_robo__9DC8EE78B8C63145");

            entity.ToTable("Dni_robocze");

            entity.HasIndex(e => e.DataDnia, "IX_DniRobocze_Dzien");

            entity.Property(e => e.IdDnia).HasColumnName("id_dnia");
            entity.Property(e => e.DataDnia).HasColumnName("data_dnia");
            entity.Property(e => e.IdKierownika).HasColumnName("id_kierownika");

            entity.HasOne(d => d.IdKierownikaNavigation).WithMany(p => p.DniRoboczes)
                .HasForeignKey(d => d.IdKierownika)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DniRobocze_Pracownicy");
        });

        modelBuilder.Entity<DostepneUrlopyRoczne>(entity =>
        {
            entity.HasKey(e => e.IdDostepnegoUrlopu).HasName("PK__Dostepne__6A75A46107D59F28");

            entity.ToTable("Dostepne_Urlopy_Roczne");

            entity.Property(e => e.IdDostepnegoUrlopu).HasColumnName("id_dostepnego_urlopu");
            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");
            entity.Property(e => e.IdTypuUrlopu).HasColumnName("id_typu_urlopu");
            entity.Property(e => e.Ilosc).HasColumnName("ilosc");
            entity.Property(e => e.Rok)
                .HasDefaultValueSql("(datepart(year,getdate()))")
                .HasColumnName("rok");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.DostepneUrlopyRocznes)
                .HasForeignKey(d => d.IdPracownika)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DostepneUrlopy_Pracownicy");

            entity.HasOne(d => d.IdTypuUrlopuNavigation).WithMany(p => p.DostepneUrlopyRocznes)
                .HasForeignKey(d => d.IdTypuUrlopu)
                .HasConstraintName("FK_DostepneUrlopy_TypyUrlopow");
        });

        modelBuilder.Entity<Pracownicy>(entity =>
        {
            entity.HasKey(e => e.IdPracownika).HasName("PK__Pracowni__5D8E25F273BAF34A");

            entity.ToTable("Pracownicy", tb => tb.HasTrigger("Dodaj_DostepneUrlopyRoczne_GdyDodaszPracownika"));

            entity.HasIndex(e => e.Nazwisko, "IX_Pracownicy_Nazwisko");

            entity.HasIndex(e => e.Mail, "UQ_Pracownicy_Mail").IsUnique();

            entity.HasIndex(e => e.Telefon, "UQ_Pracownicy_Telefon").IsUnique();

            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");
            entity.Property(e => e.Adres)
                .HasMaxLength(30)
                .HasColumnName("adres");
            entity.Property(e => e.DataRozpoczecia).HasColumnName("data_rozpoczecia");
            entity.Property(e => e.DzieciPonizej14)
                .HasDefaultValue(0)
                .HasColumnName("dzieci_ponizej_14");
            entity.Property(e => e.Haslo)
                .HasMaxLength(30)
                .HasColumnName("haslo");
            entity.Property(e => e.IdRoli).HasColumnName("id_roli");
            entity.Property(e => e.Imie)
                .HasMaxLength(30)
                .HasColumnName("imie");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .HasColumnName("mail");
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(30)
                .HasColumnName("nazwisko");
            entity.Property(e => e.Plec)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("plec");
            entity.Property(e => e.StazPracyWDniach)
                .HasDefaultValue(0)
                .HasColumnName("staz_pracy_w_dniach");
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .HasColumnName("telefon");
            entity.Property(e => e.Wiek).HasColumnName("wiek");

            entity.HasOne(d => d.IdRoliNavigation).WithMany(p => p.Pracownicies)
                .HasForeignKey(d => d.IdRoli)
                .HasConstraintName("FK_Pracownicy_Role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRoli).HasName("PK__Role__3D4847E12850301E");

            entity.ToTable("Role");

            entity.Property(e => e.IdRoli).HasColumnName("id_roli");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .HasColumnName("nazwa");
        });

        modelBuilder.Entity<StatusUrlopu>(entity =>
        {
            entity.HasKey(e => e.IdStatusu).HasName("PK__Status_U__761651A744506549");

            entity.ToTable("Status_Urlopu");

            entity.Property(e => e.IdStatusu).HasColumnName("id_statusu");
            entity.Property(e => e.IdTypuStatusu).HasColumnName("id_typu_statusu");
            entity.Property(e => e.Wyjaśnienie)
                .HasMaxLength(100)
                .HasColumnName("wyjaśnienie");

            entity.HasOne(d => d.IdTypuStatusuNavigation).WithMany(p => p.StatusUrlopus)
                .HasForeignKey(d => d.IdTypuStatusu)
                .HasConstraintName("FK_StatusUrlopu_TypyStatusow");
        });

        modelBuilder.Entity<TypyStatusow>(entity =>
        {
            entity.HasKey(e => e.IdTypuStat).HasName("PK__Typy_Sta__5301E33B60099E9B");

            entity.ToTable("Typy_Statusow");

            entity.HasIndex(e => e.Nazwa, "IX_TypyStatusow_Nazwa");

            entity.HasIndex(e => e.Nazwa, "UQ_TypyStatusow_Nazwa").IsUnique();

            entity.Property(e => e.IdTypuStat).HasColumnName("id_typu_stat");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .HasColumnName("nazwa");
            entity.Property(e => e.Opis)
                .HasMaxLength(100)
                .HasColumnName("opis");
        });

        modelBuilder.Entity<TypyUrlopow>(entity =>
        {
            entity.HasKey(e => e.IdTypuUrlopu).HasName("PK__Typy_Url__EC7B687853AF0A3D");

            entity.ToTable("Typy_Urlopow");

            entity.HasIndex(e => e.Nazwa, "IX_TypyUrlopow_Nazwa");

            entity.HasIndex(e => e.Nazwa, "UQ_TypyUrlopow_Nazwa").IsUnique();

            entity.Property(e => e.IdTypuUrlopu).HasColumnName("id_typu_urlopu");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .HasColumnName("nazwa");
        });

        modelBuilder.Entity<TypyZwolnien>(entity =>
        {
            entity.HasKey(e => e.IdTypuZwolnienia).HasName("PK__Typy_Zwo__57ED5384C9C8D8FC");

            entity.ToTable("Typy_Zwolnien");

            entity.HasIndex(e => e.Nazwa, "IX_TypyZwolnien_Nazwa");

            entity.HasIndex(e => e.Nazwa, "UQ_Typy_Zwolnien_Nazwa").IsUnique();

            entity.Property(e => e.IdTypuZwolnienia).HasColumnName("id_typu_zwolnienia");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .HasColumnName("nazwa");
            entity.Property(e => e.Opis)
                .HasMaxLength(100)
                .HasColumnName("opis");
        });

        modelBuilder.Entity<Urlopy>(entity =>
        {
            entity.HasKey(e => e.IdUrlopu).HasName("PK__Urlopy__DE8DBE5775C813AF");

            entity.ToTable("Urlopy", tb => tb.HasTrigger("UsunDni_PrzyOtrzymaniuUrlopow"));

            entity.Property(e => e.IdUrlopu).HasColumnName("id_urlopu");
            entity.Property(e => e.DataKon).HasColumnName("data_kon");
            entity.Property(e => e.DataPocz).HasColumnName("data_pocz");
            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");
            entity.Property(e => e.IdStatusu).HasColumnName("id_statusu");
            entity.Property(e => e.IdTypuUrlopu).HasColumnName("id_typu_urlopu");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.Urlopies)
                .HasForeignKey(d => d.IdPracownika)
                .HasConstraintName("FK_Urlopy_Pracownicy");

            entity.HasOne(d => d.IdStatusuNavigation).WithMany(p => p.Urlopies)
                .HasForeignKey(d => d.IdStatusu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Urlopy_Status_Urlopu");

            entity.HasOne(d => d.IdTypuUrlopuNavigation).WithMany(p => p.Urlopies)
                .HasForeignKey(d => d.IdTypuUrlopu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Urlopy_TypyUrlopow");
        });

        modelBuilder.Entity<WystawcyZwolnien>(entity =>
        {
            entity.HasKey(e => e.IdWystawcy).HasName("PK__Wystawcy__27A7F6F0F0686277");

            entity.ToTable("Wystawcy_Zwolnien");

            entity.HasIndex(e => e.Nazwa, "IX_WystawcyZwolnien_Nazwa");

            entity.HasIndex(e => e.Nazwa, "UQ_WystawcyZwolnien_Nazwa").IsUnique();

            entity.Property(e => e.IdWystawcy).HasColumnName("id_wystawcy");
            entity.Property(e => e.Adres)
                .HasMaxLength(50)
                .HasColumnName("adres");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(30)
                .HasColumnName("nazwa");
        });

        modelBuilder.Entity<Zastepstwa>(entity =>
        {
            entity.HasKey(e => e.IdZastepstwa).HasName("PK__Zastepst__6C01E0AABCA220AD");

            entity.ToTable("Zastepstwa");

            entity.Property(e => e.IdZastepstwa).HasColumnName("id_zastepstwa");
            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");
            entity.Property(e => e.IdZmiany).HasColumnName("id_zmiany");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.Zastepstwas)
                .HasForeignKey(d => d.IdPracownika)
                .HasConstraintName("FK_Zastepstwa_Pracownicy");

            entity.HasOne(d => d.IdZmianyNavigation).WithMany(p => p.Zastepstwas)
                .HasForeignKey(d => d.IdZmiany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zastepstwa_Zmiany");
        });

        modelBuilder.Entity<Zmiany>(entity =>
        {
            entity.HasKey(e => e.IdZmiany).HasName("PK__Zmiany__9B21969B31FEA3D7");

            entity.ToTable("Zmiany");

            entity.Property(e => e.IdZmiany).HasColumnName("id_zmiany");
            entity.Property(e => e.GodzRozp).HasColumnName("godz_rozp");
            entity.Property(e => e.GodzZakon).HasColumnName("godz_zakon");
            entity.Property(e => e.IdDnia).HasColumnName("id_dnia");
            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");

            entity.HasOne(d => d.IdDniaNavigation).WithMany(p => p.Zmianies)
                .HasForeignKey(d => d.IdDnia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zmiany_DniRobocze");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.Zmianies)
                .HasForeignKey(d => d.IdPracownika)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zmiany_Pracownicy");
        });

        modelBuilder.Entity<ZwolnieniaLekarskie>(entity =>
        {
            entity.HasKey(e => e.IdZwolnienia).HasName("PK__Zwolnien__290A808EC69EAA28");

            entity.ToTable("Zwolnienia_Lekarskie");

            entity.Property(e => e.IdZwolnienia).HasColumnName("id_zwolnienia");
            entity.Property(e => e.DataKon).HasColumnName("data_kon");
            entity.Property(e => e.DataPocz).HasColumnName("data_pocz");
            entity.Property(e => e.IdPracownika).HasColumnName("id_pracownika");
            entity.Property(e => e.IdTypu).HasColumnName("id_typu");
            entity.Property(e => e.IdWystawcy).HasColumnName("id_wystawcy");
            entity.Property(e => e.Opis)
                .HasMaxLength(100)
                .HasColumnName("opis");

            entity.HasOne(d => d.IdPracownikaNavigation).WithMany(p => p.ZwolnieniaLekarskies)
                .HasForeignKey(d => d.IdPracownika)
                .HasConstraintName("FK_ZwolnieniaLekarskie_Pracownicy");

            entity.HasOne(d => d.IdTypuNavigation).WithMany(p => p.ZwolnieniaLekarskies)
                .HasForeignKey(d => d.IdTypu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZwolnieniaLekarskie_TypyZwolnien");

            entity.HasOne(d => d.IdWystawcyNavigation).WithMany(p => p.ZwolnieniaLekarskies)
                .HasForeignKey(d => d.IdWystawcy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZwolnieniaLekarskie_WystawcyZwolnien");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
