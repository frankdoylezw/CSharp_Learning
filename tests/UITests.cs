using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace PlaywrightTests;

[TestFixture]
public class PlaywrightTests : PageTest
{
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;
    private string _indexPath;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        // Determine if the browser should be headless based on the PWDEBUG environment variable
        bool headless = Environment.GetEnvironmentVariable("PWDEBUG") != "1";

        // Launch the browser with the headless option
        _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless
        });

        // Set up the path to the local index.html file
        _indexPath = "file://" + Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "index.html");
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        // Close the browser
        await _browser.CloseAsync();
    }

    [SetUp]
    public async Task Setup()
    {
        // Create a new browser context with video recording options
        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "videos"),
            RecordVideoSize = new() { Width = 1280, Height = 720 }
        });

        await _context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Create a new page within the context
        _page = await _context.NewPageAsync();
        await _page.GotoAsync(_indexPath);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });

        // Close the context to stop video recording
        await _context.CloseAsync();
    }

    [Test]
    public async Task HomepageLoadsCorrectly()
    {
        var title = await _page.TitleAsync();
        Assert.That(title, Does.Contain("Structured Learning Plan for Web Application Development"));
    }

    [Test]
    public async Task CheckPhaseHeadingsExist()
    {
        // Check for main phase headings using a more specific selector
        await Expect(_page.Locator(".phase-section h2").First).ToContainTextAsync("Introduction to ASP.NET");
        await Expect(_page.Locator(".phase-section h2").Nth(1)).ToContainTextAsync("Creating APIs with ASP.NET Core");
        await Expect(_page.Locator(".phase-section h2").Nth(2)).ToContainTextAsync("The Model-View-Controller Design Pattern");
    }

    [Test]
    public async Task CheckResourceLinksWork()
    {
        // Check specific resource link exists and is clickable
        var dotnetCliLink = _page.Locator(".resource-label a", new() { HasText = "Microsoft: .NET CLI overview" });
        await Expect(dotnetCliLink).ToBeVisibleAsync();

        // Verify the href attribute
        var href = await dotnetCliLink.GetAttributeAsync("href");
        Assert.That(href, Is.EqualTo("https://learn.microsoft.com/en-us/dotnet/core/tools/"));
    }

    [Test]
    public async Task ProgressBarUpdatesCorrectly()
    {
        // Get the initial progress bar value
        var progressBar = _page.Locator("#progress-bar");
        await Expect(progressBar).ToHaveTextAsync("0%");

        // Find the first checkbox and check it
        var firstCheckbox = _page.Locator("input[type='checkbox']").First;
        await firstCheckbox.CheckAsync();

        // Get the total number of checkboxes
        var totalCheckboxes = await _page.Locator("input[type='checkbox']").CountAsync();

        // Calculate the expected progress
        var expectedProgress = (1.0 / totalCheckboxes) * 100;
        var expectedProgressText = $"{Math.Round(expectedProgress)}%";

        // Verify that the progress bar has updated
        await Expect(progressBar).ToHaveTextAsync(expectedProgressText);
    }
}
