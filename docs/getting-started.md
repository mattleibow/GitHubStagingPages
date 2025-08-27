---
layout: default
title: Getting Started
---

# Getting Started

This guide will help you get started with GreetingApp.

## Prerequisites

- .NET 8 SDK or later
- Git (for cloning the repository)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mattleibow/GitHubStagingPages.git
   cd GitHubStagingPages
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the application:
   ```bash
   dotnet build
   ```

## Usage

### Basic Usage

Run the application with default greeting:

```bash
dotnet run --project src/GreetingApp
```

Output:
```
Hello!
Current date and time: 2025-01-27 12:34:56
```

### Personalized Greeting

Run the application with a custom name:

```bash
dotnet run --project src/GreetingApp -- "John Doe"
```

Output:
```
Hello, John Doe!
Current date and time: 2025-01-27 12:34:56
```

## Next Steps

- Check out the [API Reference](api-reference.html) for more details
- Learn about [Contributing](contributing.html) to the project

[← Back to Home](index.html)