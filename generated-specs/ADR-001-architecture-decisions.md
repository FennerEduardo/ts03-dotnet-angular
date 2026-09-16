# ADR 001: Architecture Decisions for Ejecución de Saga y Manejo de Comandos CQRS en .NET 8 con Angular

## Status
Accepted

## Context
Project requiring structured implementation matching Gherkin specification.

## Decisions
- **Architecture Style**: Monolith Architecture (MVC / Monolithic) (monolith)
- **Primary Backend Language**: csharp
- **Backend Framework**: aspnet-core (.NET 8.0 / 9.0 / 10.0)
- **ORM / Persistence**: entity-framework-core (Microsoft.EntityFrameworkCore 8.0/9.0/10.0)
- **Validation**: fluent-validation (FluentValidation 11.9)
- **Authentication**: jwt-bcrypt (bcrypt cost factor 12, JWT TTL 3600s)
- **Backend Testing Framework**: xunit (xunit 2.7.0)
- **Frontend Framework**: angular
- **Frontend Language**: javascript
- **Frontend Bundler**: vite
- **Frontend Unit Testing**: vitest
- **Frontend E2E Testing**: cypress

## Prohibited Layer Dependencies
Domain core must NOT import:
- `direct SQL string interpolation`
- `global state mutation`
- `Microsoft.AspNetCore.*`
- `System.Data.SqlClient unparameterized queries`
