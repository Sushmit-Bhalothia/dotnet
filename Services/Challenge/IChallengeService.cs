using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Challenge;

namespace dotnet.Services.Challenge
{
    public interface IChallengeService
    {
        Task<ServiceResponse<String>> ChallengeCharacter(ChallengetDto challenge);
    }
}