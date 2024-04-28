using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet.Dtos.Challenge;

namespace dotnet.Services.ChallengeServices
{
    public class ChallengeService : IChallengeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChallengeService(DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        public async Task<ServiceResponse<string>> ChallengeCharacter(ChallengetDto challenge)
        {
            var response = new ServiceResponse<string>();
            try{
                 var challenger= await _context.Characters.FirstOrDefaultAsync(c => c.Id==challenge.Challenger && c.User!.Id == GetUserId());
                 var challenged= await _context.Characters.FirstOrDefaultAsync(c => c.Id==challenge.Challenged);
                if(challenger is null){
                    response.Success = false;
                    response.Message = "challenger Character not found.";
                    return response;
                }
                 // Add this import statement
                 else if(challenged is null){
                    response.Success = false;
                    response.Message = "challenged Character not found.";
                    return response;
                 }

                                else{
                                    var challengerr = new Challenge{
                                        ChallengerId = challenger.Id,
                                        CharacterId = challenged.Id
                                    };
                                    await _context.Challenges.AddAsync(challengerr);
                                    await _context.SaveChangesAsync();

                                
                                    response.Data = "Challenge Sent to " + challenged.Name + " " + "by" + challenger.Name;
                                }

            }
            catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }
            await _context.SaveChangesAsync(); 
            return response;
            
        }
    }
}