using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightTests : PageTest
{
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

        // Create a new browser context with video recording options
        _context = await Browser.NewContextAsync(new BrowserNewContextOptions
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

    [Test]
    public async Task CheckboxFunctionality()
    {
        try
        {
            await _page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

            // Get the first checkbox (section1-completed)
            var checkbox = _page.Locator("#section1-completed");

            // Verify it exists
            await Expect(checkbox).ToBeVisibleAsync();

            // Check it and verify the state changes
            await checkbox.CheckAsync();
            Assert.That(await checkbox.IsCheckedAsync(), Is.True);

            // Uncheck it and verify the state changes
            await checkbox.UncheckAsync();
            Assert.That(await checkbox.IsCheckedAsync(), Is.False);
        }
        catch (Exception)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshots/CheckboxFunctionality.png" });
            throw;
        }
    }

    [Test]
    public async Task LongerTest()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Expect(Page.Locator("body")).ToContainTextAsync("This structured learning plan is designed to help you develop essential skills in ASP.NET Core and web application development. Each phase builds upon the previous one, with clear objectives and resources to guide your learning.");
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Creating APIs with ASP.NET" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Controller-Based API" })).ToBeVisibleAsync();
        await Expect(Page.Locator("body")).ToContainTextAsync("Module Goal: Explain the differences and cost/benefit of a Controller-Based API vs a Minimal API. Refactor your Minimal API into a Controller-Based API.");
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Back to Top" })).ToBeVisibleAsync();
        await Expect(Page.Locator("body")).ToContainTextAsync("This learning plan includes interactive checkboxes that allow you to track your progress. Your progress is saved in your browser's local storage, so you can pick up where you left off, even after refreshing the page. Simply tick off tasks as you complete them!");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Microsoft: Logging in .NET" }).ClickAsync();
        await Expect(Page.Locator("#logging-in-net-core-and-aspnet-core")).ToContainTextAsync("Logging in .NET Core and ASP.NET Core");
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Expect(Page.Locator("body")).ToContainTextAsync("Exercise: What command would you use to create a new .NET console application using the .NET CLI? How do you list all available templates in the .NET CLI? Which command is used to build a .NET project? Describe the command to run a .NET application. How can you add a NuGet package to a .NET project using the CLI?");
        await Page.GetByText("Describe the command to run a").ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Microsoft: Minimal APIs overview" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Kestrel endpoint configuration" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "C# Corner: Restful API In ASP" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue with Recommended" }).ClickAsync();
        await Page.GetByText("ASP.NET", new() { Exact = true }).ClickAsync();
        await Page.GetByText("ASP.NET is a free web").ClickAsync();
        await Expect(Page.Locator("#ctl00_MainContent_DivDescription")).ToContainTextAsync("ASP.NET is a free web framework for building Web sites and Web applications using HTML, CSS and JavaScript. Create Web APIs, mobile sites and use real-time technologies.");
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Page.GetByRole(AriaRole.Heading, new() { Name = "dotnet cli" }).ClickAsync();
        await Page.GetByRole(AriaRole.Checkbox, new() { Name = "Section 1 completed" }).CheckAsync();
        await Expect(Page.GetByRole(AriaRole.Checkbox, new() { Name = "Section 1 completed" })).ToBeCheckedAsync();
    }
}
