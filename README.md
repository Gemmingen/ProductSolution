# ProductSolution

# 🧠 Product API – Clean Architecture mit .NET 8

Dies ist eine mehrschichtige Web API-Anwendung, die nach dem Clean Architecture Prinzip strukturiert ist. Sie bietet REST-Endpunkte zur Verwaltung von Produkten inklusive persistenter Speicherung via Entity Framework Core.

---

## 📂 Projektstruktur

```bash
src/
├── Product.API         # REST API Layer (Controller, Swagger, Mapping)
├── Product.Business    # Business Logic Layer (Model, Service, Interfaces)
└── Product.Repository  # Datenzugriff Layer (EF Core, Repositories, DbContext)
