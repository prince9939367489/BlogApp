# Fieldnotes — BlogApp

A responsive reading website built with **ASP.NET Core MVC and .NET 10**. Browse four original sample essays about software, design, and learning, then open a focused reading page.

[Live website](https://prince-fieldnotes-blog.princekumar120207.chatgpt.site)

## What works

- Four complete sample articles with stable URLs, reading-time estimates, and related-story links.
- Search across titles, summaries, and topics, combined with a category filter.
- Live filtering, shareable search URLs, result counts, reset controls, and an empty state.
- Server-rendered search when JavaScript is disabled in the MVC app.
- Responsive layouts, semantic headings, labeled controls, keyboard focus, and reduced-motion support.
- About and Privacy pages, article descriptions, and a real HTTP 404 for unknown article slugs.
- A static export generated from the rendered MVC pages, keeping both versions consistent.

Articles are demonstration content stored in a typed C# catalog. Database persistence, author accounts, editing, and comments are not implemented. The static website requires JavaScript for filtering; reading and navigation work without it.

## Run locally

Install the .NET 10 SDK.

```bash
git clone https://github.com/prince9939367489/BlogApp.git
cd BlogApp
dotnet restore
dotnet run --project BlogApp/BlogApp.csproj --urls http://localhost:5249
```

Open [localhost:5249](http://localhost:5249). Try searching for "quiet", filtering by Development, opening a story, and using the site on a narrow screen.

## Validate

```bash
dotnet build BlogApp.sln --configuration Release
```

With the app running, use Node.js 22 or newer for the dependency-free HTTP checks:

```bash
node scripts/smoke-test.mjs http://localhost:5249
```

The checks cover all articles, unknown routes, combined search/category filters, whitespace, escaped input, supporting pages, and static assets. GitHub Actions builds the application and runs these checks.

## Regenerate the static website

With the MVC app running:

```bash
node scripts/export-static.mjs http://localhost:5249
```

This renders the home, About, Privacy, and article pages into `dist/`, rewrites internal links to static HTML paths, and copies the CSS, JavaScript, and favicon. Serve `dist/` at a website root. The public showcase is a separately hosted static version; a GitHub commit alone does not redeploy it.

## Project map

| Location | Purpose |
| --- | --- |
| `BlogApp/Models/BlogPost.cs` | Typed article catalog and reading-time calculation |
| `BlogApp/Controllers/HomeController.cs` | Search inputs, page routes, article lookup and 404s |
| `BlogApp/Views/Home` | Story collection, reading pages, About, Privacy |
| `BlogApp/Views/Shared/_Layout.cshtml` | Navigation, metadata, shared footer |
| `BlogApp/wwwroot` | Responsive styles and progressive search enhancement |
| `scripts` | HTTP smoke checks and static export |
| `dist` | Generated static website |

## Next steps

- Add persistent storage and an authenticated authoring workflow.
- Add pagination when the collection grows.
- Introduce comments only with appropriate moderation and privacy controls.
