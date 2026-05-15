# VerdantArc 🌿

A full-stack web application for exploring and managing a fictional nature reserve. Built with ASP.NET Core 8 MVC, VerdantArc allows visitors to discover hiking trails, browse documented wildlife and plant species, and check live weather conditions — while administrators can manage all content through a protected dashboard.

---

## Features

- **Trails** — browse and filter hiking routes by difficulty (Easy / Medium / Hard), with distance, estimated duration, and elevation gain for each trail
- **Species catalog** — explore fauna and flora with live observation data pulled from the iNaturalist API based on scientific name and location
- **Live weather** — real-time conditions and 7-day forecast from the Open-Meteo API, cached in the database to avoid unnecessary requests
- **Authentication** — register and log in with email and password; role-based access control separates regular users from administrators
- **Visitor reports** — authenticated users can submit trail condition reports with a 1–5 rating
- **Full-text search** — search across trails and species simultaneously using PostgreSQL case-insensitive matching
- **Admin dashboard** — protected area for managing trails and species with full CRUD operations
- **Automatic weather refresh** — a background service updates the weather cache every 25 minutes without any manual intervention

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 MVC + Razor Views |
| Language | C# 12 |
| Database | PostgreSQL 16 |
| ORM | Entity Framework Core 8 (Npgsql) |
| Authentication | ASP.NET Core Identity |
| Logging | Serilog (Console + rolling File sink) |
| HTTP Client | IHttpClientFactory |
| Frontend | Razor Views + Vanilla JavaScript |
| Containerization | Docker + Docker Compose |
| External APIs | Open-Meteo (weather), iNaturalist (species observations) |

---

## Architecture

The solution follows a classic **N-Tier architecture** split across four projects:

```
VerdantArc.sln
├── VerdantArc.Web           # Controllers, Razor Views, Middlewares, REST API
├── VerdantArc.Application   # Services, DTOs, Interfaces
├── VerdantArc.Infrastructure # DbContext, EF Migrations, Repositories, Seed Data
└── VerdantArc.Domain        # Entities (Trail, Species, WeatherCache, etc.)
```

All dependencies are registered via the built-in ASP.NET Core DI container following the Repository → Service → Controller pattern.

---

## Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Run with Docker

```bash
git clone https://github.com/your-username/VerdantArc.git
cd VerdantArc
docker-compose up --build
```

The application will be available at **http://localhost:8080**

The database schema and seed data are created automatically on first startup.

### Default Admin Account

| Field | Value |
|---|---|
| Email | admin@verdantarc.ro |
| Password | Admin123! |

---

## External APIs

**Open-Meteo** — free weather API, no key required. Fetches current conditions and a 7-day forecast for the reserve's coordinates. Results are cached in PostgreSQL with a 30-minute TTL.

**iNaturalist** — free biodiversity API. Used on species detail pages to display the latest real-world observations matching the species' scientific name within Romania.

---

## Environment Variables

Configured via `appsettings.json` or Docker environment variables:

```
ConnectionStrings__DefaultConnection   PostgreSQL connection string
WeatherApi__Latitude                   Reserve latitude (default: 45.5)
WeatherApi__Longitude                  Reserve longitude (default: 25.0)
WeatherApi__CacheDurationMinutes       Weather cache TTL (default: 30)
```

---

## Academic Context

Developed as a semester project for the **Web Application Development** course. The application covers the following requirements: REST API, EF Core with migrations and relationships, ASP.NET Core Identity, exception handling middleware, Serilog logging, DTOs + Repository + Service + DI pattern, protected routes, client and server-side form validation, external API integration from JavaScript, and a minimum of 5 Razor pages.
