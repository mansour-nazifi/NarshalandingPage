# AGENTS.md — Narsha Landing Page

## Stack & entrypoint
- ASP.NET Core 10 (`net10.0`), single project: `StaticSiteGeneration/NarshaStaticSite.csproj`
- Entry: `StaticSiteGeneration/Program.cs` — registers controllers, HTTP client, response compression, HTML minification middleware
- No test project, no CI config, no lint/typecheck scripts

## Build commands
- `dotnet build NarshaStaticSite.csproj -c Release` — also runs `npm run build:styles` (Sass compressed + rtlcss+cssnano RTL) via `AfterTargets="Build"`
- `npm run build:styles` — builds `wwwroot/styles/site.css` and `site.rtl.css` from `wwwroot/styles/scss/site.scss`
- Static generation: run the app (`dotnet run -c Release`), then GET `http://localhost:5049/generate-static` → writes to `static-output/` (deletes and recreates it first)

## Static generation flow
- `Controllers/StaticSiteGenerationController.cs` fetches the app's own pages over HTTP (`/` and each category/product slug) and writes HTML + copied `wwwroot/` assets into `static-output/`
- Pages reference root-relative assets: `/styles/style.css`, `/scripts/script.js`, `/images/...` (never relative paths)
- All HTML/CSS files must be UTF-8 (Persian text)

## RTL / CSS constraints (verified in project.md + corrections.md)
- Bootstrap 5.3.3 is LTR by default → `rtlcss` pipeline is required; `dir="rtl"` alone is insufficient
- Do NOT set `direction` on `body` in SCSS; use `<html lang="fa" dir="rtl">`
- Do NOT use custom `line-height` in SCSS; use Bootstrap defaults
- CSS assets are root-relative (e.g. `/styles/style.css`)
- Navigation active state: static `active` class in HTML only; never toggle via JS
- Terminology: use "شومیز", never "بلوز"

## Card/image conventions
- Card images are wrapped in `.card-image-wrap` (`position: relative`, `overflow: hidden`, `display: block`, `width: 100%`) so `transform: scale()` hover zoom doesn't grow card height or overflow
- Cards with a "مشاهده" link include `<a class="card-clickable-overlay">` (full-card click area, `aria-hidden="true"`, `tabindex="-1`)
- SCSS source: `wwwroot/styles/scss/` (entry `site.scss` imports Bootstrap + `styles/` Narsha overrides)

## Non-obvious gotchas
- `Program.cs` minifies `text/html` responses via `HtmlMinificationMiddleware` + gzip/brotli compression (`EnableForHttps=true`)
- `dotnet build` triggers the Sass/RTL build; don't edit compiled `site.css`/`site.rtl.css` directly — edit SCSS then rebuild
- No `package-lock.json` commit requirement; `node_modules/` exists locally
