using Microsoft.Playwright;

namespace PlaywrightDotnetApiUi.Framework.Pages;

public class HomePage(IPage page)
{
    private readonly ILocator _logOutButton =
        page.GetByRole(AriaRole.Listitem).Filter(new LocatorFilterOptions { HasText = " Logout" });

    public ILocator LoggedInText => page.GetByRole(AriaRole.Listitem).Filter(new LocatorFilterOptions
    {
        HasText = " Logged in as"
    });

    public async Task LogOutAsync()
    {
        await _logOutButton.ClickAsync();
    }
}