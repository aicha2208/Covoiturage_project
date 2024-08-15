using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GESTIONCOVOITURES.Models;

public partial class GestioncovoituragesContext : DbContext
{
    public GestioncovoituragesContext()
    {
    }

    public GestioncovoituragesContext(DbContextOptions<GestioncovoituragesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Trajet> Trajets { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<Vehicule> Vehicules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reservat__3213E83FCF4943A7");

            entity.ToTable("reservation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateReservation)
                .HasColumnType("datetime")
                .HasColumnName("date_reservation");
            entity.Property(e => e.PassagerId).HasColumnName("passager_id");
            entity.Property(e => e.TrajetId).HasColumnName("trajet_id");

            entity.HasOne(d => d.Passager).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PassagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__passa__412EB0B6");

            entity.HasOne(d => d.Trajet).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TrajetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__traje__403A8C7D");
        });

        modelBuilder.Entity<Trajet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trajet__3213E83FE99E95C3");

            entity.ToTable("trajet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Heure).HasColumnName("heure");
            entity.Property(e => e.Montant).HasColumnName("montant");
            entity.Property(e => e.Nombreplace).HasColumnName("nombreplace");
            entity.Property(e => e.Nombreplacereserve).HasColumnName("nombreplacereserve");
            entity.Property(e => e.VilleArrivee)
                .HasMaxLength(255)
                .HasColumnName("ville_arrivee");
            entity.Property(e => e.VilleDepart)
                .HasMaxLength(255)
                .HasColumnName("ville_depart");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__utilisat__3213E83F3037C654");

            entity.ToTable("utilisateurs");

            entity.HasIndex(e => e.Email, "UQ__utilisat__AB6E616479232EEE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Prenom)
                .HasMaxLength(255)
                .HasColumnName("prenom");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Vehicule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vehicule__3213E83F279252F1");

            entity.ToTable("vehicule");

            entity.HasIndex(e => e.Immatriculation, "UQ__vehicule__A7349C8FE3BBA82D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConducteurId).HasColumnName("conducteur_id");
            entity.Property(e => e.Immatriculation)
                .HasMaxLength(255)
                .HasColumnName("immatriculation");
            entity.Property(e => e.Marque)
                .HasMaxLength(255)
                .HasColumnName("marque");
            entity.Property(e => e.Modele)
                .HasMaxLength(255)
                .HasColumnName("modele");

            entity.HasOne(d => d.Conducteur).WithMany(p => p.Vehicules)
                .HasForeignKey(d => d.ConducteurId)
                .HasConstraintName("FK__vehicule__conduc__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
