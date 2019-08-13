using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Base.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BaseDbContext _context;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityUser> _roleManager;
        private readonly IConfiguration _configuration;

        public UsuariosController(
            BaseDbContext context,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityUser> roleManager,
            IConfiguration configuration)
        {
            _context = context;

            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

    }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            //return await _context.Usuario.ToListAsync();
            
            var usuarios = await _userManager.Users.ToListAsync();

            return usuarios;

        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
