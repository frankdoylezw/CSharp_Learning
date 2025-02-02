﻿using Microsoft.Playwright.NUnit;
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
    public async Task MyTest()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Page.Locator("li:has-text('Write a simple calculator') a").ClickAsync();
        await Page.Locator("a:has-text('Back to Home')").ClickAsync();
        await Expect(Page.Locator("body")).ToContainTextAsync("This learning plan includes interactive checkboxes that allow you to track your progress. Your progress is saved in your browser's local storage, so you can pick up where you left off, even after refreshing the page. Simply tick off tasks as you complete them!");
    }

    [Test]
    public async Task MyOtherTest()
    {
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Page.Locator("label").Filter(new() { HasText = "Variables (Learn More)" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.GotoAsync("https://frankdoylezw.github.io/CSharp_Learning/");
        await Page.Locator("li").Filter(new() { HasText = "Write a simple calculator" }).GetByRole(AriaRole.Link).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Steps to Write a Calculator Program");
    }

}