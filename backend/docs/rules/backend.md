# STTB Website — Backend Standard
# Stack: ASP.NET Core (.NET 8) + PostgreSQL + Entity Framework Core
# Applies to: /backend directory
# Development environment: Parallels (Windows VM)
# IDE: Visual Studio (Windows) — bukan VS Code

---

## 1. Stack

| Layer | Technology | Notes |
|---|---|---|
| Framework | ASP.NET Core (.NET 8+) | Web API only |
| Language | C# | PascalCase conventions |
| Database | PostgreSQL | Via EF Core code-first |
| ORM | Entity Framework Core | Migrations auto-applied on startup |
| Pattern | Clean Architecture + CQRS | Same layer separation as mentor standard |
| Mediator | MediatR | Controllers call _mediator.Send() only |
| Validation | FluentValidation | All commands with user input must have a validator |
| Auth | ASP.NET Core Identity + JWT | Admin panel only — public website is unauthenticated |
| Logging | Serilog | Structured JSON logging |
| Object Storage | S3-compatible (MinIO local / AWS prod) | For uploaded images, PDFs |

---

## 2. Project Structure

```
backend/
├── Sttb.WebAPI/                 # Entry point — thin controllers only
│   ├── Controllers/
│   │   ├── BeritaController.cs
│   │   ├── KegiatanController.cs
│   │   ├── MediaController.cs
│   │   ├── PerpustakaanController.cs
│   │   └── AdminController.cs
│   ├── Program.cs               # DI, middleware, startup
│   └── appsettings.json
├── Sttb.Commons/                # Application layer — business logic
│   ├── Constants/
│   ├── Extensions/              # Service registration
│   ├── RequestHandlers/         # CQRS handlers, grouped by feature
│   │   ├── Berita/
│   │   ├── Kegiatan/
│   │   ├── Media/
│   │   └── Perpustakaan/
│   ├── Services/                # Infrastructure services (storage, email)
│   └── Validators/              # FluentValidation validators by feature
├── Sttb.Contracts/              # DTOs — zero logic, zero dependencies
│   ├── RequestModels/
│   │   ├── Berita/
│   │   ├── Kegiatan/
│   │   └── ...
│   └── ResponseModels/
│       ├── Berita/
│       ├── Kegiatan/
│       └── ...
└── Sttb.Entities/               # Domain + data access
    ├── ApplicationDbContext.cs
    ├── Migrations/
    └── Entities/
        ├── Berita.cs
        ├── Kegiatan.cs
        ├── MediaArtikel.cs
        ├── MediaVideo.cs
        └── KoleksiPerpustakaan.cs
```

---

## 3. CORS Configuration

The frontend runs on Mac (`localhost:3000`) and hits the backend on Parallels.
CORS must explicitly allow this during development:

```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("SttbFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",           // Next.js dev server (Mac)
                "https://sttb.ac.id"               // Production
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

app.UseCors("SttbFrontend");
```

---

## 4. Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Classes / Methods / Properties | PascalCase | `GetBeritaListHandler`, `PublishedAt` |
| Parameters / Local variables | camelCase | `beritaId`, `pageNumber` |
| Private fields | `_camelCase` | `_mediator`, `_dbContext` |
| Request models | `[Action][Resource]Request` | `GetBeritaListRequest`, `CreateBeritaRequest` |
| Response models | `[Action][Resource]Response` | `GetBeritaListResponse`, `GetBeritaDetailResponse` |
| Validators | `[RequestName]Validator` | `CreateBeritaRequestValidator` |
| Handlers | `[RequestName]Handler` | `GetBeritaListHandler` |
| List queries | `Get[Entity]ListRequest` | NOT `List[Entity]Request` |
| Entities | PascalCase singular noun | `Berita`, `Kegiatan`, `MediaArtikel` |
| DB tables | plural snake_case (configured in EF) | `beritas`, `kegiatans` |

---

## 5. Controller Standard

Controllers are thin — they only call `_mediator.Send()`.
No business logic inside controllers.

