# SolutionOrders (MBA2026Day)

Modular monolith (.NET 10): bounded contexts under `src/Modules/<Feature>/` with **Domain**, **Core**, **Application**, **Persistence**, **Infrastructure**. Shared composition lives in `SolutionOrders.API`; SQL persistence is centralized in `src/SolutionOrders.Sql` (`ApplicationDbContext`, EF configurations per module, migrations under `Migrations/`).

## Running locally

- **SQL Server**: connection string key `ConnectionStrings:ApplicationDbContext` (see `SolutionOrders.API/appsettings*.json`).
- **MongoDB**: read projections — `ConnectionStrings:MongoDb` (default `mongodb://127.0.0.1:27017`) and `MongoDb:DatabaseName` (default `SolutionOrdersRead`). Writes stay on SQL; Mongo is refreshed after each successful `SaveChanges` and once at startup (`MongoProjectionBackfillHostedService`).
- **Docker**: `docker compose -f docker-compose-api.yml up --build` — brings up SQL Server, MongoDB, and the API (`5000` → container `8080`). Set env vars as in that file if you override defaults.

## EF Core migrations

`ApplicationDbContext` lives in `SolutionOrders.Sql`. Design-time factory: `SolutionOrders.Sql/ApplicationDbContextFactory.cs`.

Typical command (from repo root):

```bash
dotnet ef migrations add <Name> --project src/SolutionOrders.Sql/SolutionOrders.Sql.csproj --startup-project SolutionOrders.API/SolutionOrders.API.csproj --output-dir Migrations --context ApplicationDbContext
```

If `dotnet ef` reports assembly load issues, rebuild the solution, ensure the global tool matches the SDK (`dotnet tool update --global dotnet-ef`), and retry.

### Windows / DLL blocking (`FileLoadException` 0x800711C7)

If verbose output shows **„Zasady kontroli aplikacji zablokowały ten plik”**, run **`dotnet ef`** inside Linux SDK (mount your repo):

```bash
docker run --rm -v "c:/git/MBA2026Day:/src" -w /src mcr.microsoft.com/dotnet/sdk:10.0 bash -c 'dotnet tool install -g dotnet-ef --version 10.0.7 && export PATH="$PATH:/root/.dotnet/tools" && dotnet restore SolutionOrders.slnx && dotnet ef migrations add <Name> --project src/SolutionOrders.Sql/SolutionOrders.Sql.csproj --startup-project SolutionOrders.API/SolutionOrders.API.csproj --output-dir Migrations --context ApplicationDbContext'
```

Adjust the host path to your clone. Alternatives: **WSL**, another machine, or relaxing policy for the dev folder.

### Development vs migrations

The repo includes **`InitialCreate`**; **`Migrate()`** runs at startup when migrations are embedded. If you remove migrations locally, `Development` may fall back to **`EnsureCreated()`** (see `Program.cs`) — recreate the database after you add migrations again.

**Production / staging** requires migrations in the Sql assembly.

**Before production deploy**, apply pending migrations (`dotnet ef database update ...` with the same `--project` / `--startup-project`).

## Solution layout (high level)

| Area | Role |
|------|------|
| `SolutionOrders.API` | HTTP API, DI composition root, MediatR + Mapster |
| `SolutionOrders.Sql` | Composed `DbContext`, Mongo sync/backfill, EF migrations |
| `SolutionOrders.Core` | Cross-cutting contracts (`IUnitOfWork`) |
| `src/Modules/*` | Feature slices; query reads use Mongo `I*ReadRepository` where registered |
