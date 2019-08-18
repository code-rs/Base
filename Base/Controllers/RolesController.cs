using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace Base.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(
            RoleManager<Rol> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;

        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRol()
        {

            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(string id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            var actualizar = await _roleManager.FindByIdAsync(id);

            _mapper.Map(rol, actualizar);

            var resultado = await _roleManager.UpdateAsync(actualizar);

            if (resultado.Succeeded)
            {
                return CreatedAtAction("GetRol", new { id = rol.Id }, rol);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

            //return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            var resultado = await _roleManager.CreateAsync(rol);

            if (resultado.Succeeded)
            {

                return CreatedAtAction("GetRol", new { id = rol.Id }, rol);
            }
            else
            {

                return BadRequest(resultado.Errors);
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rol>> DeleteRol(string id)
        {
            var rol = await _roleManager.FindByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            var resultado = await _roleManager.DeleteAsync(rol);

            if (resultado.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

    }
}
