---
layout: default
title: Contributing
---

# Contributing

We welcome contributions to GreetingApp! This guide explains how to contribute to the project.

## Development Setup

1. Fork the repository
2. Clone your fork:
   ```bash
   git clone https://github.com/your-username/GitHubStagingPages.git
   cd GitHubStagingPages
   ```

3. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Making Changes

### Code Changes

1. Make your changes to the source code in `src/GreetingApp/`
2. Ensure your code follows the existing style
3. Build and test your changes:
   ```bash
   dotnet build
   dotnet run --project src/GreetingApp
   ```

### Documentation Changes

Documentation is located in the `docs/` directory and uses Jekyll for static site generation.

1. Make changes to markdown files in `docs/`
2. Test locally if needed (Jekyll setup required)
3. Ensure all links work correctly

## Submitting Changes

1. Commit your changes:
   ```bash
   git add .
   git commit -m "Describe your changes"
   ```

2. Push to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```

3. Create a Pull Request on GitHub

## Pull Request Process

1. Ensure your PR has a clear title and description
2. Reference any related issues
3. Check that all GitHub Actions workflows pass
4. Wait for review and address any feedback

## Documentation Staging

When you create a PR, the documentation will be automatically staged at:
`https://mattleibow.github.io/GitHubStagingPages/staging/[pr-number]`

This allows reviewers to see documentation changes before they're merged.

## Code Style

- Use standard C# conventions
- Include XML documentation for public methods
- Keep code simple and readable
- Follow existing patterns in the codebase

## Questions?

If you have questions about contributing, please:
- Check existing issues and discussions
- Create a new issue for questions
- Reach out to maintainers

Thank you for contributing!

[← Back to Home](index.html)