# playwright-dotnet-api-ui-testing

A hybrid API + UI test automation framework built with Playwright .NET, C#, and NUnit targeting Automation Exercise.

## Purpose

This repository demonstrates hybrid SDET testing strategy — combining Playwright's native API testing capabilities with browser automation. It shows how to use API calls for test data setup and verification alongside UI-driven scenarios, avoiding brittle UI-only setup where a direct API call is faster and more reliable.

## Tech Stack

- C#
- .NET 10 LTS
- Playwright .NET 1.61.0
- NUnit
- Microsoft.Playwright.NUnit
- Rider (recommended IDE)
- GitHub Actions
- dotnet CLI

## Project Structure

```text
playwright-dotnet-api-ui-testing/
  src/
    PlaywrightDotnetApiUi.Framework/
      ApiClients/       # API client wrappers (UserApiClient)
      Configuration/     # TestConfiguration with environment variable support
      Pages/             # Page Objects for UI interactions
  tests/
    PlaywrightDotnetApiUi.Tests/
      Fixtures/          # BaseTest (hybrid) and ApiBaseTest (API-only)
      Tests/              # NUnit test fixtures organized by feature
```

## Setup

```powershell
dotnet restore
dotnet build
pwsh tests/PlaywrightDotnetApiUi.Tests/bin/Debug/net10.0/playwright.ps1 install
```

## Running Tests

Run all tests:

```powershell
dotnet test
```

Run by category:

```powershell
dotnet test --filter "Category=Products"
dotnet test --filter "Category=UserRegistration"
dotnet test --filter "Category=Login"
```

## Test Coverage

| Fixture | Category | Type | Tests |
|---|---|---|---|
| ProductsApiTest | Products | API only | Fetch product list, validate response |
| UserRegistrationApiTest | UserRegistration | API only | Create user, verify creation and retrieval by email |
| LoginTest | Login | Hybrid | Create user via API, login via UI, verify session |

**Total: 4 tests**

## Architecture Notes

- **Two-project structure** separates framework logic (Page Objects, API clients, configuration) from test logic, consistent with `playwright-dotnet-ecommerce-framework`.
- **Two base test classes** address a key architectural concern — API-only tests should never launch a browser:
    - `ApiBaseTest` inherits from `PlaywrightTest` — provides `IAPIRequestContext` only, no browser overhead
    - `BaseTest` inherits from `PageTest` — provides both `IPage` and `IAPIRequestContext` for hybrid scenarios, plus tracing on failure
- **Playwright native API testing** is used instead of `HttpClient` or third-party HTTP libraries — `IAPIRequestContext` shares configuration and lifecycle with the same Playwright instance used for UI automation.
- **`UserApiClient`** centralizes user creation logic used across multiple test fixtures, avoiding duplicated form data setup.
- **Multipart form data** is required for Automation Exercise API endpoints — `application/x-www-form-urlencoded` and `application/json` are both rejected by this specific API.
- **Unique email per test run** (`Guid.NewGuid()`) avoids account collision issues, since Automation Exercise's account deletion via API is unreliable.

## CI/CD

GitHub Actions runs on every push and PR to `main`.

- Runner: `ubuntu-latest`
- Base URLs stored as GitHub Secrets
- Playwright trace artifacts are uploaded on failure

## Debugging and Artifacts

When a hybrid or UI test fails locally, a trace zip is saved to:

```
playwright-traces/{TestName}.zip
```

API-only tests do not generate traces since no browser context exists.

Open a trace with:

```powershell
pwsh tests/PlaywrightDotnetApiUi.Tests/bin/Debug/net10.0/playwright.ps1 show-trace playwright-traces/{TestName}.zip
```

## Next Improvements

- Add product search API + UI hybrid scenario
- Add negative API test cases (invalid credentials, missing fields)
- Explore cart API endpoints for hybrid cart scenarios
