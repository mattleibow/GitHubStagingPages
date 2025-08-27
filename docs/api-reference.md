---
layout: default
title: API Reference
---

# API Reference

This section provides detailed information about the GreetingApp functionality.

## Program.cs

The main entry point of the application.

### Main Method

The application accepts command line arguments and processes them as follows:

```csharp
// Check if a name was provided as a command line argument
string greeting;
if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
{
    greeting = $"Hello, {args[0]}!";
}
else
{
    greeting = "Hello!";
}

// Print the greeting
Console.WriteLine(greeting);

// Print the current date and time
Console.WriteLine($"Current date and time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
```

### Input Parameters

| Parameter | Type | Description | Required |
|-----------|------|-------------|----------|
| args[0] | string | Name for personalized greeting | No |

### Output

The application outputs:
1. A greeting message (personalized if name provided)
2. Current date and time in `yyyy-MM-dd HH:mm:ss` format

### Examples

**Default greeting:**
```bash
$ dotnet run --project src/GreetingApp
Hello!
Current date and time: 2025-01-27 12:34:56
```

**Personalized greeting:**
```bash
$ dotnet run --project src/GreetingApp -- "Matthew"
Hello, Matthew!
Current date and time: 2025-01-27 12:34:56
```

## Project Configuration

The application is configured as a .NET console application:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

[← Back to Home](index)
