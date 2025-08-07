# Copilot Instructions for CSharp_Learning Repository

## Repository Overview

This repository hosts a **structured learning plan for web application development** focused on ASP.NET Core. It serves as an educational resource deployed as a GitHub Pages website at `https://frankdoylezw.github.io/CSharp_Learning/`. The project combines a static website frontend with automated end-to-end testing using C# and Playwright.

**Repository Statistics:**
- **Type**: Educational website project with automated testing
- **Languages**: HTML, CSS, JavaScript (frontend), C# (.NET 8.0 for testing)
- **Size**: Small repository (~10 source files excluding node_modules)
- **Target Runtime**: .NET 8.0, Node.js 18+
- **Primary Framework**: ASP.NET learning content, NUnit + Playwright for testing

## Build and Validation Commands

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ and npm
- PowerShell (for Playwright browser installation)

### Essential Commands (Run in Order)

#### 1. Build the Solution
```bash
cd /home/runner/work/CSharp_Learning/CSharp_Learning
dotnet build CSharpLearning.sln
```
**Expected time:** ~20-30 seconds  
**Always required:** Yes, run this before any other .NET operations

#### 2. Install NPM Dependencies
```bash
npm install
```
**Expected time:** ~5 seconds  
**Note:** Only installs @playwright/test package

#### 3. Install Playwright Browsers (Critical for Testing)
```bash
cd tests
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
```
**Expected time:** 2-5 minutes  
**Common Issue:** Download can fail due to network issues. If this fails:
- Tests will fail with "Executable doesn't exist" error
- GitHub Actions workflow handles this automatically
- Locally, retry the command or use `playwright install` from npm

#### 4. Run Tests
```bash
cd tests
dotnet test --logger "console;verbosity=normal"
```
**Expected time:** 10-30 seconds (once browsers are installed)  
**Note:** Tests validate the live GitHub Pages deployment, so they require internet access

### Alternative Test Commands
```bash
# Run with tracing (as done in CI)
cd tests
PLAYWRIGHT_HEADLESS=1 dotnet test --logger "trx;LogFileName=TestResults.trx"

# Run with browser debugging (local development)
cd tests
PWDEBUG=1 dotnet test
```

### Validation Pipeline Replication
To replicate the exact GitHub Actions workflow locally:
```bash
# Full pipeline sequence
dotnet build CSharpLearning.sln
cd tests
dotnet restore
dotnet build --configuration Release --no-restore
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
mkdir -p bin/Debug/net8.0/playwright-traces
mkdir -p bin/Debug/net8.0/videos  
PLAYWRIGHT_HEADLESS=1 dotnet test --logger "trx;LogFileName=TestResults.trx"
```

## Project Layout and Architecture

### Root Directory Structure
```
/
├── .github/workflows/playwright.yml    # CI/CD pipeline
├── .gitignore                         # Standard .NET gitignore
├── .vs/                              # Visual Studio files (ignored)
├── CSharpLearning.sln                # Solution file
├── README.md                         # Basic project description
├── assets/                           # Website favicon and icons
├── index.html                        # Main learning plan webpage
├── package.json                      # NPM dependencies for testing
├── scripts.js                        # Frontend JavaScript functionality  
├── styles.css                        # Website styling
├── tests/                            # C# test project directory
│   ├── UITests.cs                    # Playwright end-to-end tests
│   └── tests.csproj                  # Test project file
└── node_modules/                     # NPM dependencies (ignored)
```

### Architectural Components

#### 1. Frontend Website (GitHub Pages)
- **Purpose**: Interactive learning plan for ASP.NET development
- **Technology**: Vanilla HTML/CSS/JavaScript with Bootstrap 4.5.2
- **Key Features**: Progress tracking, smooth scrolling, Supabase integration
- **Deployment**: Automatic GitHub Pages deployment from main branch

#### 2. Testing Infrastructure
- **Framework**: NUnit 3.13.2 + Microsoft.Playwright.NUnit 1.49.0
- **Target**: .NET 8.0
- **Test Type**: End-to-end UI tests against live deployment
- **Browser**: Chromium (headless in CI, configurable locally)
- **Features**: Video recording, tracing, screenshot capture

#### 3. CI/CD Pipeline (.github/workflows/playwright.yml)
- **Trigger**: Push/PR to main branch
- **Environment**: Ubuntu latest
- **Steps**: .NET setup → Node setup → Build → Test → Artifact upload
- **Artifacts**: Test results, traces, videos (1-day retention)

### Key Dependencies and Configurations

#### Critical NuGet Packages (tests/tests.csproj):
- `Microsoft.Playwright.NUnit` (1.49.0) - Core testing framework
- `NUnit` (3.13.2) - Test runner
- `Microsoft.NET.Test.Sdk` (17.8.0) - Test SDK

#### Configuration Notes:
- **Target Framework**: net8.0 with latest C# language version
- **Test Configurations**: Debug, Release, DebugWithBrowser
- **Nullable**: Enabled
- **ImplicitUsings**: Enabled

### Common Issues and Workarounds

#### 1. Playwright Browser Installation Failures
**Symptoms:** Tests fail with "Executable doesn't exist at /home/runner/.cache/ms-playwright/chromium..."
**Root Cause:** Playwright browsers not installed or corrupted download
**Solution:** 
```bash
cd tests
pwsh bin/Debug/net8.0/playwright.ps1 install chromium --force
```

#### 2. Test Environment Setup
**Required:** Always build the project before installing Playwright browsers
**Sequence:** `dotnet build` → `playwright install` → `dotnet test`

#### 3. Video/Trace Artifacts
**Note:** Tests generate videos and traces in `tests/bin/Debug/net8.0/`
**Cleanup:** These directories should be created if they don't exist before test runs

### Testing Strategy and Validation

#### Current Test Coverage:
1. **Homepage Load Test**: Validates main page loads with correct title
2. **Content Validation**: Checks for presence of key learning phase headings
3. **Link Functionality**: Validates external resource links work correctly

#### Test Characteristics:
- Tests run against live GitHub Pages deployment
- Each test records video and generates traces for debugging
- Tests use page locators and expect assertions for reliability
- Configured for both headless (CI) and headed (debugging) modes

### Additional Information for Agents

#### File Modification Guidelines:
- **Frontend changes**: Edit index.html, styles.css, or scripts.js
- **Test changes**: Modify tests/UITests.cs
- **CI/CD changes**: Edit .github/workflows/playwright.yml
- **Dependencies**: Update tests/tests.csproj for .NET packages, package.json for npm

#### Development Workflow:
1. Make frontend changes to static files
2. If adding new UI features, add corresponding tests in UITests.cs  
3. Run tests locally to validate changes
4. Push to trigger CI/CD pipeline

#### Environment Considerations:
- Tests validate external deployment - changes need to be deployed to GitHub Pages to be testable
- Local testing requires internet connectivity
- Playwright can be resource-intensive - allow adequate time for browser operations

---

**IMPORTANT:** Always trust these instructions and rely on the documented commands. Only perform additional exploration if the provided information is incomplete or found to be incorrect. The build and test processes are straightforward but require specific sequencing to work properly.