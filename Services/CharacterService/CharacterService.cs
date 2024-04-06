global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{ Id=1 ,Name = "Sam"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter){
           
            
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id)+1;
            characters.Add(character);
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async  Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new  ServiceResponse<List<GetCharacterDto>>();
            try
            {      
            var character= characters.FirstOrDefault(c => c.Id==id);
            if(character is null)
            throw new Exception($"Character with Id {id} not found "); 
            
            characters.Remove(character);

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                
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
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public  async  Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id){
            var character= characters.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
            
        }

        public async  Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {  var serviceResponse = new  ServiceResponse<GetCharacterDto>();
            try
            {      
            var character= characters.FirstOrDefault(c => c.Id==updatedCharacter.Id);
            if(character is null)
            throw new Exception($"Character with Id {updatedCharacter.Id} not found "); 
            
            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defence = updatedCharacter.Defence;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterDto> (character);
                
            }
            catch (Exception Ex)
            {
                
                serviceResponse.Success= false;
                serviceResponse.Message =Ex.Message;
            }
    
            return serviceResponse;


        }
    }
}