using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;
using PlaywrightDotnetApiUi.Framework.Configuration;

namespace PlaywrightDotnetApiUi.Tests.Fixtures;

public abstract class BaseTest : PageTest
{
    protected IAPIRequestContext Request = null!;

    [SetUp]
    public async Task SetUpHybridTesting()
    {
        var headers = new Dictionary<string, string> { { "Accept", "application/json" } };

        Request = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = TestConfiguration.ApiBaseUrl,
            ExtraHTTPHeaders = headers
        });

        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task TearDownHybridTesting()
    {
        var failed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed;

        if (failed)
        {
            var testPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "playwright-traces",
                $"{TestContext.CurrentContext.Test.Name}.zip");

            await Context.Tracing.StopAsync(new TracingStopOptions { Path = testPath });
        }
        else
        {
            await Context.Tracing.StopAsync(new TracingStopOptions());
        }

        await Request.DisposeAsync();
    }
}