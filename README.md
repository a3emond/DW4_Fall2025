# DW4 FALL 2025

## Repository structure

- [Templates_ASP.NET_.NET_Framework_4.7.2/](./Templates_ASP.NET_.NET_Framework_4.7.2/)  
  Pre-generated ASP.NET (.NET Framework 4.7.2) template projects:
  * [Empty](./Templates_ASP.NET_.NET_Framework_4.7.2/Empty_Template_ASP.NET_.NET_Framework_4.7.2/)
  * [Web API](./Templates_ASP.NET_.NET_Framework_4.7.2/WebAPI_Template_ASP.NET_.NET_Framework_4.7.2/)
  * [MVC](./Templates_ASP.NET_.NET_Framework_4.7.2/MVC_Template_ASP.NET_.NET_Framework_4.7.2/)
  * [Web Forms](./Templates_ASP.NET_.NET_Framework_4.7.2/WebForms_Template_ASP.NET_.NET_Framework_4.7.2/)
  * [SPA](./Templates_ASP.NET_.NET_Framework_4.7.2/SPA_Template_ASP.NET_.NET_Framework_4.7.2/)

- [src/](./src/)  
  Project-specific code for the semester, including assignments, homeworks, and lab exercises.

- [libs/](./libs/)  
  Shared libraries reusable across projects (e.g., authentication, database access, utilities).

- [APIs/](./APIs/)  
  Modular APIs that build on top of the shared libraries. Each API is self-contained but can consume code from `libs/`.

- [init-sql/](./init-sql/)  
  SQL Server initialization scripts executed by Docker Compose to set up the development databases.

- [docker-compose.yml](./docker-compose.yml)  
  Docker Compose configuration for MSSQL with persistent volumes and initialization support.

---

## [Database setup (MSSQL + Docker Compose)](./docker-compose.yml)

A SQL Server 2022 Developer Edition instance is included using Docker Compose.
It runs on port **1433** with a persistent volume (`mssql_data`) and executes any initialization scripts from the [`init-sql/`](./init-sql/) directory.

### Usage

Start the database container:

```bash
docker-compose up -d
```

Check logs:

```bash
docker logs mssql
```

Stop and remove:

```bash
docker-compose down
```

Reset everything (re-run init scripts):

```bash
docker-compose down -v
docker-compose up -d
```

### Connection strings

**From host (Rider, Visual Studio, Azure Data Studio, etc.):**

```text
Server=localhost,1433;Database=main_db;User Id=sa;Password=Password123!;TrustServerCertificate=True;
```

**From host without targeting a specific DB (defaults to master):**

```text
Server=localhost,1433;User Id=sa;Password=Password123!;TrustServerCertificate=True;
```

**From another container on the `devnet` network:**

```text
Server=mssql,1433;Database=main_db;User Id=sa;Password=Password123!;TrustServerCertificate=True;
```

### Initialization scripts

Initialization files are located in [`init-sql/`](./init-sql/).

Example:

* `01-main-database.sql` → creates `main-db` if it does not exist and sets it as the current database.
* `02-specific-database.sql` → optional additional databases or tables per exercise.

---

