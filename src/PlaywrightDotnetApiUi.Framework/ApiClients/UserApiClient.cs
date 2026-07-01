using Microsoft.Playwright;

namespace PlaywrightDotnetApiUi.Framework.ApiClients;

public class UserApiClient(IAPIRequestContext request)
{
    public async Task<(string Email, string Password)> CreateUserAsync(string name = "john_doe",
        string password = "secure_password123")
    {
        var email = $"test_user_{Guid.NewGuid():N}@example.com";

        var formData = request.CreateFormData();
        formData.Set("name", name);
        formData.Set("email", email);
        formData.Set("password", password);
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

        var response = await request.PostAsync("/api/createAccount", new APIRequestContextOptions
        {
            Multipart = formData
        });

        return !response.Ok
            ? throw new Exception($"Failed to create user. Status: {response.Status}")
            : (email, password);
    }
}