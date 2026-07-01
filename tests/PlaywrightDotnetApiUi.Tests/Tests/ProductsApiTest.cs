using PlaywrightDotnetApiUi.Tests.Fixtures;

namespace PlaywrightDotnetApiUi.Tests.Tests;

[TestFixture]
[Category("Products")]
public class ProductsApiTest : ApiBaseTest
{
    [Test]
    public async Task ShouldGetProductList()
    {
        var response = await Request.GetAsync("/api/productsList");

        await Expect(response).ToBeOKAsync();

        var body = await response.JsonAsync();
        Assert.That(body.HasValue, Is.True);

        var responseCode = body.Value.GetProperty("responseCode").GetInt32();
        Assert.That(responseCode, Is.EqualTo(200));

        var products = body.Value.GetProperty("products");
        Assert.That(products.GetArrayLength(), Is.GreaterThan(0));
    }
}