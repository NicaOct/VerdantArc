using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VerdantArc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScientificName = table.Column<string>(type: "text", nullable: false),
                    CommonName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ConservationStatus = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    InatNaturalistId = table.Column<string>(type: "text", nullable: true),
                    IsEndemic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "integer", nullable: false),
                    DistanceKm = table.Column<decimal>(type: "numeric", nullable: false),
                    EstimatedDurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    ElevationGainM = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GpxFileUrl = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherCaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FetchedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TemperatureCelsius = table.Column<decimal>(type: "numeric", nullable: false),
                    WindSpeedKmh = table.Column<decimal>(type: "numeric", nullable: false),
                    HumidityPercent = table.Column<int>(type: "integer", nullable: false),
                    WeatherDescription = table.Column<string>(type: "text", nullable: false),
                    ForecastJson = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherCaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrailSightings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrailId = table.Column<int>(type: "integer", nullable: false),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    ObservedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ReportedByUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailSightings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrailSightings_AspNetUsers_ReportedByUserId",
                        column: x => x.ReportedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrailSightings_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrailSightings_Trails_TrailId",
                        column: x => x.TrailId,
                        principalTable: "Trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitorReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrailId = table.Column<int>(type: "integer", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConditionRating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    SubmittedByUserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorReports_AspNetUsers_SubmittedByUserId",
                        column: x => x.SubmittedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorReports_Trails_TrailId",
                        column: x => x.TrailId,
                        principalTable: "Trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegisteredAt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "e9c2028c-b872-4cd3-86cc-46fe54afeffc", "admin@verdantarc.ro", true, "System Administrator", false, null, "ADMIN@VERDANTARC.RO", "ADMIN@VERDANTARC.RO", "AQAAAAIAAYagAAAAEJxQT7myVr9BNzxO8/4Flg/1nAEMe5ya1cvE/J2uNYIMv8rX/0HaExMPou5pTl5nAA==", null, false, new DateTime(2026, 5, 15, 21, 37, 19, 983, DateTimeKind.Utc).AddTicks(1198), "18317855-7dcd-42b1-8d00-4dc0f7ff98c4", false, "admin@verdantarc.ro" },
                    { "2", 0, "387db489-776f-44f4-b765-cf7dd9848457", "user@verdantarc.ro", true, "Frequent Visitor", false, null, "USER@VERDANTARC.RO", "USER@VERDANTARC.RO", "AQAAAAIAAYagAAAAEMrFieH244203TTvVyIpgJDdCPoBF0yiUmXCLDcRnlJdMOcE6YvVqj7ylOkJ3hmwfg==", null, false, new DateTime(2026, 5, 15, 21, 37, 20, 68, DateTimeKind.Utc).AddTicks(386), "047cdf6c-5773-4e19-8414-c5334d255ac0", false, "user@verdantarc.ro" }
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Category", "CommonName", "ConservationStatus", "CreatedAt", "Description", "ImageUrl", "InatNaturalistId", "IsEndemic", "ScientificName" },
                values: new object[,]
                {
                    { 1, 0, "Brown Bear", 2, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8989), "The largest mammal in the reserve.", null, null, false, "Ursus arctos" },
                    { 2, 0, "Red Deer", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9008), "A large herbivorous animal.", null, null, false, "Cervus elaphus" },
                    { 3, 1, "Golden Eagle", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9013), "An impressive bird of prey.", null, null, false, "Aquila chrysaetos" },
                    { 4, 2, "Horned Viper", 1, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9018), "A venomous snake.", null, null, false, "Vipera ammodytes" },
                    { 5, 3, "Edelweiss", 2, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9023), "A rare alpine flower.", null, null, false, "Leontopodium nivale" },
                    { 6, 4, "Mountain Trout", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9040), "A fish found in fast-flowing mountain streams.", null, null, false, "Salmo trutta fario" },
                    { 7, 5, "Alpine Longhorn Beetle", 2, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9042), "A protected beetle species.", null, null, false, "Rosalia alpina" },
                    { 8, 0, "Grey Wolf", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9046), "A social pack predator.", null, null, false, "Canis lupus" },
                    { 9, 0, "Eurasian Lynx", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9050), "A wild feline predator.", null, null, false, "Lynx lynx" },
                    { 10, 3, "Snowdrop", 0, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(9055), "A spring-blooming flower.", null, null, false, "Galanthus nivalis" }
                });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "CreatedAt", "Description", "DifficultyLevel", "DistanceKm", "ElevationGainM", "EstimatedDurationMinutes", "GpxFileUrl", "ImageUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8750), "An easy trail around the main lake.", 0, 2.5m, 20, 45, null, "https://images.unsplash.com/photo-1542385151-efd9000785a0?q=80&w=2000", true, "The Lake Trail", new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8768) },
                    { 2, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8820), "A moderate trail through dense forest.", 1, 8.0m, 350, 180, null, "https://images.unsplash.com/photo-1448375240586-882707db888b?q=80&w=2000", true, "Bear's Path", new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8822) },
                    { 3, new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8826), "A challenging trail up to the highest point of the reserve.", 2, 15.0m, 1200, 420, null, "https://images.unsplash.com/photo-1519681393784-d120267933ba?q=80&w=2000", true, "Pietros Peak", new DateTime(2026, 5, 15, 21, 37, 20, 154, DateTimeKind.Utc).AddTicks(8828) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrailSightings_ReportedByUserId",
                table: "TrailSightings",
                column: "ReportedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrailSightings_SpeciesId",
                table: "TrailSightings",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_TrailSightings_TrailId",
                table: "TrailSightings",
                column: "TrailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorReports_SubmittedByUserId",
                table: "VisitorReports",
                column: "SubmittedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorReports_TrailId",
                table: "VisitorReports",
                column: "TrailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TrailSightings");

            migrationBuilder.DropTable(
                name: "VisitorReports");

            migrationBuilder.DropTable(
                name: "WeatherCaches");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Trails");
        }
    }
}
