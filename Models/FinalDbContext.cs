using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Models;

public partial class FinalDbContext : DbContext
{
    public FinalDbContext()
    {
    }

    public FinalDbContext(DbContextOptions<FinalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<LoginHistory> LoginHistories { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<SocailType> SocailTypes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffType> StaffTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Utilizer> Utilizers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=FinalDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.ToTable("Bill");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Company)
                .HasConstraintName("FK_Bill_Company");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("FK_Bill_Staff");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Ein)
                .HasMaxLength(50)
                .HasColumnName("EIN");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.PasNumber).HasMaxLength(15);
            entity.Property(e => e.PasSeries).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.SocialSecNumber).HasMaxLength(50);

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Company)
                .HasConstraintName("FK_Client_Company");

            entity.HasOne(d => d.SocailTypeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.SocailType)
                .HasConstraintName("FK_Client_SocailType");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Bic)
                .HasMaxLength(25)
                .HasColumnName("BIC");
            entity.Property(e => e.Ip)
                .HasMaxLength(15)
                .HasColumnName("IP");
            entity.Property(e => e.Itn)
                .HasMaxLength(25)
                .HasColumnName("ITN");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PaymentAccount).HasMaxLength(25);

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("FK_Company_Country");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LoginHistory>(entity =>
        {
            entity.ToTable("LoginHistory");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(15)
                .HasColumnName("IP");
            entity.Property(e => e.Login).HasMaxLength(50);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Text).HasMaxLength(4000);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Client");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Status");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderService_1");

            entity.ToTable("OrderService");

            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderService_Order");

            entity.HasOne(d => d.ServiceNavigation).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.Service)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderService_Service");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("FK_OrderService_Staff");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_OrderService_Status");

            entity.HasOne(d => d.UtilizerNavigation).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.Utilizer)
                .HasConstraintName("FK_OrderService_Utilizer");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.OrderServiceId);

            entity.Property(e => e.OrderServiceId).ValueGeneratedNever();
            entity.Property(e => e.Result1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Result");

            entity.HasOne(d => d.OrderService).WithOne(p => p.Result)
                .HasForeignKey<Result>(d => d.OrderServiceId)
                .HasConstraintName("FK_Results_OrderService");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Service");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.AverageDeviation).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AverageResult).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<SocailType>(entity =>
        {
            entity.ToTable("SocailType");

            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(25);
            entity.Property(e => e.Lastenter).HasColumnType("date");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(25);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_Staff_StaffType");

            entity.HasMany(d => d.Services).WithMany(p => p.Staff)
                .UsingEntity<Dictionary<string, object>>(
                    "StaffService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("Service")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StaffService_Service"),
                    l => l.HasOne<Staff>().WithMany()
                        .HasForeignKey("Staff")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StaffService_Staff"),
                    j =>
                    {
                        j.HasKey("Staff", "Service");
                        j.ToTable("StaffService");
                    });
        });

        modelBuilder.Entity<StaffType>(entity =>
        {
            entity.ToTable("StaffType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Utilizer>(entity =>
        {
            entity.ToTable("Utilizer");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            entity.HasMany(d => d.Services).WithMany(p => p.Utilizers)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilizerService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UtilizerServices_Service"),
                    l => l.HasOne<Utilizer>().WithMany()
                        .HasForeignKey("UtilizerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UtilizerServices_Utilizer"),
                    j =>
                    {
                        j.HasKey("UtilizerId", "ServiceId");
                        j.ToTable("UtilizerServices");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
