# GitHubStagingPages

An example repo showing how to set up staging docs with GitHub Pages.

## GreetingApp

This repository contains a simple .NET 8 console application called GreetingApp that demonstrates:
- Command-line argument processing
- Personalized greeting messages
- Date and time display

## Documentation

📖 **[View Documentation](https://mattleibow.github.io/GitHubStagingPages)**

The documentation is automatically built and deployed using GitHub Actions:
- **Main docs**: https://mattleibow.github.io/GitHubStagingPages
- **PR staging**: https://mattleibow.github.io/GitHubStagingPages/staging/[pr-number]

## Quick Start

```bash
# Clone the repository
git clone https://github.com/mattleibow/GitHubStagingPages.git
cd GitHubStagingPages

# Build the application
dotnet build

# Run with default greeting
dotnet run --project src/GreetingApp

# Run with custom name
dotnet run --project src/GreetingApp -- "Your Name"
```

## Features

- ✅ Automated documentation deployment to GitHub Pages
- ✅ PR staging for documentation reviews
- ✅ Automatic cleanup of staging sites when PRs are closed
- ✅ Jekyll-based static site generation

## Setup

To enable GitHub Pages for your own repository, see [GitHub Pages Setup Guide](GITHUB_PAGES_SETUP.md).
