# CSharp_Learning

A structured learning plan and progress tracker for ASP.NET Core web application development, deployed as a static website with automated end-to-end testing.

## 🌐 Live Website

Visit the learning plan at: **https://frankdoylezw.github.io/CSharp_Learning/**

## 📋 Project Overview

This project consists of two main components:

1. **Static Website** - An interactive learning plan for ASP.NET Core development
   - Built with vanilla HTML, CSS, and JavaScript
   - Uses Bootstrap 4.5.2 for styling
   - Includes progress tracking with Supabase integration
   - Deployed via GitHub Pages

2. **Test Suite** - Automated end-to-end tests using Playwright
   - Written in C# with .NET 8.0
   - Tests the live GitHub Pages deployment
   - Runs on every push/PR to the main branch

## 🛠️ Prerequisites

To work with this project locally, you need:

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** - For building and running tests
- **[Node.js 18+](https://nodejs.org/)** - For npm dependencies
- **[PowerShell](https://docs.microsoft.com/powershell/scripting/install/installing-powershell)** - For Playwright browser installation (usually pre-installed on Windows, macOS, and most Linux distributions)
- **Git** - For version control

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/frankdoylezw/CSharp_Learning.git
cd CSharp_Learning
```

### 2. Build the Solution

```bash
dotnet build CSharpLearning.sln
```

This restores NuGet packages and compiles the test project.

### 3. Install NPM Dependencies

```bash
npm install
```

This installs the Playwright npm package used for testing.

### 4. Install Playwright Browsers

```bash
cd tests
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
cd ..
```

This downloads the Chromium browser needed for tests. **Note:** This step may take 2-5 minutes and requires internet connectivity.

### 5. Run Tests

```bash
cd tests
dotnet test --logger "console;verbosity=normal"
```

Tests will run against the live GitHub Pages deployment.

## 🏗️ Build Commands

### Build the Test Project

```bash
dotnet build CSharpLearning.sln
```

### Build with Specific Configuration

```bash
cd tests
dotnet build --configuration Release
```

Available configurations: `Debug`, `Release`, `DebugWithBrowser`

### Run Tests with Different Options

```bash
# Standard test run
cd tests
dotnet test

# Run with detailed console output
dotnet test --logger "console;verbosity=normal"

# Run with TRX (Visual Studio Test Results) format
dotnet test --logger "trx;LogFileName=TestResults.trx"

# Run in headed mode for debugging (shows browser)
PWDEBUG=1 dotnet test

# Run in headless mode (as in CI)
PLAYWRIGHT_HEADLESS=1 dotnet test
```

### Clean Build Artifacts

```bash
dotnet clean CSharpLearning.sln
```

## 🌍 Deployment

### GitHub Pages Deployment

The website is automatically deployed via **GitHub Pages**:

1. **Source**: GitHub Pages is configured to serve from the `main` branch root directory
2. **Files Served**: `index.html`, `styles.css`, `scripts.js`, and `assets/` folder
3. **URL**: https://frankdoylezw.github.io/CSharp_Learning/
4. **Automatic**: Any push to `main` triggers a deployment (handled by GitHub)

**No build step is required** for the website itself - GitHub Pages serves the static files directly.

### To Deploy Changes

Simply push your changes to the `main` branch:

```bash
git add .
git commit -m "Your descriptive commit message"
git push origin main
```

GitHub Pages will automatically update the website within a few minutes.

## 🔄 CI/CD Pipeline

The project uses GitHub Actions for continuous testing:

### Workflow: `.github/workflows/playwright.yml`

**Trigger**: Runs on every push or pull request to `main`

**Steps**:
1. Checkout code
2. Setup .NET 8.0
3. Setup Node.js 18
4. Install Playwright CLI
5. Restore dependencies and build
6. Install Chromium browser
7. Run Playwright tests in headless mode
8. Upload test results, traces, and videos as artifacts

**Artifacts** (retained for 1 day):
- `test-results` - Test execution results (TRX format)
- `playwright-traces` - Detailed execution traces for debugging
- `playwright-videos` - Video recordings of test runs

### Viewing Test Results

After a workflow run:
1. Go to the [Actions tab](https://github.com/frankdoylezw/CSharp_Learning/actions)
2. Click on a workflow run
3. Download artifacts at the bottom of the page
4. View traces using [trace.playwright.dev](https://trace.playwright.dev/)

## 📁 Project Structure

```
/
├── .github/
│   └── workflows/
│       └── playwright.yml        # CI/CD configuration
├── assets/                       # Favicon and icons
├── tests/
│   ├── UITests.cs               # Playwright test suite
│   ├── tests.csproj             # Test project file
│   └── TestResults/             # Generated test results
├── index.html                   # Main website page
├── styles.css                   # Website styles
├── scripts.js                   # Website JavaScript
├── package.json                 # NPM dependencies
├── CSharpLearning.sln          # Solution file
└── README.md                    # This file
```

## 🧪 Testing

### Test Suite Overview

The project includes three main tests:

1. **HomepageLoadsCorrectly** - Validates the page loads with correct title
2. **CheckPhaseHeadingsExist** - Verifies learning phase headings are present
3. **CheckResourceLinksWork** - Tests external resource links

### Test Features

- Video recording of each test run
- Trace generation for debugging
- Screenshot capture on test failure
- Tests run against live deployment

### Troubleshooting Tests

#### Issue: "Executable doesn't exist" Error

**Cause**: Playwright browsers not installed or corrupted

**Solution**:
```bash
cd tests
pwsh bin/Debug/net8.0/playwright.ps1 install chromium --force
```

#### Issue: Tests Fail Locally but Pass in CI

**Check**:
- Ensure you have internet connectivity (tests hit live site)
- Verify you're testing the latest deployed version
- Check if local browser installation is outdated

#### Issue: Build Required Before Browser Installation

**Sequence**:
```bash
dotnet build CSharpLearning.sln
cd tests
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
dotnet test
```

## 🔧 Development Workflow

### Making Changes to the Website

1. Edit `index.html`, `styles.css`, or `scripts.js`
2. Test locally by opening `index.html` in a browser
3. Commit and push to `main`:
   ```bash
   git add index.html styles.css scripts.js
   git commit -m "Update learning content"
   git push origin main
   ```
4. GitHub Pages will deploy automatically
5. Wait a few minutes, then run tests to verify:
   ```bash
   cd tests
   dotnet test
   ```

### Adding New Tests

1. Edit `tests/UITests.cs`
2. Add test methods following NUnit conventions
3. Build and run:
   ```bash
   cd tests
   dotnet build
   dotnet test
   ```

### Working with Branches

Create a feature branch for development:
```bash
git checkout -b feature/your-feature-name
# Make changes
git add .
git commit -m "Add new feature"
git push origin feature/your-feature-name
```

Create a pull request on GitHub. The CI/CD pipeline will run tests automatically.

## 📝 Technologies Used

- **Frontend**: HTML5, CSS3, JavaScript (ES6+)
- **CSS Framework**: Bootstrap 4.5.2
- **Icons**: Font Awesome 5.15.4
- **Testing**: 
  - .NET 8.0
  - NUnit 3.13.2
  - Microsoft.Playwright.NUnit 1.49.0
- **Backend Services**: Supabase (for progress tracking)
- **Deployment**: GitHub Pages
- **CI/CD**: GitHub Actions

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure tests pass: `cd tests && dotnet test`
5. Submit a pull request

## 📄 License

This project is for educational purposes.

## 🆘 Getting Help

- Review the [Copilot Instructions](.github/copilot-instructions.md) for detailed build/test information
- Check [GitHub Actions runs](https://github.com/frankdoylezw/CSharp_Learning/actions) for CI/CD status
- Review test artifacts for debugging failed tests
- Open an issue for bugs or questions
