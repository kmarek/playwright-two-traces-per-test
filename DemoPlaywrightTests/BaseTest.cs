using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoPlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class BaseTest : BrowserTest
{
    private readonly List<IBrowserContext> _contexts = new();

    public virtual BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            Locale = "en-GB"
        };
    }

    protected async Task<IPage> OpenNewPageAsync()
    {
        var context = await NewContext(ContextOptions());
        _contexts.Add(context);

        await context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        var page = await context.NewPageAsync();
        return page;
    }

    [TearDown]
    public async Task BaseTearDown()
    {
        string baseTraceName = $"trace_{TestContext.CurrentContext.Result.Outcome.Status}_{TestContext.CurrentContext.Test.MethodName}_{DateTime.UtcNow:yyyy-MM-dd_HHmmss}_";
        int traceIndex = 1;

        foreach (var context in _contexts)
        {
            string traceName = $"{baseTraceName}{traceIndex++}.zip";

            await context.Tracing.StopAsync(new()
            {
                Path = traceName
            });

            TestContext.AddTestAttachment(traceName);
        }

        _contexts.Clear();
    }
}
