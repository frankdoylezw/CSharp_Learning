using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests;
//[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightTests : PageTest
{
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;

    [SetUp]
    public async Task Setup()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Determine if the browser should be headless based on the PWDEBUG environment variable
        bool headless = Environment.GetEnvironmentVariable("PWDEBUG") != "1";

        // Launch the browser with the headless option
        _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless
        });

        // Create a new browser context with video recording options
        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "videos"),
            RecordVideoSize = new() { Width = 1280, Height = 720 }
        });

        // Create a new page within the context
        _page = await _context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            )
        });

        // Close the context to stop video recording
        await _context.CloseAsync();

        // Add video files as test attachments
        var videoPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "videos");
        if (Directory.Exists(videoPath))
        {
            var videoFiles = Directory.GetFiles(videoPath, "*.webm");
            foreach (var videoFile in videoFiles)
            {
                TestContext.AddTestAttachment(videoFile);
            }
        }

        // Close the browser
        await _browser.CloseAsync();
    }

    [Test]
    public async Task HomepageLoadsCorrectly()
    {
        await _page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        var title = await _page.TitleAsync();
        Assert.That(title, Does.Contain("Structured Learning Plan for Web Application Development"));
    }

    [Test]
    public async Task CheckPhaseHeadingsExist()
    {
        await _page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

        // Check for main phase headings using a more specific selector
        await Expect(_page.Locator(".phase-section h2").First).ToContainTextAsync("Introduction to ASP.NET");
        await Expect(_page.Locator(".phase-section h2").Nth(1)).ToContainTextAsync("Creating APIs with ASP.NET Core");
        await Expect(_page.Locator(".phase-section h2").Nth(2)).ToContainTextAsync("The Model-View-Controller Design Pattern");
    }

    [Test]
    public async Task CheckResourceLinksWork()
    {
        try
        {
            await _page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

            // Check specific resource link exists and is clickable
            var dotnetCliLink = _page.Locator(".resource-label a", new() { HasText = "Microsoft: .NET CLI overview" });
            await Expect(dotnetCliLink).ToBeVisibleAsync();

            // Verify the href attribute
            var href = await dotnetCliLink.GetAttributeAsync("href");
            Assert.That(href, Is.EqualTo("https://learn.microsoft.com/en-us/dotnet/core/tools/"));
        }
        catch (Exception)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshots/CheckResourceLinksWork.png" });
            throw;
        }
    }
}
