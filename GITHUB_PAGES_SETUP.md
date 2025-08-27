# GitHub Pages Setup Guide

This document explains how to configure GitHub Pages for this repository to enable the documentation site and PR staging functionality.

## Repository Settings Configuration

To enable GitHub Pages for this repository, follow these steps:

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

### 3. Branch Protection (Optional but Recommended)

For production use, consider setting up branch protection rules:

1. Go to **Settings** → **Branches**
2. Click **Add rule** for `main` branch
3. Enable:
   - ✅ Require status checks to pass before merging
   - ✅ Require branches to be up to date before merging
   - Select the relevant workflow checks

## How It Works

### Main Documentation Deployment

- **Trigger**: Push to `main` branch with changes in `docs/` folder
- **Target**: https://mattleibow.github.io/GitHubStagingPages
- **Workflow**: `.github/workflows/deploy-docs-main.yml`

### PR Staging

- **Trigger**: Pull request with changes in `docs/` folder
- **Target**: https://mattleibow.github.io/GitHubStagingPages/staging/[pr-number]
- **Workflow**: `.github/workflows/deploy-docs-staging-pr.yml`
- **Auto-comment**: Adds a comment to the PR with the staging URL

### Cleanup

- **Trigger**: Pull request closed
- **Action**: Removes staging directory from `gh-pages` branch
- **Workflow**: `.github/workflows/deploy-docs-staging-pr-cleanup.yml`

## Testing the Setup

### 1. Test Main Documentation

1. Merge this PR to `main`
2. Check that the deployment workflow runs successfully
3. Visit https://mattleibow.github.io/GitHubStagingPages

### 2. Test PR Staging

1. Create a new PR with documentation changes
2. Check that the staging workflow runs
3. Look for the bot comment with the staging URL
4. Verify the staging site works
5. Close the PR and verify cleanup runs

## Troubleshooting

### Common Issues

1. **Workflow fails with permissions error**
   - Check that workflow permissions are set to "Read and write"
   - Ensure GITHUB_TOKEN has necessary permissions

2. **404 on GitHub Pages site**
   - Verify Pages is enabled in repository settings
   - Check that the deployment workflow completed successfully
   - Wait a few minutes for DNS propagation

3. **Jekyll build fails**
   - Check the workflow logs for specific errors
   - Verify all markdown files have proper front matter
   - Ensure Gemfile dependencies are correct

### Workflow Logs

To debug issues:
1. Go to **Actions** tab in GitHub
2. Click on the failing workflow run
3. Expand the failed step to see detailed logs

## Local Development

To test documentation changes locally:

```bash
cd docs
gem install bundler
bundle install
bundle exec jekyll serve
```

Visit http://localhost:4000 to preview changes.

## Customization

### Changing the Theme

Edit `docs/_config.yml` and change the `theme` value:

```yaml
theme: minima  # or another Jekyll theme
```

### Adding Custom Styling

Create `docs/assets/css/style.scss`:

```scss
---
---

@import "{{ site.theme }}";

/* Custom styles here */
```

### Custom Layouts

Create custom layouts in `docs/_layouts/` directory.

## Security Notes

- The workflows use `GITHUB_TOKEN` which is automatically provided
- No additional secrets are required for basic functionality
- All deployments are to the public `gh-pages` branch
- Staging sites are publicly accessible but not indexed by search engines

## Automatic Workflow Synchronization

This repository automatically ensures that the `deploy-docs-live.yml` workflow is available in the gh-pages branch. When the main documentation is deployed, the workflow file is copied to gh-pages, enabling direct pushes to gh-pages to trigger GitHub Pages deployment automatically. This ensures seamless deployment whether changes come from the main branch or are pushed directly to gh-pages.