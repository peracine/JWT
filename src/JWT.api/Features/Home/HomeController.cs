using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWT.api.Features.Home
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            return Ok(new { application = "JWT Application", utc_time = DateTime.UtcNow });            
        }   
    }
}