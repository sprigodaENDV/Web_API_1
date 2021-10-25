using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchAggregator : ControllerBase
    {

        [HttpGet]
        public IActionResult ShowMatches()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchMatchByPlayer(Player player)
        {
            return IActionResult OK();
        }
    }
}
