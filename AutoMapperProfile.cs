using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Fight;
using dotnet.Dtos.Skill;
using dotnet.Dtos.Weapon;

namespace dotnet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            CreateMap<Weapon,GetWeaponDto>();
            CreateMap<Skills,GetSkillDto>();
            CreateMap<Character, HighScoreDto>();
        }
    }
}