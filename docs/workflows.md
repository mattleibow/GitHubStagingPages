---
layout: default
title: Workflows Reference
---

# Workflows Reference

This page describes each GitHub Actions workflow in `.github/workflows/`.

## `pages-staging-deploy.yml` — Pages: Staging - Build and Deploy

**Triggers:**
- Push to `main`
- Pull request opened, synchronized, or reopened against `main`
- Manual (`workflow_dispatch`) with optional `pr_number` and `dry_run` inputs

**What it does:**

```
build → deploy → comment (PR only)
```

The `deploy` job pushes built content to the `gh-pages` branch. The actual GitHub Pages publish is handled by a separate `pages-go-live.yml` that watches for this workflow to complete.

### `build` job

Runs on every trigger. Produces a site artifact containing:

| Output path | Built from |
|-------------|-----------|
| `index.html`, `style.css`, `404.html` | `site/` (static landing page) |
| `docs/` | Jekyll output from `docs/` |
| `app/` | Blazor WASM publish output |

The Blazor `<base href>` is rewritten to the correct deployment path (e.g., `/GitHubStagingPages/app/` for main, `/GitHubStagingPages/staging/3/app/` for PR #3).

### `deploy` job

Pushes the built artifact into the `gh-pages` branch:

- **Main push:** replaces all root content (preserving `staging/`)
- **PR:** writes to `staging/{pr-number}/`

If `gh-pages` does not exist, it is created as an orphan branch.

**Concurrency:** main deploys use the group `docs-deploy-main`. Each PR deploy uses the group `docs-deploy-pr-{pr-number}`. `cancel-in-progress: true` means a newer commit supersedes an older build in flight.

### `comment` job (PR only)

Posts (or updates) a bot comment on the PR with links to the staging preview. The comment is identified by a hidden HTML marker so it is updated in-place on subsequent pushes rather than creating new comments.

---

## `pages-go-live.yml` — Pages: Go Live

**Triggers:**
- `workflow_run` on completion of "Pages - Staging - Build and Deploy" or "Pages - Staging - Cleanup"
- `workflow_dispatch` (manual re-deploy)

This is the only place GitHub Pages is deployed. It is triggered automatically whenever either `pages-staging-deploy.yml` or `pages-staging-cleanup.yml` completes successfully.

> **Important:** `workflow_run` always executes from the **default branch** (`main`). This means the `pages-go-live.yml` on `main` handles go-live for all pushes and PRs — you only need this file correctly set up on `main`.

**Concurrency:** all go-live runs share the group `docs-go-live` with `cancel-in-progress: true`, so only the latest wins.

---

## `pages-staging-cleanup.yml` — Pages: Staging Cleanup

**Triggers:** Pull request closed against `main`

**What it does:**

```
cleanup
```

Removes `staging/{pr-number}/` from the `gh-pages` branch and pushes. The `pages-go-live.yml` workflow then fires automatically (via `workflow_run`) to re-deploy GitHub Pages with the staging directory gone.

The PR number is validated as a positive integer before being used in any file path. If the staging directory does not exist, the job exits cleanly.

**Concurrency:** Uses the group `docs-deploy-pr-{pr-number}` — the same group as the corresponding staging deploy job in `pages-staging-deploy.yml`. This ensures cleanup always waits for any in-progress staging deploy for the same PR to finish before removing the directory.

---

## `build-and-run.yml` — Build and Test

**Triggers:** Push to `main`, pull request against `main`

Basic CI: restores, builds, and runs the console app to verify the .NET solution compiles and produces correct output.

---

## Workflow Interaction Diagram

```
Push to main
  └─ pages-staging-deploy.yml (build → deploy)      writes root of gh-pages
       └─ pages-go-live.yml (workflow_run)           deploys gh-pages → Pages

PR opened/updated
  └─ pages-staging-deploy.yml (build → deploy → comment)  writes staging/{pr}/ in gh-pages
       └─ pages-go-live.yml (workflow_run)                 deploys gh-pages → Pages

PR closed
  └─ pages-staging-cleanup.yml (cleanup)  removes staging/{pr}/ from gh-pages
       └─ pages-go-live.yml (workflow_run)   deploys gh-pages → Pages
```

> `pages-go-live.yml` runs from `main` and handles all GitHub Pages deployments, regardless of which branch or PR triggered the upstream workflow.

[← Back to Home](index)
