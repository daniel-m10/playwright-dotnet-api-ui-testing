using Microsoft.Playwright;
using PlaywrightDotnetApiUi.Framework.ApiClients;
using PlaywrightDotnetApiUi.Framework.Pages;
using PlaywrightDotnetApiUi.Tests.Fixtures;

namespace PlaywrightDotnetApiUi.Tests.Tests;

[TestFixture]
[Category("Login")]
public class LoginTest : BaseTest
{
    private LoginPage _loginPage = null!;
    private HomePage _homePage = null!;
    private UserApiClient _userApiClient = null!;
    private string _email = string.Empty;
    private string _password = string.Empty;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(Page);
        await _loginPage.GoToLoginPageAsync();

        _userApiClient = new UserApiClient(Request);
        (_email, _password) = await _userApiClient.CreateUserAsync();

        _homePage = new HomePage(Page);
    }

    [Test]
    public async Task ShouldCreateUserViaApiAndLoginViaUi()
    {
        await _loginPage.LoginAsync(_email, _password);

        await Expect(_homePage.LoggedInText).ToBeVisibleAsync();
        await Expect(_homePage.LoggedInText).ToContainTextAsync("john_doe");
    }
}