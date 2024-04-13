using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[] { };
        public byte[] PasswordSalt { get; set; } = new byte[] { };
        public List<Character>? Characters { get; set; }
        
    }
}