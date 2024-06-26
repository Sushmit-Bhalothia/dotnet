using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Character
    {
      public int Id { get; set; } 
      public string Name { get; set; } = "Frodo";
      public int HitPoints { get; set; } = 100;
      public int Strength { get; set; } = 10;
      public int Defence { get; set; } = 10;
      public int Intelligence { get; set; } = 10;
      public RpgClass Class { get; set; } = RpgClass.Knight;
      public User? User { get; set; }
      public Weapon? Weapon { get; set; }
      public List<Skills>? Skills { get; set; }
      public int Fights { get; set; }
      public int Victories { get; set; }
      public int Defeats { get; set; }
      public List<Challenge> Challenges { get; set; }= new List<Challenge>();

    

        
    }
}