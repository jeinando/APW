APW Inventory Management System

A decoupled full-stack solution built with ASP.NET Core 8, featuring a RESTful API and an MVC frontend integrated with a containerized SQL Server.
Key Layers

    APW.API: .NET 8 service using EF Core with data projections to serve optimized, flat JSON.

    APW.Web: MVC consumer utilizing Razor (functions, switch cases, and loops) for dynamic UI rendering.

    APW.Data: Shared library for DB Context and relational models.

Tech Stack

    Backend: .NET 8, Entity Framework Core.

    Database: SQL Server 2022 via Docker.

    Frontend: Razor, Bootstrap 5.

Quick Start

    Database: docker-compose up -d

    API: cd APW.API && dotnet run

    Web: cd APW.Web && dotnet run

Developed for the APW Course.
