# Day 22 - June 3rd, 2025
## Session Overview
- Policy-based Authorization
- Unit Testing, AAA method

## Policy-based Authorization
[Reference](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-9.0)
- **Policy:** A named set of requirements that a user must satisfy to access a resource
```cs
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtleastExperience10", policy =>
        policy.Requirements.Add(new MinimumExperienceRequirement(10)));
});
```
- **Requirement:** A requirement inside a policy. Defines data that will be used by the Handler.
```cs
public class MinimumExperienceRequirement : IAuthorizationRequirement
{
    public int MinimumExperience { get; }

    public MinimumExperienceRequirement(int minimumExperience)
    {
        MinimumExperience = minimumExperience;
    }
}
```
- **Handler:** Logic that check whether a requirement is met. Should implement the `HandleRequirementAsync` method. The `HandleRequirementAsync` Method should either call `context.Succeed(requirement)` to mark the requirement to be satisfied, call `context.Fail()` to fail the requirement explicitly or do nothing (fails implicitly) 
```cs
public class MinimumExperienceHandler : AuthorizationHandler<MinimumExperienceRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                    MinimumExperienceRequirement requirement)
    {
        var experienceClaim = context.User.FindFirst("YearsOfExperience");

        if (experienceClaim == null)
            return Task.CompletedTask;

        int years;
        if (!int.TryParse(experienceClaim.Value, out years))
            return Task.CompletedTask;

        if (years >= requirement.MinimumExperience)
                context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
```

### Defining different types of requirements in policy
[Reference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authorization.authorizationpolicybuilder?view=aspnetcore-9.0)
- `RequireAuthenticatedUser()`: Requires the user to be authenticated
- `RequireAssertion(Func<AuthorizationHandlerContext, bool>)`: Custom lambda-based checks. Should return `true` or `false`
- `RequireClaim(string claimType)`: Requires the presence of a specific type of claim
- `RequireClaim(string claimType, string[] acceptedValues)`: Requires the specified type of claim to have one of the accepted values
- `RequireRole(string role)`: Requires the user to be a specific role
- `RequireUserName(string userName)`: Requires the user's identity to match the specified username
- `AddRequirements(IAuthorizationRequirement)`: Custom requirement class
- `Combine(AuthorizationPolicy)`: Combines with another policy

## Unit Testing
[Reference](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-nunit)
- Testing individual units of code in isolation
```bash
dotnet new nunit <TestProjectName>
```
- Add reference to the project under tested
```bash
dotnet reference add <ProjectName>
```
- Best practice - Minimum three unit tests for each method - one passing, one failing and one exception.
- **Moq:** Library to mock(simulate) other dependencies of a method - by assuming all the other dependecies give the correct output - to focus on only testing the unit of code
```bash
dotnet add package Moq
```
- If project is huge, an entire another database could be used for testing alone. Else in-memory database could be used to simulate a database
```bash
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```
- **Setup:** Prepare the test environment. Method tagged with `[Setup]` Attribute is immediately run before calling each test
```cs
[SetUp]
public void Setup()
{
    var options = new DbContextOptionsBuilder<ClinicContext>()
                        .UseInMemoryDatabase("TestDb")
                        .Options;
    _context = new ClinicContext(options);
}
```
- **Three A's of Unit Testing:** Common pattern to write unit tests for a method. [Reference](https://learn.microsoft.com/en-us/visualstudio/test/unit-test-basics?view=vs-2022#write-your-tests)
```cs
[Test]
public void Withdraw_ValidAmount_ChangesBalance()
{
    // Arrange - Initialize objects and values needed by the method under test
    double currentBalance = 10.0;
    double withdrawal = 1.0;
    double expected = 9.0;
    var account = new CheckingAccount("JohnDoe", currentBalance);

    // Act - Invoke the method under test with the arranged parameters
    account.Withdraw(withdrawal);

    // Assert - Verify the action of the method under test behaves as expected
    Assert.AreEqual(expected, account.Balance);
}
```
- **Assertions:** Validate various test outcomes. [Reference](https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html)
- **TearDown:** Dispose of objects used for testing (like in-memory database, memory streams)
```cs
[TearDown]
public void TearDown()
{
    _context.Dispose();
}
```
- **Running the test:**
```bash
dotnet test
```
- `[Test]` **Attribute:** Simple, standalone test. No parameter can be passed in directly
```cs
[Test]
public void Add_TwoNumbers_ReturnsSum()
{
    Assert.AreEqual(5, Add(2, 3));
}
```
- `[TestCase]` **Attribute:** Parameterized tests. A single method could be tested with multiple input-output combos
```cs
[TestCase(2, 3, 5)]
[TestCase(0, 0, 0)]
[TestCase(-1, 1, 0)]
public void Add_WithVariousInputs_ReturnsCorrectSum(int a, int b, int expected)
{
    Assert.AreEqual(expected, Add(a, b));
}
```