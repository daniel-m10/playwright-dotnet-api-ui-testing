namespace PlaywrightDotnetApiUi.Framework.Configuration;

public static class TestConfiguration
{
    public static string BaseUrl => Environment.GetEnvironmentVariable("BASE_URL") ?? "https://automationexercise.com";
    public static string ApiBaseUrl => Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://automationexercise.com";
}