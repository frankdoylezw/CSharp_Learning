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

        // Start video recording
        await Context.NewPageAsync(new BrowserNewPageOptions
        {
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "videos"),
            RecordVideoSize = new() { Width = 1280, Height = 720 }
        });
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

        // Stop video recording
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
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        var title = await Page.TitleAsync();
        Assert.That(title, Does.Contain("Structured Learning Plan for Web Application Development"));
    }

    [Test]
    public async Task CheckPhaseHeadingsExist()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

        // Check for main phase headings using a more specific selector
        await Expect(Page.Locator(".phase-section h2").First).ToContainTextAsync("Introduction to ASP.NET");
        await Expect(Page.Locator(".phase-section h2").Nth(1)).ToContainTextAsync("Creating APIs with ASP.NET Core");
        await Expect(Page.Locator(".phase-section h2").Nth(2)).ToContainTextAsync("The Model-View-Controller Design Pattern");
    }

    [Test]
    public async Task CheckResourceLinksWork()
    {
        try
        {
            await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

            // Check specific resource link exists and is clickable
            var dotnetCliLink = Page.Locator(".resource-label a", new() { HasText = "Microsoft: .NET CLI overview" });
            await Expect(dotnetCliLink).ToBeVisibleAsync();

            // Verify the href attribute
            var href = await dotnetCliLink.GetAttributeAsync("href");
            Assert.That(href, Is.EqualTo("https://learn.microsoft.com/en-us/dotnet/core/tools/"));
        }
        catch (Exception)
        {
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshots/CheckResourceLinksWork.png" });
            throw;
        }
    }

    [Test]
    public async Task CheckboxFunctionality()
    {
        try
        {
            await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");

            // Get the first checkbox (section1-completed)
            var checkbox = Page.Locator("#section1-completed");

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
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshots/CheckboxFunctionality.png" });
            throw;
        }
    }
}
