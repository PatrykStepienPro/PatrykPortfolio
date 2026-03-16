# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run development server (hot reload)
dotnet watch

# Build
dotnet build

# Publish for production
dotnet publish -c Release
```

## Architecture

This is a **Blazor WebAssembly** portfolio site targeting **.NET 10.0**. It runs entirely in the browser via WebAssembly — there is no server-side rendering.

**Entry point:** `Program.cs` bootstraps the app, registers `App` as the root component, and provides an `HttpClient` scoped to the base address.

**Routing:** `App.razor` uses `<Router>` to auto-discover pages by assembly scan. All `.razor` files with `@page` directives are routable. The default layout is `MainLayout`.

**Layout:** `Layout/MainLayout.razor` wraps every page with a sidebar (`NavMenu`) and a main content area. `Layout/NavMenu.razor` contains the navigation links — update it when adding new pages.

**Pages** live in `Pages/` and use `@page "/route"` directives.

**Styling:** Bootstrap (via `wwwroot/lib/bootstrap`) plus custom styles in `wwwroot/css/app.css`. Component-scoped CSS can be added as `ComponentName.razor.css` files alongside their component.

**Global imports:** `_Imports.razor` contains namespaces available to all components without explicit `@using` statements.

**Static assets** are served from `wwwroot/`. The HTML shell is `wwwroot/index.html`.
