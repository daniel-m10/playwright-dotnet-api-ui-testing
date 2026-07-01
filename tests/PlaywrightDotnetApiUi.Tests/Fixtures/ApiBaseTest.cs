using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightDotnetApiUi.Framework.Configuration;

namespace PlaywrightDotnetApiUi.Tests.Fixtures;

public abstract class ApiBaseTest : PlaywrightTest
{
    protected IAPIRequestContext Request = null!;

    [SetUp]
    public async Task SetUpApiTesting()
    {
        var headers = new Dictionary<string, string> { { "Accept", "application/json" } };

        Request = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = TestConfiguration.ApiBaseUrl,
            ExtraHTTPHeaders =  headers
        });
    }

    [TearDown]
    public async Task TearDownApiTesting()
    {
        await Request.DisposeAsync();
    }
}