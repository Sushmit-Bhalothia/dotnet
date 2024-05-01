using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<ServiceResponse<string>> GetChallenge(int charId)
        {
            var response = new ServiceResponse<string>();
            try{
                var character = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c=> c.Id == charId);
                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }
                if(character.User!.Id != GetUserId()){
                    response.Success = false;
                    response.Message = "Not your character.";
                    return response;
                }
                var challenges = await _context.Challenges.Where(c => c.Character!.User!.Id == GetUserId() && c.CharacterId == charId).ToListAsync();
                foreach (var c in challenges)
                {
                    response.Data += "charcter with id  "+ c.ChallengerId + " challenged you  ";
                }
                
               if(challenges.Count == 0){
                   response.Data = "No challenges found";
               }
            }
            catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}