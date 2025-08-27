---
layout: default
title: Home
---

# GreetingApp

Welcome to the GreetingApp documentation site! This is a simple console application that demonstrates greeting functionality.

## What is GreetingApp?

GreetingApp is a .NET console application that provides personalized greetings. It can:

- Display a default greeting message
- Accept a name as a command line argument for personalized greetings
- Show the current date and time

## Quick Start

### Building the Application

```bash
dotnet build
```

### Running the Application

Default greeting:
```bash
dotnet run --project src/GreetingApp
```

Personalized greeting:
```bash
dotnet run --project src/GreetingApp -- "Your Name"
```

## Features

- Simple command-line interface
- Customizable greeting messages
- Date and time display
- Cross-platform compatibility

## Documentation Sections

- [Getting Started](getting-started)
- [API Reference](api-reference)
- [Contributing](contributing)

---

*This documentation is automatically built and deployed using GitHub Actions.*

> **Note**: Testing new unified deployment architecture (gh-pages → GitHub Pages).
