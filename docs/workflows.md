---
layout: default
title: Workflows Reference
---

# Workflows Reference

This page describes each GitHub Actions workflow in `.github/workflows/`.

## `build-site.yml` — Site: Build and Deploy

**Triggers:**
- Push to `main`
- Pull request opened, synchronized, or reopened against `main`
- Manual (`workflow_dispatch`) with optional `pr_number` and `dry_run` inputs

**What it does:**

```
build → deploy → go-live → comment (PR only)
```

### `build` job

Runs on every trigger. Produces a `site-out/` artifact containing:

| Output path | Built from |
|-------------|-----------|
| `index.html`, `style.css`, `404.html` | `site/` (static landing page) |
| `docs/` | Jekyll output from `docs/` |
| `app/` | Blazor WASM publish output |

The Blazor `<base href>` is rewritten to the correct deployment path (e.g., `/GitHubStagingPages/app/` for main, `/GitHubStagingPages/staging/3/app/` for PR #3).

### `deploy` job

Pushes the built artifact into the `docs-live` branch:

- **Main push:** replaces all root content (preserving `staging/`)
- **PR:** writes to `staging/{pr-number}/`

If `docs-live` does not exist, it is created as an orphan branch.

**Concurrency:** main deploys use the group `docs-deploy-main`. Each PR deploy uses the group `docs-deploy-pr-{pr-number}`. `cancel-in-progress: true` means a newer commit supersedes an older build in flight.

### `go-live` job

Deploys the full `docs-live` branch to GitHub Pages using `actions/deploy-pages`.

This runs after every successful `deploy` — for both main and PR staging — so GitHub Pages always reflects the latest `docs-live` state.

**Concurrency:** all go-live runs share a single group (`docs-go-live`) with `cancel-in-progress: true`, so only the latest wins.

### `comment` job (PR only)

Posts (or updates) a bot comment on the PR with links to the staging preview. The comment is identified by a hidden HTML marker so it is updated in-place on subsequent pushes rather than creating new comments.

---

## `docs-cleanup-staging-pr.yml` — Site: PR Staging Cleanup

**Triggers:** Pull request closed against `main`

**What it does:**

```
cleanup → go-live
```

### `cleanup` job

Removes `staging/{pr-number}/` from the `docs-live` branch. If the directory does not exist (e.g., the PR never triggered a build), the job exits cleanly.

The PR number is validated as a positive integer before being used in any file path.

### `go-live` job

Re-deploys `docs-live` to GitHub Pages after the staging directory has been removed, so the deleted preview disappears from the live site.

**Concurrency:** Uses the group `docs-deploy-pr-{pr-number}` — the same group used by the corresponding staging deploy job in `build-site.yml`. This ensures cleanup always waits for any in-progress staging deploy for the same PR to finish before removing the directory.

---

## `docs-go-live.yml` — Docs: Go Live!

**Triggers:** `workflow_dispatch` only

This is a manual safety valve. If a Pages deployment fails for any reason, run this workflow to re-deploy whatever is currently in `docs-live` without rebuilding anything.

---

## `build-and-run.yml` — Build and Test

**Triggers:** Push to `main`, pull request against `main`

Basic CI: restores, builds, and runs the console app to verify the .NET solution compiles and produces correct output.

---

## Workflow Interaction Diagram

```
Push to main
  └─ build-site.yml
       ├─ build
       ├─ deploy   (writes root of docs-live)
       └─ go-live  (deploys docs-live → Pages)

PR opened/updated
  └─ build-site.yml
       ├─ build
       ├─ deploy   (writes staging/{pr}/ in docs-live)
       ├─ go-live  (deploys docs-live → Pages)
       └─ comment  (posts/updates PR preview comment)

PR closed
  └─ docs-cleanup-staging-pr.yml
       ├─ cleanup  (removes staging/{pr}/ from docs-live)
       └─ go-live  (deploys docs-live → Pages)
```

[← Back to Home](index)
