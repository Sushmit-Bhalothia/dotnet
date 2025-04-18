using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class NewClass
    {
        public int id { get; set; }
        public int ChallengerId { get; set; }
        public string ResulT { get; set; } = "Pending";
        public Character? Character { get; set; }
        public int CharacterId { get; set; }
    }
}