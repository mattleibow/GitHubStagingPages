---
layout: default
title: Quick Start
---

# Quick Start

Get a multi-section GitHub Pages site with PR staging up and running in under 10 minutes.

## Prerequisites

- A GitHub repository (public or private with Pages enabled)
- GitHub Actions enabled on the repository
- Basic familiarity with YAML and GitHub Actions

## Step 1 — Fork or Clone This Repo

```bash
git clone https://github.com/mattleibow/GitHubStagingPages.git
cd GitHubStagingPages
```

Or fork it directly on GitHub and rename it to suit your project.

## Step 2 — Enable GitHub Pages

1. Go to **Settings → Pages**
2. Set **Source** to **GitHub Actions**
3. Save

## Step 3 — Set Workflow Permissions

1. Go to **Settings → Actions → General**
2. Under **Workflow permissions**, select **Read and write permissions**
3. Check **Allow GitHub Actions to create and approve pull requests**
4. Save

## Step 4 — Trigger the First Deployment

Push a commit to `main` (or run the workflow manually from the **Actions** tab):

```bash
git commit --allow-empty -m "chore: trigger first deployment"
git push
```

The `pages-staging-deploy.yml` workflow will:
1. Build the Jekyll docs
2. Publish the Blazor app
3. Push everything to the `gh-pages` branch
4. Deploy `gh-pages` to GitHub Pages

Your site will be live at:
```
https://{your-username}.github.io/{your-repo}/
```

## Step 5 — Test PR Staging

1. Create a new branch and make any change:
   ```bash
   git checkout -b test/staging-preview
   echo "# Test" >> docs/index.md
   git add docs/index.md
   git commit -m "test: staging preview"
   git push -u origin test/staging-preview
   ```
2. Open a pull request against `main`
3. Wait for the workflow to finish
4. The bot will comment on the PR with preview links:
   - 🔗 Staging site
   - 📖 Staging docs
   - ⚡ Staging app

## Step 6 — Close the PR

When the PR is closed (merged or abandoned), the staging directory is automatically removed from `gh-pages` and Pages is updated.

---

**Next:** [Repository Setup](setup) — detailed settings and configuration options.

[← Back to Home](index)
