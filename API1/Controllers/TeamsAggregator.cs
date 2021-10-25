using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [Route("api/[controller]/{id}")]
    [ApiController]
    public class TeamsAggregator: ControllerBase
    {
        [HttpGet]
        public IActionResult GetTeam(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTeam(string  teanName)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeam(int id, string name)
        {
            return View();
        }

        [HttpPatch]
        public IActionResult AddPlayerToTeam(string playerName)
        {
            return View();
        }
    }
}
