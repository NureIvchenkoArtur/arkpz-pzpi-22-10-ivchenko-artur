using Microsoft.EntityFrameworkCore;
using AutosportTelemetry.Models;
using System.Text.Json;

namespace AutosportTelemetry.Config
{
    public class TelemetryDbContext : DbContext 
    {
        public TelemetryDbContext(DbContextOptions<TelemetryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Lap> Laps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Sensors)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);


            modelBuilder.Entity<Track>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Track>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tracks)
                .HasForeignKey(t => t.CreatedById);

            modelBuilder.Entity<Track>()
                .HasMany(t => t.Sensors)
                .WithOne(s => s.Track)
                .HasForeignKey(s => s.CurrentTrackId);

            modelBuilder.Entity<Track>().Property(t => t.StartFinishCoordinates)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    v => JsonSerializer.Deserialize<List<Coordinate>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                );

            modelBuilder.Entity<Track>().Property(t => t.PathCoordinates)
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    v => v == null ? null : JsonSerializer.Deserialize<List<Coordinate>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                );



            modelBuilder.Entity<Track>()
                .HasMany(t => t.Races)
                .WithOne(r => r.Track)
                .HasForeignKey(r => r.TrackId);

            modelBuilder.Entity<Sensor>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sensors)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Track)
                .WithMany(t => t.Sensors)
                .HasForeignKey(s => s.CurrentTrackId);

            modelBuilder.Entity<Race>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Race>()
                .HasOne(r => r.Track)
                .WithMany(t => t.Races)
                .HasForeignKey(r => r.TrackId);

            modelBuilder.Entity<Race>()
                .HasOne(r => r.User)
                .WithMany(u => u.Races)
                .HasForeignKey(r => r.CreatedById);

            modelBuilder.Entity<Lap>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Lap>()
                .HasOne(l => l.Race)
                .WithMany(r => r.Laps)
                .HasForeignKey(l => l.RaceId);

            modelBuilder.Entity<Lap>()
                .HasOne(l => l.Sensor)
                .WithMany(s => s.Laps)
                .HasForeignKey(l => l.SensorId);

            modelBuilder.Entity<Lap>().Property(l => l.BrakingPoints)
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    v => v == null ? null : JsonSerializer.Deserialize<List<Coordinate>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                );

            modelBuilder.Entity<Lap>().Property(l => l.AccelerationPoints)
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    v => v == null ? null : JsonSerializer.Deserialize<List<Coordinate>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                );
        }
    }
}
