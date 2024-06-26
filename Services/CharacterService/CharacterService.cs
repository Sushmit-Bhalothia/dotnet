global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace dotnet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper,DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter){
           
            
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = await _context.Characters
            .Where(c => c.User!.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async  Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new  ServiceResponse<List<GetCharacterDto>>();
            try
            {      
            var character= await _context.Characters.FirstOrDefaultAsync(c => c.Id==id && c.User!.Id == GetUserId());
            if(character is null)
            throw new Exception($"Character with Id {id} not found "); 
            
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
            .Where(c => c.User!.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
                
            }
            catch (Exception Ex)
            {
                
                serviceResponse.Success= false;
                serviceResponse.Message =Ex.Message;
            }
    
            return serviceResponse;


        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(){
           
           var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
           var dbCharacters = await _context.Characters
            .Include(c => c.Weapon)
            .Include(c => c.Skills)
            .Where(c=> c.User!.Id==GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public  async  Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id){
            var dbcharacter= await _context.Characters
            .Include(c => c.Weapon)
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);
            return serviceResponse;
            
        }

        public async  Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {  var serviceResponse = new  ServiceResponse<GetCharacterDto>();
            try
            {      
            var character=  await _context.Characters
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id==updatedCharacter.Id);
            if(character is null || character.User!.Id != GetUserId())
            throw new Exception($"Character with Id {updatedCharacter.Id} not found "); 
            
            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defence = updatedCharacter.Defence;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto> (character);
                
            }
            catch (Exception Ex)
            {
                
                serviceResponse.Success= false;
                serviceResponse.Message =Ex.Message;
            }
    
            return serviceResponse;


        }

        public async  Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharactersSkillsDto newCharacterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User!.Id == GetUserId());
                if(character is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                    return serviceResponse;
                }
                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if(skill is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Skill not found.";
                    return serviceResponse;
                }
               character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}