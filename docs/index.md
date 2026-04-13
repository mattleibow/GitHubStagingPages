---
layout: default
title: Home
---

# GitHub Staging Pages

This repository demonstrates how to build and deploy a **multi-section site** on GitHub Pages with full **PR staging support** — so every pull request gets its own live preview before merging.

## What This Demonstrates

The pattern shown here gives you:

- A **landing page** that links to all site sections
- A **documentation site** (Jekyll) at `/docs/`
- An **interactive Blazor WASM app** at `/app/`
- **Automatic PR staging** — every PR gets a preview at `/staging/{pr-number}/`
- **PR comments** with direct links to the staging previews
- **Automatic cleanup** — staging sites are removed when their PR is closed

## Site Sections

| Path | Contents |
|------|---------|
| `/` | Landing page |
| `/docs/` | Jekyll documentation (this site) |
| `/app/` | Blazor WebAssembly interactive demo |
| `/staging/{pr}/` | Per-PR preview (mirroring the above structure) |

## Documentation

- [Quick Start](quickstart) — Get up and running in minutes
- [Repository Setup](setup) — Settings and permissions required
- [Workflows Reference](workflows) — How the CI/CD workflows work
- [Branch Strategy](branching) — How `main`, `gh-pages`, and `staging/` fit together

---

*Built on: {{ site.time | date: "%Y-%m-%d %H:%M:%S UTC" }}*
