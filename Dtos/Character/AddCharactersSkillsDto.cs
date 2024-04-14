using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Dtos.Character
{
    public class AddCharactersSkillsDto
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
    }
}