```csharp
[ApiController]
[Route("api/[controller]")]
public class BeritaController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeritaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    public async Task<ActionResult<GetBeritaListResponse>> List(
        [FromQuery] GetBeritaListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<GetBeritaDetailResponse>> Detail(
        string slug,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetBeritaDetailRequest { Slug = slug },
            cancellationToken);
        return Ok(result);
    }
}
```

### Rules
- Public website endpoints: no `[Authorize]`
- Admin CMS endpoints: always `[Authorize]`
- GET requests use `[FromQuery]`, POST/PUT/PATCH use `[FromBody]`
- Always accept `CancellationToken`
- Use standard HTTP status codes (200, 201, 400, 404, 500)

---

## 6. Entity Standard

```csharp
public class Berita
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(300)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty; // HTML/Markdown body

    [StringLength(500)]
    public string Excerpt { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ThumbnailUrl { get; set; }

    public bool IsPublished { get; set; } = false;

    public DateTime PublishedAt { get; set; }

    // Audit
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

### Rules
- `[StringLength]` on ALL string properties
- Use `Guid` as primary key
- Include audit fields (`CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`) on all entities
- Initialize navigation collections to `new List<T>()`
- Every entity change requires a new EF migration

---

## 7. Pagination Standard

All list endpoints must support pagination:

```csharp
// Contracts/RequestModels/Shared/PagedRequest.cs
public abstract class PagedRequest : IRequest<object>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 9; // Default to 9 (3x3 grid)
}

// Contracts/ResponseModels/Shared/PagedResponse.cs
public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
```

---

## 8. Response Format

All API responses follow a consistent shape:

```json
// List response
{
  "items": [...],
  "totalCount": 32,
  "page": 1,
  "pageSize": 9,
  "totalPages": 4
}

// Detail response — return the object directly (not wrapped)
{
  "id": "...",
  "title": "...",
  ...
}
```

---

## 9. Database Management

- Code-first with EF Core
- Auto-apply pending migrations on startup via `HostedService`
- PostgreSQL naming: configure `UseSnakeCaseNamingConvention()` in EF options

```bash
# Create a new migration
dotnet ef migrations add AddBeritaTable \
  --project Sttb.Entities \
  --startup-project Sttb.WebAPI

# Apply manually if needed
dotnet ef database update \
  --project Sttb.Entities \
  --startup-project Sttb.WebAPI
```

---

## 10. Validation

Every command (POST/PUT/PATCH) must have a FluentValidation validator:

```csharp
// Commons/Validators/Berita/CreateBeritaRequestValidator.cs
public class CreateBeritaRequestValidator : AbstractValidator<CreateBeritaRequest>
{
    public CreateBeritaRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(300)
            .Matches("^[a-z0-9-]+$")
            .WithMessage("Slug must be lowercase alphanumeric with hyphens only.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .Must(c => BeritaCategory.ValidValues.Contains(c))
            .WithMessage("Invalid category.");
    }
}
```

---

## 11. Error Handling

- Do NOT swallow exceptions
- Return `ProblemDetails` for all error responses
- Use global exception middleware — do not try/catch in every handler
- Let 404s surface when entity is not found (use `NotFoundException`)
- Log exceptions with Serilog including request context

---

## 12. Configuration

```json
// appsettings.json
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "" // Set in appsettings.Development.json or user-secrets
  },
  "Jwt": {
    "Issuer": "sttb-api",
    "Audience": "sttb-admin"
    // Secret set via user-secrets ONLY
  },
  "Storage": {
    "BucketName": "sttb-uploads"
    // Keys set via user-secrets or environment variables
  }
}
```

Never put actual secrets in `appsettings.json`.
Use `dotnet user-secrets` for local development.

---

## 13. Mandatory Rules for AI Agents

- Never run `dotnet build`, `dotnet run`, or execute the application
- Never hardcode connection strings, JWT secrets, or API keys
- Always place files in correct feature folders — never dump in root
- Always create a FluentValidation validator for every new command
- Business logic goes in `RequestHandlers` — never in controllers
- DTOs go in `Contracts` — never in `Commons` or `WebAPI`
- Every new entity requires a new EF Core migration
- Use `async/await` for all I/O-bound operations
- Never use `SELECT *` equivalent — always project to specific response DTOs
