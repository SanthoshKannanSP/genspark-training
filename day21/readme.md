# Day 21 - June 2nd, 2025
## Session Overview
- REST API
- Hashing
- JWT Authentication

## REST API
[Reference](https://aws.amazon.com/what-is/restful-api/)
- **REST:** Representational State Transfer
- REST is a guideline on how to manage communication of different software systems over the internet
- **REST API:** APIs that follow the REST architectural style
- **Principles of REST architecture:**
    - **Client-Server Architecture:** The client(frontend) is separate from the server(backend)
    - **Uniform Interface:** Consistent way to interact with the API, regardless of the resource.
    - **Statelessness:** The server doesn't persist any data between requests. The client must provide all the information needed by the server to process the request.
    - **Layered System:** The API can be made of multiple layers (caching, authentication). The client doesn't know about the layers of the API
    - **Cacheability:** Should support caching. Response must indicate whether they are cacheable or noncacheable
    - **Code on demand:** Can return executable programming code to the client

## Hashing
[Reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha256?view=net-9.0)
- Hashing with secret key
```cs
string message = "Hello, World!";
string secretKey = "my-secret-key";

HMACSHA256 hMACSHA256 = new HMACSHA256(secretKey);
var EncryptedData = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(message));
```
- Hashing with random key
```cs
string message = "Hello, World!";

HMACSHA256 hMACSHA256 = new HMACSHA256(secretKey);
var EncryptedData = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(message));
var secretKey = hMACSHA256.Key;
```

## JWT Authentication
[Reference](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0)
- **JWT:** JSON Web Token
- Widely used for stateless authentication
- **JWT Authentication Flow**
    - User logs in with credentials
    - Server validates credentials
    - Server generates a JWT Token with claims (user info) and sends it back to client
    - Client stores the token either in local storage or cookies
    - For future requests, client sends the token in `Authorization` header
    - Server verifies the token before granting access to secure resources
- **Installing**
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
- **Configuration** in `appsettings.json`
```JSON
"Jwt": {
  "Key": "your_secret_key_here",
  "Issuer": "yourapp",
  "Audience": "yourapp_users",
  "ExpiresInMinutes": 60
}
```
- **Generating JWT Token**
```cs
public async Task<string> GenerateToken(User user)
{
    // The claims (user info) that will be in the token
    List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier,user.Username),
        new Claim(ClaimTypes.Role,user.Role)
    };
    // Signing the JWT Token with JWT security key
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

    // Creating JWT Token
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
    };
    var tokenHandler = new JwtSecurityTokenHandler();

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
```
- **JWT Token explicit validation** in `Program.cs` [Reference](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0#jwt-bearer-token-explicit-validation)
```cs
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]))
                    };
                });
```
- **Forcing authentication in Controller** [Reference](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0#forcing-the-bearer-authentication)
```cs
// Forcing Authentication for entire controller
[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{}

// Forcing Authentication for select routes
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    // Authorized roles separated by comma
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
}
```
