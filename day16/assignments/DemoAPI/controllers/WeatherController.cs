using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public ActionResult WeatherForecast()
    {
        return Ok("Weather Forecast");
    }
}