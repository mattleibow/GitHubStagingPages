# GitHubStagingPages Repository

GitHubStagingPages is a demonstration repository showcasing GitHub Pages with PR staging functionality. It contains a .NET 8 console application (GreetingApp) and Jekyll-based documentation with automated deployment workflows.

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Prerequisites
- .NET 8 SDK (pre-installed in GitHub Actions environments)
- Ruby 3.2+ with bundler for documentation (pre-installed in GitHub Actions environments)

### Bootstrap, Build, and Run the .NET Application
Run these commands to build and test the GreetingApp:

```bash
# Navigate to repository root
cd /path/to/GitHubStagingPages

# Restore dependencies - takes 1-8 seconds. NEVER CANCEL.
dotnet restore
# Timeout: Set to 120+ seconds for slow network conditions

# Build the application - takes 1-10 seconds. NEVER CANCEL.
dotnet build
# Timeout: Set to 120+ seconds for slow systems

# Run with default greeting
dotnet run --project src/GreetingApp

# Run with custom name
dotnet run --project src/GreetingApp -- "Your Name"
```

### Bootstrap, Build, and Serve Documentation
Run these commands to build and serve the Jekyll documentation:

```bash
# Navigate to docs directory
cd docs

# Install bundler for local user (if not available globally)
gem install --user-install bundler
export PATH="$HOME/.local/share/gem/ruby/3.2.0/bin:$PATH"

# Configure bundle to use local vendor directory (required for permission restrictions)
bundle config set --local path 'vendor/bundle'

# Install Jekyll dependencies - first run takes 20-60 seconds, subsequent runs are much faster (~1 second). NEVER CANCEL.
bundle install
# Timeout: Set to 300+ seconds for slow network conditions

# Build documentation - takes ~1 second
bundle exec jekyll build

# Serve documentation locally for development
bundle exec jekyll serve --host 0.0.0.0 --port 4000
# Access at http://localhost:4000
```

## Validation

### Manual Validation Requirements
**ALWAYS manually validate changes by running through complete user scenarios after making code changes:**

#### .NET Application Scenarios
1. **Default greeting test**: Run `dotnet run --project src/GreetingApp` and verify output shows "Hello!" with current timestamp
2. **Custom name test**: Run `dotnet run --project src/GreetingApp -- "Test User"` and verify output shows "Hello, Test User!" with current timestamp

#### Documentation Scenarios  
1. **Build verification**: Run `bundle exec jekyll build` and verify `docs/_site/` contains: index.html, getting-started.html, api-reference.html, contributing.html
2. **Navigation test**: Check that generated `docs/_site/index.html` contains working links to all documentation sections
3. **Content validation**: Verify each documentation page has proper H1 headings and expected content structure

### Automated Quality Checks
Always run these commands before committing changes:

```bash
# Format .NET code and verify no changes needed
dotnet format --verify-no-changes --verbosity diagnostic
# Timeout: Set to 120+ seconds

# Build and test the application (no explicit unit tests in this repository)
dotnet build
dotnet run --project src/GreetingApp
dotnet run --project src/GreetingApp -- "CI Test"

# Build documentation to catch Jekyll errors  
cd docs && bundle exec jekyll build
```

## Build Timing and Critical Warnings

**⚠️ CRITICAL TIMEOUT SETTINGS:**
- `dotnet restore`: **NEVER CANCEL** - typically 1-8 seconds, set timeout to 120+ seconds
- `dotnet build`: **NEVER CANCEL** - typically 1-10 seconds, set timeout to 120+ seconds  
- `bundle install`: **NEVER CANCEL** - first run 20-60 seconds, cached runs ~1 second, set timeout to 300+ seconds
- `bundle exec jekyll build`: typically 1 second, set timeout to 60+ seconds

**⚠️ NEVER CANCEL any build or test commands.** Builds may take longer on slow systems or networks. Wait for completion.

