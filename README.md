# GitHubStagingPages

An example repo showing how to set up a multi-part site with staging support using GitHub Pages.

## GreetingApp

This repository contains a simple .NET console application called GreetingApp and a Blazor WebAssembly interactive demo.

## Site

The site is made up of three parts:

| URL | Content |
|-----|---------|
| [Landing page](https://mattleibow.github.io/GitHubStagingPages/) | Home page linking to docs and app |
| [Docs](https://mattleibow.github.io/GitHubStagingPages/docs/) | Jekyll documentation site |
| [App](https://mattleibow.github.io/GitHubStagingPages/app/) | Blazor WebAssembly interactive demo |

PR staging mirrors this structure under `/staging/{pr-number}/`.

## Quick Start

```bash
# Clone the repository
git clone https://github.com/mattleibow/GitHubStagingPages.git
cd GitHubStagingPages

# Run the console app
dotnet run --project src/GreetingApp

# Run the Blazor app locally
dotnet run --project src/GreetingApp.Blazor
```

## Features

- ✅ Unified build and deploy workflow (landing page + docs + Blazor app)
- ✅ PR staging for all site sections
- ✅ Automatic PR comment with preview links
- ✅ Automatic cleanup of staging sites when PRs are closed
- ✅ Jekyll documentation site
- ✅ Blazor WebAssembly interactive app

## Setup

To enable GitHub Pages for your own repository, see the [Repository Setup](docs/setup.md) documentation.
