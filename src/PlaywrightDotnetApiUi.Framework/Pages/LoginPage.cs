using Microsoft.Playwright;
using PlaywrightDotnetApiUi.Framework.Configuration;

namespace PlaywrightDotnetApiUi.Framework.Pages;

public class LoginPage(IPage page)
{
    private readonly ILocator _emailAddressField = page.Locator("[data-qa='login-email']");
    private readonly ILocator _passwordField = page.Locator("[data-qa='login-password']");

    private readonly ILocator _loginButton = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions
    {
        Name = "Login",
    });
    
    public async Task GoToLoginPageAsync()
    {
        await page.GotoAsync($"{TestConfiguration.BaseUrl}/login");
    }

    public async Task LoginAsync(string email, string password)
    {
        await _emailAddressField.FillAsync(email);
        await _passwordField.FillAsync(password);
        await _loginButton.ClickAsync();
    }
}