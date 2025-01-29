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
        await Page.Locator("label").Filter(new() { HasText = "Variables (Learn More)" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.Locator("li").Filter(new() { HasText = "Write a simple calculator" }).GetByRole(AriaRole.Link).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Steps to Write a Calculator Program");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Back to Home" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Back to Top" }).ClickAsync();
    }

}