using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Challenge;

namespace dotnet.Services.Challenge
{
    public class ChallengeService : IChallengeService
    {
        private readonly DataContext _context;

        public ChallengeService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> ChallengeCharacter(ChallengetDto challenge)
        {
            var response = new ServiceResponse<string>();
            try{
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id== challenge.Challenged);
                if(character is null){
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }
                else{
                    response.Data = "Challenge Sent to " + character.Name + "!";
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