using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PickMeApp.Models
{
    public partial class PickMeAppContext : DbContext
    {
        public PickMeAppContext()
        {
        }

        public PickMeAppContext(DbContextOptions<PickMeAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Feedbacks> Feedbacks { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<RoutePoints> RoutePoints { get; set; }
        public virtual DbSet<RouteRequest> RouteRequest { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local)\\SQLEXPRESS;Initial Catalog=PickMeApp;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.Property(e => e.CarId)
                    .HasColumnName("carId")
                    .ValueGeneratedNever();

                entity.Property(e => e.BabySeat).HasColumnName("babySeat");

                entity.Property(e => e.CarMake)
                    .HasColumnName("carMake")
                    .HasMaxLength(50);

                entity.Property(e => e.CarModel)
                    .HasColumnName("carModel")
                    .HasMaxLength(50);

                entity.Property(e => e.LicensePlate)
                    .HasColumnName("licensePlate")
                    .HasMaxLength(50);

                entity.Property(e => e.SeatsAvaliable)
                    .HasColumnName("seatsAvaliable")
                    .HasMaxLength(10);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Cars_Users");
            });

            modelBuilder.Entity<Feedbacks>(entity =>
            {
                entity.HasKey(e => e.FeedbackId);

                entity.Property(e => e.FeedbackId)
                    .HasColumnName("feedbackId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Feedback)
                    .HasColumnName("feedback")
                    .HasMaxLength(50);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserFrom).HasColumnName("userFrom");

                entity.Property(e => e.UserTo).HasColumnName("userTo");

                entity.HasOne(d => d.UserFromNavigation)
                    .WithMany(p => p.FeedbacksUserFromNavigation)
                    .HasForeignKey(d => d.UserFrom)
                    .HasConstraintName("FK_Feedbacks_Users");

                entity.HasOne(d => d.UserToNavigation)
                    .WithMany(p => p.FeedbacksUserToNavigation)
                    .HasForeignKey(d => d.UserTo)
                    .HasConstraintName("FK_Feedbacks_Users1");
            });

            modelBuilder.Entity<Points>(entity =>
            {
                entity.HasKey(e => e.PointId);

                entity.Property(e => e.PointId)
                    .HasColumnName("pointId")
                    .ValueGeneratedNever();

                entity.Property(e => e.PointName)
                    .HasColumnName("pointName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoutePoints>(entity =>
            {
                entity.HasKey(e => e.RoutePointId);

                entity.Property(e => e.RoutePointId)
                    .HasColumnName("routePointId")
                    .ValueGeneratedNever();

                entity.Property(e => e.PointId).HasColumnName("pointId");

                entity.Property(e => e.PointOrder).HasColumnName("pointOrder");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RouteId).HasColumnName("routeId");

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.RoutePoints)
                    .HasForeignKey(d => d.PointId)
                    .HasConstraintName("FK_RoutePoints_Points");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RoutePoints)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_RoutePoints_Routes");
            });

            modelBuilder.Entity<RouteRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.Property(e => e.RequestId)
                    .HasColumnName("requestId")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndPoint).HasColumnName("endPoint");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(50);

                entity.Property(e => e.NumberOfSeats).HasColumnName("numberOfSeats");

                entity.Property(e => e.RouteId).HasColumnName("routeId");

                entity.Property(e => e.StartPoint).HasColumnName("startPoint");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.EndPointNavigation)
                    .WithMany(p => p.RouteRequestEndPointNavigation)
                    .HasForeignKey(d => d.EndPoint)
                    .HasConstraintName("FK_RouteRequest_RoutePoints1");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteRequest)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_RouteRequest_Routes");

                entity.HasOne(d => d.StartPointNavigation)
                    .WithMany(p => p.RouteRequestStartPointNavigation)
                    .HasForeignKey(d => d.StartPoint)
                    .HasConstraintName("FK_RouteRequest_RoutePoints");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RouteRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RouteRequest_Users");
            });

            modelBuilder.Entity<Routes>(entity =>
            {
                entity.HasKey(e => e.RouteId);

                entity.Property(e => e.RouteId)
                    .HasColumnName("routeId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CarId).HasColumnName("carId");

                entity.Property(e => e.DateOfRoute)
                    .HasColumnName("dateOfRoute")
                    .HasColumnType("date");

                entity.Property(e => e.DriverId).HasColumnName("driverId");

                entity.Property(e => e.NumberOfSeats).HasColumnName("numberOfSeats");

                entity.Property(e => e.PreferencesAtRide)
                    .HasColumnName("preferencesAtRide")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_Routes_Cars");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Routes_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bio)
                    .HasColumnName("bio")
                    .HasMaxLength(50);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthDate")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);
            });
        }
    }
}
