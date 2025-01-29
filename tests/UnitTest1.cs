using Microsoft.Playwright.NUnit;

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
    public async Task MyTest()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Page.Locator("label:has-text('Variables (Learn More)')").ClickAsync();
        await Page.Locator("li:has-text('Write a simple calculator') a").ClickAsync();
        await Expect(Page.Locator("h1")).ToContainTextAsync("Steps to Write a Calculator Program");
        await Page.Locator("a:has-text('Back to Home')").ClickAsync();
        await Page.Locator("button:has-text('Back to Top')").ClickAsync();
    }

}