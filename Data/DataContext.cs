using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skills>().HasData(
                new Skills { Id = 1, Name = "Fireball", Damage = 30 },
                new Skills { Id = 2, Name = "Frenzy", Damage = 20 },
                new Skills { Id = 3, Name = "Blizzard", Damage = 50 }
            );
        }
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skills> Skills => Set<Skills>();
    }
}