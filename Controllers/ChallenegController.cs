using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Challenge;
using dotnet.Services.Challenge;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]    
    public class ChallenegController : ControllerBase
    {
       private readonly  IChallengeService _ChallengeService;
    

        public ChallenegController(IChallengeService challengeService)
        {
            
            
            _ChallengeService = challengeService;
        }
        [HttpPost("Challenge")]
        public async Task<ActionResult<ServiceResponse<string>>> ChallengeCharacter(ChallengetDto challenge){
            return Ok(await _ChallengeService.ChallengeCharacter(challenge));
        }
        
    }
}