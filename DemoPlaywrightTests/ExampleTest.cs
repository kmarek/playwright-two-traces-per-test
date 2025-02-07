namespace DemoPlaywrightTests;

public class ExampleTest : BaseTest
{
    [Test]
    public async Task TestUsingTwoBrowsers()
    {
        var page1 = await OpenNewPageAsync();
        var page2 = await OpenNewPageAsync();

        await page1.GotoAsync("https://playwright.dev");
        await Expect(page1).ToHaveTitleAsync(new Regex("Playwright"));

        await page2.GotoAsync("https://azure.microsoft.com/en-us/products/playwright-testing/");
        await Expect(page2).ToHaveTitleAsync(new Regex("Playwright"));
    }
}
