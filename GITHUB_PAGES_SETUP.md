# GitHub Pages Setup Guide

This document explains how to configure GitHub Pages for this repository to enable the full site (landing page, docs, and Blazor app) with PR staging functionality.

## Site Structure

The deployed site has three sections:

| Path | Content |
|------|---------|
| `/GitHubStagingPages/` | Landing page |
| `/GitHubStagingPages/docs/` | Jekyll documentation site |
| `/GitHubStagingPages/app/` | Blazor WebAssembly interactive app |

PR staging mirrors this structure under `/GitHubStagingPages/staging/{pr-number}/`.

## Repository Settings Configuration

### 1. Enable GitHub Pages

1. Go to your repository on GitHub
2. Click on **Settings** tab
3. Scroll down to **Pages** section in the left sidebar
4. Under **Source**, select **GitHub Actions**
5. Save the settings

### 2. Required Permissions

The workflows require the following permissions to be enabled:

1. Go to **Settings** → **Actions** → **General**
2. Under **Workflow permissions**, ensure:
   - ✅ Read and write permissions
   - ✅ Allow GitHub Actions to create and approve pull requests

## How It Works

### Unified Build and Deploy (`build-site.yml`)

All site content (landing page, docs, Blazor app) is built in a single workflow and pushed to the pages branch (default: `gh-pages`).

- **Main deploy** (push to `main`): site goes to the root of the pages branch
- **PR staging** (pull request): site goes to `{pages-branch}/staging/{pr-number}/`
- **PR comment**: bot posts (and updates) a comment with staging preview links
- **Dry run**: `workflow_dispatch` with `dry_run: true` builds but skips deploy

### Go Live (`docs-go-live.yml`)

Triggered automatically after a successful build/deploy or cleanup, this workflow publishes the pages branch to GitHub Pages.

### PR Cleanup (`docs-cleanup-staging-pr.yml`)

When a PR is closed, the staging directory is removed from the pages branch.

## Workflow Summary

| Workflow | Trigger | Action |
|----------|---------|--------|
| `build-site.yml` | Push to main / PR open or update | Build site, deploy to pages branch (default: `gh-pages`), comment on PR |
| `docs-go-live.yml` | After build/deploy/cleanup | Publish pages branch to GitHub Pages |
| `docs-cleanup-staging-pr.yml` | PR closed | Remove staging directory from pages branch |
| `build-and-run.yml` | Push / PR | Build and run the console app CI check |

## Local Development

### Documentation (Jekyll)

```bash
cd docs
gem install bundler
bundle install
bundle exec jekyll serve
```

Visit http://localhost:4000 to preview.

### Blazor WASM App

```bash
cd src/GreetingApp.Blazor
dotnet run
```

Visit http://localhost:5000 to preview.

## Troubleshooting

### Workflow fails with permissions error
- Check that workflow permissions are set to "Read and write"
- Ensure GITHUB_TOKEN has the necessary permissions

### 404 on GitHub Pages site
- Verify Pages is enabled and set to "GitHub Actions" source
- Check that the `docs-go-live.yml` workflow completed successfully
- Wait a few minutes for GitHub Pages to propagate

### Jekyll build fails
- Check the workflow logs for specific errors
- Verify all markdown files have proper front matter
- Ensure `docs/Gemfile` dependencies are correct

### Blazor app shows blank page
- Confirm the `<base href>` was correctly rewritten in the deploy step
- Check browser console for errors — usually indicates a wrong base path
