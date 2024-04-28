using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public int ChallengerId { get; set; }
        public string Result { get; set; } = "Pending";
        public Character? Character { get; set; }
        public int CharacterId { get; set; }
        
    }
}