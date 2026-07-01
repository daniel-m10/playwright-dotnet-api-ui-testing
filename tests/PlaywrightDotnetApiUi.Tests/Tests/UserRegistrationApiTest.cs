using Microsoft.Playwright;
using PlaywrightDotnetApiUi.Framework.ApiClients;
using PlaywrightDotnetApiUi.Tests.Fixtures;

namespace PlaywrightDotnetApiUi.Tests.Tests;

[TestFixture]
[Category("UserRegistration")]
public class UserRegistrationApiTest : ApiBaseTest
{
    [Test]
    public async Task ShouldCreateUserAndVerifyViaApi()
    {
        var email = $"test_user_{Guid.NewGuid():N}@example.com";

        var formData = Request.CreateFormData();
        formData.Set("name", "john_doe");
        formData.Set("email", email);
        formData.Set("password", "secure_password123");
        formData.Set("title", "Mr");
        formData.Set("birth_date", "01");
        formData.Set("birth_month", "January");
        formData.Set("birth_year", "1990");
        formData.Set("firstname", "John");
        formData.Set("lastname", "Doe");
        formData.Set("company", "Example Corp");
        formData.Set("address1", "123 Main St");
        formData.Set("address2", "Apt 4B");
        formData.Set("country", "USA");
        formData.Set("zipcode", "12345");
        formData.Set("state", "NY");
        formData.Set("city", "New York");
        formData.Set("mobile_number", "5551234567");

        var response = await Request.PostAsync("/api/createAccount", new APIRequestContextOptions
        {
            Multipart = formData,
        });

        await Expect(response).ToBeOKAsync();

        var body = await response.JsonAsync();
        Assert.That(body.HasValue, Is.True);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(body.Value.GetProperty("responseCode").GetInt32(), Is.EqualTo(201));
            Assert.That(body.Value.GetProperty("message").GetString(), Is.EqualTo("User created!"));
        }
    }

    [Test]
    public async Task ShouldGetCreatedUserByEmailAndVerifyViaApi()
    {
        var userApiClient = new UserApiClient(Request);
        var (email, _) = await userApiClient.CreateUserAsync();

        var response = await Request.GetAsync($"/api/getUserDetailByEmail?email={email}");

        await Expect(response).ToBeOKAsync();
        var body = await response.JsonAsync();

        Assert.That(body.HasValue, Is.True);
        var userName = body.Value.GetProperty("user").GetProperty("name").GetString();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(body.Value.GetProperty("responseCode").GetInt32(), Is.EqualTo(200));
            Assert.That(userName, Is.EqualTo("john_doe"));
        }
    }
}