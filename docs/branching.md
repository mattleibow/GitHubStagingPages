---
layout: default
title: Branch Strategy
---

# Branch Strategy

This page explains how the three key branches work together to deliver both a live production site and per-PR staging previews.

## Overview

```
main          ──── source of truth (code + content)
gh-pages      ──── assembled site content (never edited manually)
  ├── index.html, style.css, 404.html, .nojekyll
  ├── docs/         ← Jekyll output
  ├── app/          ← Blazor WASM publish output
  └── staging/
        ├── 12/     ← staging preview for PR #12
        │    ├── index.html
        │    ├── docs/
        │    └── app/
        └── 17/     ← staging preview for PR #17
```

## `main` — Source of Truth

All development happens on `main` (or short-lived feature branches that PR into `main`).

- `docs/` — Jekyll documentation source
- `site/` — static landing page
- `src/` — application source code
- `.github/workflows/` — CI/CD definitions

> **Never push site output to `main`.** The workflows build and deploy automatically.

## `gh-pages` — Assembled Site

`gh-pages` is an **orphan branch** that contains only the assembled, ready-to-serve site. It is the single source used for every GitHub Pages deployment.

### Root (main content)

When a commit is pushed to `main`, `pages-staging-deploy.yml` overwrites the root of `gh-pages` with the freshly built site — but it **preserves the `staging/` directory**:

```bash
find . -maxdepth 1 \
    -not -name 'staging' \
    -not -name '.git' \
    -exec rm -rf {} \;
cp -r /tmp/site-temp/. .
```

### `staging/{pr-number}/` (PR content)

When a PR is opened or updated, its built site is written to a subdirectory:

```bash
rm -rf "staging/$PR_NUMBER"
mkdir -p "staging/$PR_NUMBER"
cp -r /tmp/site-temp/. "staging/$PR_NUMBER/"
```

This means multiple PRs can have active staging previews at the same time without interfering with each other or the main site.

### Cleanup

When a PR is closed, its `staging/{pr-number}/` directory is deleted from `gh-pages` and Pages is re-deployed. The main site content is untouched.

## Why Not Deploy Directly From `main`?

Deploying from `main` directly (e.g., `actions/upload-pages-artifact` on the source checkout) would mean the live Pages site could only ever contain the full current state of main — no room for staging sub-directories.

The `gh-pages` branch acts as a **mutable staging area** that can hold both the latest main content and any number of PR previews simultaneously.

## Base Href Rewrites

Both the Blazor app and the Jekyll docs must know their deployment path at **build time** so they generate correct asset URLs.

- **Jekyll**: `--baseurl` flag → controls all relative links in generated HTML
- **Blazor**: `<base href>` in `wwwroot/index.html` → rewritten with `sed` during assembly

| Deployment target | Jekyll baseurl | Blazor base href |
|-------------------|---------------|-----------------|
| Main site | `/GitHubStagingPages/docs` | `/GitHubStagingPages/app/` |
| PR #5 staging | `/GitHubStagingPages/staging/5/docs` | `/GitHubStagingPages/staging/5/app/` |

## `.nojekyll`

A `.nojekyll` file is always present at the root of `gh-pages`. This tells GitHub Pages **not** to run Jekyll on the branch content — the site is already pre-built. Without this file, GitHub Pages would ignore directories starting with `_` (like `_framework/` used by Blazor).

[← Back to Home](index)
