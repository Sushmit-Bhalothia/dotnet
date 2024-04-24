using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Fight;

namespace dotnet.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async  Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
           try
           {
               var attacker = await _context.Characters
                   .Include(c => c.Skills)
                   .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
               var opponent = await _context.Characters
                   .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

               if(attacker is null || opponent is null || attacker.Skills is null)
               {
                throw new Exception("Attacker or Opponent or Attacker's Weapon not found");
               }
               var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
               if(skill is null)
               {
                   response.Success = false;
                   response.Message = $"{attacker.Name} does not have this skill";
                   return response;
               }
               int damage= skill.Damage + (new Random().Next(attacker.Intelligence));
               damage -= new Random().Next(opponent.Defeats);
               if(damage>0)
               {
                   opponent.HitPoints -= damage;
               }
               if(opponent.HitPoints<=0)
               {
                   response.Message = $"{opponent.Name} has been defeated";
               }
               await _context.SaveChangesAsync();
               response.Data = new AttackResultDto
               {
                   Attacker = attacker.Name,
                   Opponent = opponent.Name,
                   AttackerHP = attacker.HitPoints,
                   OpponentHP = opponent.HitPoints,
                   Damage = damage
               };
           }
           catch (Exception ex)
           {
               response.Success = false;
               response.Message = ex.Message;
           }
           return response;
        }

        public async  Task<ServiceResponse<AttackResultDto>> WeaponAttack(WaeponAttackDto request)
        {
           var response = new ServiceResponse<AttackResultDto>();
           try
           {
               var attacker = await _context.Characters
                   .Include(c => c.Weapon)
                   .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
               var opponent = await _context.Characters
                   .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

               if(attacker is null || opponent is null || attacker.Weapon is null)
               {
                throw new Exception("Attacker or Opponent or Attacker's Weapon not found");
               }
               int damage= attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
               damage -= new Random().Next(opponent.Defeats);
               if(damage>0)
               {
                   opponent.HitPoints -= damage;
               }
               if(opponent.HitPoints<=0)
               {
                   response.Message = $"{opponent.Name} has been defeated";
               }
               await _context.SaveChangesAsync();
               response.Data = new AttackResultDto
               {
                   Attacker = attacker.Name,
                   Opponent = opponent.Name,
                   AttackerHP = attacker.HitPoints,
                   OpponentHP = opponent.HitPoints,
                   Damage = damage
               };
           }
           catch (Exception ex)
           {
               response.Success = false;
               response.Message = ex.Message;
           }
           return response;
        }
    }
}