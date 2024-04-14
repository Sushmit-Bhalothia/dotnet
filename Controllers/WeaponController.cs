using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.Dtos.Weapon;
using dotnet.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponsService _weaponsService;
        public WeaponController(IWeaponsService weaponsService)
        {
            _weaponsService = weaponsService;
            
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponsService.AddWeapon(newWeapon));
        }
        
        
    }

    public interface IActionResult<T>
    {
    }
}