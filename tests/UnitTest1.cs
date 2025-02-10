using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[TestFixture]
public class PlaywrightTests : PageTest
{
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
        
        // Check for main phase headings
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Introduction to ASP.NET" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Creating APIs with ASP.NET Core" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "The Model-View-Controller Design Pattern" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task CheckResourceLinksWork()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        
        // Check specific resource link exists and is clickable
        var dotnetCliLink = Page.GetByRole(AriaRole.Link, new() { Name = "Microsoft: .NET CLI overview" });
        await Expect(dotnetCliLink).ToBeVisibleAsync();
        
        // Verify the href attribute
        var href = await dotnetCliLink.GetAttributeAsync("href");
        Assert.That(href, Is.EqualTo("https://learn.microsoft.com/en-us/dotnet/core/tools/"));
    }

    [Test]
    public async Task CheckboxFunctionality()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        
        // Get the first checkbox (dotnet-cli)
        var checkbox = Page.Locator("#dotnet-cli");
        
        // Verify it exists and is initially unchecked
        await Expect(checkbox).ToBeVisibleAsync();
        Assert.That(await checkbox.IsCheckedAsync(), Is.False);
        
        // Check it and verify the state changes
        await checkbox.CheckAsync();
        Assert.That(await checkbox.IsCheckedAsync(), Is.True);
    }
}
