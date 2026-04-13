---
layout: default
title: Repository Setup
---

# Repository Setup

This page covers every setting and configuration option needed to make the staging Pages pattern work.

## GitHub Pages Settings

### Source

The site is deployed via GitHub Actions artifacts — **not** from a branch directly.

1. Go to **Settings → Pages**
2. Set **Source** to **GitHub Actions**

> ⚠️ Do **not** set the source to `gh-pages` or `docs-live` branches. The workflow uses `actions/deploy-pages` which requires the **GitHub Actions** source.

### Custom Domain (Optional)

If you want a custom domain, add it in the Pages settings. The `docs-live` branch root should also contain a `CNAME` file with the domain name.

## Actions Permissions

### Workflow Permissions

The deploy workflow needs to commit to the `docs-live` branch and post comments on pull requests.

1. Go to **Settings → Actions → General**
2. Under **Workflow permissions**:
   - ✅ **Read and write permissions**
   - ✅ **Allow GitHub Actions to create and approve pull requests**

### The `github-pages` Environment

GitHub automatically creates a `github-pages` environment when you enable GitHub Pages via Actions. The `go-live` job in `build-site.yml` targets this environment:

```yaml
environment:
  name: github-pages
  url: ${{ steps.deployment.outputs.page_url }}
```

You can optionally require manual approval before deployments go live by configuring environment protection rules at **Settings → Environments → github-pages**.

## The `docs-live` Branch

The `docs-live` branch is the staging area for all published content. It is:

- Created automatically by the first workflow run
- **Never edited manually** — always written by the workflows
- Structured as shown in [Branch Strategy](branching)

You do not need to create this branch yourself.

## Local Development

### Jekyll Documentation

```bash
cd docs
gem install bundler
bundle install
bundle exec jekyll serve
```

Preview at `http://localhost:4000`.

### Blazor WASM App

```bash
dotnet run --project src/GreetingApp.Blazor
```

Preview at `http://localhost:5000`.

### Console App

```bash
dotnet run --project src/GreetingApp
dotnet run --project src/GreetingApp -- "Your Name"
```

## Adapting for Your Own Project

To use this pattern in your own project:

1. Replace `docs/` content with your own documentation
2. Replace `src/GreetingApp.Blazor/` with your own Blazor app (or remove it)
3. Update `site/index.html` with your own landing page
4. Update the base URLs in `build-site.yml` (search for `GitHubStagingPages`)
5. Update the PR comment URLs in `build-site.yml`

[← Back to Home](index)
