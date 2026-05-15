using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VerdantArc.Domain.Entities;
using VerdantArc.Domain.Enums;

namespace VerdantArc.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Trail> Trails { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<TrailSighting> TrailSightings { get; set; }
    public DbSet<WeatherCache> WeatherCaches { get; set; }
    public DbSet<VisitorReport> VisitorReports { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships
        builder.Entity<TrailSighting>()
            .HasOne(ts => ts.Trail)
            .WithMany(t => t.TrailSightings)
            .HasForeignKey(ts => ts.TrailId);

        builder.Entity<TrailSighting>()
            .HasOne(ts => ts.Species)
            .WithMany(s => s.TrailSightings)
            .HasForeignKey(ts => ts.SpeciesId);

        builder.Entity<TrailSighting>()
            .HasOne(ts => ts.ReportedByUser)
            .WithMany(u => u.TrailSightings)
            .HasForeignKey(ts => ts.ReportedByUserId);

        builder.Entity<VisitorReport>()
            .HasOne(vr => vr.Trail)
            .WithMany(t => t.VisitorReports)
            .HasForeignKey(vr => vr.TrailId);

        builder.Entity<VisitorReport>()
            .HasOne(vr => vr.SubmittedByUser)
            .WithMany(u => u.VisitorReports)
            .HasForeignKey(vr => vr.SubmittedByUserId);

        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        // Roles
        var adminRoleId = "1";
        var userRoleId = "2";
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
        );

        // Users
        var adminId = "1";
        var userId = "2";
        var hasher = new PasswordHasher<ApplicationUser>();

        var admin = new ApplicationUser
        {
            Id = adminId,
            UserName = "admin@verdantarc.ro",
            NormalizedUserName = "ADMIN@VERDANTARC.RO",
            Email = "admin@verdantarc.ro",
            NormalizedEmail = "ADMIN@VERDANTARC.RO",
            EmailConfirmed = true,
            FullName = "Administrator System",
            RegisteredAt = DateTime.UtcNow
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

        var user = new ApplicationUser
        {
            Id = userId,
            UserName = "user@verdantarc.ro",
            NormalizedUserName = "USER@VERDANTARC.RO",
            Email = "user@verdantarc.ro",
            NormalizedEmail = "USER@VERDANTARC.RO",
            EmailConfirmed = true,
            FullName = "Vizitator Frecvent",
            RegisteredAt = DateTime.UtcNow
        };
        user.PasswordHash = hasher.HashPassword(user, "User123!");

        builder.Entity<ApplicationUser>().HasData(admin, user);

        // User Roles
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminId },
            new IdentityUserRole<string> { RoleId = userRoleId, UserId = userId }
        );

        // Trails
        builder.Entity<Trail>().HasData(
            new Trail { Id = 1, Name = "Traseul Lacului", Description = "Un traseu ușor în jurul lacului principal.", DifficultyLevel = DifficultyLevel.Easy, DistanceKm = 2.5m, EstimatedDurationMinutes = 45, ElevationGainM = 20, ImageUrl = "https://images.unsplash.com/photo-1542385151-efd9000785a0?q=80&w=2000" },
            new Trail { Id = 2, Name = "Poteca Urșilor", Description = "Traseu mediu prin pădure deasă.", DifficultyLevel = DifficultyLevel.Medium, DistanceKm = 8.0m, EstimatedDurationMinutes = 180, ElevationGainM = 350, ImageUrl = "https://images.unsplash.com/photo-1448375240586-882707db888b?q=80&w=2000" },
            new Trail { Id = 3, Name = "Vârful Pietros", Description = "Traseu dificil până în vârful rezervației.", DifficultyLevel = DifficultyLevel.Hard, DistanceKm = 15.0m, EstimatedDurationMinutes = 420, ElevationGainM = 1200, ImageUrl = "https://images.unsplash.com/photo-1519681393784-d120267933ba?q=80&w=2000" }
        );

        // Species
        builder.Entity<Species>().HasData(
            new Species { Id = 1, CommonName = "Urs Brun", ScientificName = "Ursus arctos", Category = Category.Mammal, ConservationStatus = ConservationStatus.VU, Description = "Cel mai mare mamifer din rezervație." },
            new Species { Id = 2, CommonName = "Cerb", ScientificName = "Cervus elaphus", Category = Category.Mammal, ConservationStatus = ConservationStatus.LC, Description = "Animal erbivor masiv." },
            new Species { Id = 3, CommonName = "Acvila de munte", ScientificName = "Aquila chrysaetos", Category = Category.Bird, ConservationStatus = ConservationStatus.LC, Description = "Pasăre de pradă impresionantă." },
            new Species { Id = 4, CommonName = "Vipera cu corn", ScientificName = "Vipera ammodytes", Category = Category.Reptile, ConservationStatus = ConservationStatus.NT, Description = "Șarpe veninos." },
            new Species { Id = 5, CommonName = "Floare de colț", ScientificName = "Leontopodium nivale", Category = Category.Plant, ConservationStatus = ConservationStatus.VU, IsEndemic = false, Description = "Floare rară montană." },
            new Species { Id = 6, CommonName = "Păstrăv de munte", ScientificName = "Salmo trutta fario", Category = Category.Fish, ConservationStatus = ConservationStatus.LC, Description = "Pește din apele repezi." },
            new Species { Id = 7, CommonName = "Croitorul fagului", ScientificName = "Rosalia alpina", Category = Category.Insect, ConservationStatus = ConservationStatus.VU, Description = "Gândac protejat." },
            new Species { Id = 8, CommonName = "Lup", ScientificName = "Canis lupus", Category = Category.Mammal, ConservationStatus = ConservationStatus.LC, Description = "Prădător în haită." },
            new Species { Id = 9, CommonName = "Râs", ScientificName = "Lynx lynx", Category = Category.Mammal, ConservationStatus = ConservationStatus.LC, Description = "Felină sălbatică." },
            new Species { Id = 10, CommonName = "Ghiocel", ScientificName = "Galanthus nivalis", Category = Category.Plant, ConservationStatus = ConservationStatus.LC, Description = "Floare de primăvară." }
        );
    }
}
