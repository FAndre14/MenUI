using Common.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace MenUI.Server.Controllers
{
	[ApiController]
	[Route("api/")]
	public class WeatherController : Controller
	{
        [HttpGet]
        [Route("GetWeather")]
        public IActionResult GetWeatherFunc()
        {
            return Ok(DatabaseMockup.Products);
        }
    }
}