## Repository Structure

### Key Projects and Locations
- **GreetingApp**: `src/GreetingApp/` - Main .NET 8 console application
- **Documentation**: `docs/` - Jekyll-based documentation site  
- **GitHub Actions**: `.github/workflows/` - 4 workflows for CI/CD and docs deployment
- **Solution file**: `GreetingApp.sln` - Visual Studio solution in repository root

### Important Files
- `README.md` - Repository overview and quick start guide
- `GITHUB_PAGES_SETUP.md` - Detailed GitHub Pages configuration instructions
- `docs/_config.yml` - Jekyll site configuration
- `docs/Gemfile` - Ruby dependencies for documentation
- `index.html` - Root redirect page to documentation site

### GitHub Actions Workflows
- `build-and-run.yml` - Builds and tests .NET application on push/PR
- `deploy-docs.yml` - Deploys documentation to GitHub Pages on main branch docs changes  
- `deploy-pr-staging.yml` - Creates staging documentation sites for PRs
- `cleanup-pr-staging.yml` - Removes staging sites when PRs are closed

## Common Commands Reference

### .NET Application
```bash
# Build and run (from repository root)
dotnet restore && dotnet build
dotnet run --project src/GreetingApp
dotnet run --project src/GreetingApp -- "Custom Name"

# Code formatting
dotnet format --verify-no-changes
```

### Jekyll Documentation  
```bash
# Setup and build (from docs/ directory)
export PATH="$HOME/.local/share/gem/ruby/3.2.0/bin:$PATH"
bundle config set --local path 'vendor/bundle'
bundle install
bundle exec jekyll build

# Local development server
bundle exec jekyll serve --host 0.0.0.0 --port 4000
```

### Repository Overview Commands
```bash
# Repository root structure
ls -la
# Output:
.git/
.github/
.gitignore
GITHUB_PAGES_SETUP.md  
GreetingApp.sln
LICENSE
README.md
docs/
index.html
src/

# Source structure  
ls -la src/
# Output:
GreetingApp/

# Documentation structure
ls -la docs/
# Output:
Gemfile
_config.yml
api-reference.md
contributing.md
getting-started.md
index.md
```

## Common Issues and Solutions

### Jekyll Permission Issues
- **Problem**: `bundle install` fails with permission errors
- **Solution**: Use `bundle config set --local path 'vendor/bundle'` before `bundle install`

### Jekyll Sass Deprecation Warnings
- **Expected**: Multiple Sass deprecation warnings during `jekyll build`
- **Impact**: Warnings are harmless, build succeeds correctly
- **Action**: No action required, warnings do not affect functionality
- **Sample warnings**: "@import rules are deprecated", "lighten() is deprecated", "darken() is deprecated"

### .NET Build Output
- **Expected**: Build succeeds with 0 warnings, 0 errors in ~10 seconds
- **Expected output location**: `src/GreetingApp/bin/Debug/net8.0/`

## Development Workflows

### Making Changes to .NET Application
1. Edit files in `src/GreetingApp/`
2. Run `dotnet build` to verify compilation
3. Test with `dotnet run --project src/GreetingApp` 
4. Test with custom input: `dotnet run --project src/GreetingApp -- "Test"`
5. Verify formatting: `dotnet format --verify-no-changes`

### Making Changes to Documentation
1. Edit markdown files in `docs/`
2. Test build: `cd docs && bundle exec jekyll build`
3. Verify generated content in `docs/_site/`
4. Test locally: `bundle exec jekyll serve --host 0.0.0.0 --port 4000`
5. Check navigation and content in browser at http://localhost:4000

### Working with GitHub Actions
- Builds trigger automatically on push to main or PR creation
- Documentation staging automatically creates preview sites for PRs
- All workflows must pass for successful CI/CD
- Check `.github/workflows/` for workflow definitions

**Always run the complete validation scenarios above before committing any changes.**