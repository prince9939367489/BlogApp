# BlogApp — Fieldnotes

Fieldnotes is a responsive ASP.NET Core MVC blog showcase built to demonstrate controller routing, Razor views, shared layouts, custom CSS, static assets, and production error handling.

## Application output

The project renders a complete editorial-style interface with:

- A featured story and three static sample article cards
- Responsive desktop and mobile layouts
- A project About page
- A truthful privacy page for the current no-data-collection scope
- Shared navigation and footer through a Razor layout
- Keyboard-visible focus states and reduced-motion support

> The articles are demonstration content. Post storage, create/edit workflows, authentication, comments, and search are not implemented yet.

**Live demo:** [Open Fieldnotes](https://prince-fieldnotes-blog.princekumar120207.chatgpt.site)

The public URL serves a static export of the same three portfolio pages so visitors can view the output without installing .NET. The MVC source remains the canonical implementation.

## Technology

- ASP.NET Core MVC
- .NET 10 and C#
- Razor views and Tag Helpers
- Custom responsive CSS

## Run locally

### Prerequisite

Install the .NET 10 SDK.

```bash
git clone https://github.com/prince9939367489/BlogApp.git
cd BlogApp
dotnet restore
dotnet run --project BlogApp/BlogApp.csproj
```

Open the local URL printed by .NET. The development profiles currently use:

- `https://localhost:7161`
- `http://localhost:5249`

## Project structure

- `BlogApp/Controllers` — MVC request handlers
- `BlogApp/Models` — view models
- `BlogApp/Views/Home` — Stories, About, and Privacy pages
- `BlogApp/Views/Shared` — shared layout and error page
- `BlogApp/wwwroot` — the project stylesheet and static assets
- `BlogApp/Program.cs` — services, middleware, and conventional routing

## Current scope

This is a polished, learning-focused MVC foundation and static content showcase. It intentionally avoids claiming database-backed blog functionality that has not been implemented.

## Roadmap

- Add post create, read, update, and delete workflows
- Add database persistence with Entity Framework Core
- Add user authentication and authorization
- Add categories, comments, search, and pagination
- Add automated tests
- Add a persistent post model and publishing workflow before moving beyond the static showcase
