using Newtonsoft.Json;

namespace Backend.Models;

public class CaptchaResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }
    [JsonProperty("error-codes")]
    public List<string> ErrorCodes { get; set; } = [];
}
