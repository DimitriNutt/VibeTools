# VibeTools Web App

VibeTools is a simple directory for discovering, submitting, and reviewing trendy AI tools. Built with .NET 8 (C#), React (TypeScript), and PostgreSQL, it lets users browse, add, and rate tools—no login required.

## Features

- Browse a list of suggested AI tools, including name, ranking, and details
- Click a tool to view its details and reviews
- Submit your own tools to the directory
- Review tools with comments and a star rating
- Search for tools by name, category, or description
- Tools are ranked by the average of all their ratings

## Tech Stack

- **Backend:** ASP.NET Core 8.0 (C#), Carter (Minimal APIs), CQRS (MediatR), Marten (PostgreSQL Document DB), FluentValidation, Mapster
- **Frontend:** React (TypeScript, Vite)
- **Database:** PostgreSQL
- **Containerisation:** Docker Compose

## Quick Start

### Prerequisites
- .NET 8 SDK
- Docker Desktop
- PowerShell (for Windows)

### Run with Docker (Recommended)
```powershell
# Start the full stack (database, API, and web)
docker-compose up -d
```

### Access
- API: http://localhost:5015
- Swagger: http://localhost:5015/swagger
- Web App: http://localhost:5173

### API Endpoints (Key)
- `GET /tools` – List tools (paginated)
- `POST /tools` – Add a new tool
- `GET /tools/{id}` – Tool details
- `PUT /tools/{id}` – Update tool
- `DELETE /tool/{id}` – Delete tool
- `GET /tools/category/{category}` – Tools by category

### Project Structure
```
VibeTools/
├── BuildingBlocks/          # Shared infrastructure
│   ├── CQRS/               # Command/Query interfaces
│   ├── Behaviours/         # MediatR behaviours
│   └── Exceptions/         # Custom exceptions
├── Tools.API/              # Main API project
│   ├── Tools/              # Feature folders
│   ├── Models/             # Domain models
│   └── Data/               # Data seeding
├── tools.web/              # React frontend
└── docker-compose.yml      # Container orchestration
```

## Notes
- No authentication or login required
- Styling is minimal—focus is on functionality
- You can use or extend any part of the stack

## Troubleshooting
- Ensure Docker is running for database, API and web
- Check ports 5432 (Postgres), 5015 (API), and 5173 (Web) are available