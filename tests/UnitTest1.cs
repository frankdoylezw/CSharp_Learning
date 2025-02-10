using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class PlaywrightTests : PageTest
{
    [SetUp]
    public void SetUp()
    {
        // Clear the screenshots directory
        var screenshotsDir = "screenshots";
        if (Directory.Exists(screenshotsDir))
        {
            Directory.Delete(screenshotsDir, true);
        }
        Directory.CreateDirectory(screenshotsDir);
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
        catch (Exception ex)
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
        
            // Verify it exists and is initially unchecked
            await Expect(checkbox).ToBeVisibleAsync();
            Assert.That(await checkbox.IsCheckedAsync(), Is.False);
        
            // Check it and verify the state changes
            await checkbox.CheckAsync();
            Assert.That(await checkbox.IsCheckedAsync(), Is.True);
        }
        catch (Exception)
        {
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshots/CheckboxFunctionality.png" });
            throw;
        }
    }
}
