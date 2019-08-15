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
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuariosController(
            UserManager<Usuario> userManager,
            RoleManager<Rol> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;

        }

        // GET: /Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            //return await _context.Usuario.ToListAsync();

            //var usuarios = await _userManager.Users.ToListAsync();

            var usuarios = await _userManager.Users.ToListAsync();

            return usuarios;

        }

        // GET: /Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            //var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: /Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario Usuario)
        {
            if (string.IsNullOrEmpty(Usuario.UserName)) Usuario.UserName = Usuario.Email;
            //_context.Rol.Add(rol);
            var resultado = await _userManager.CreateAsync(Usuario, Usuario.PasswordHash);

            if (resultado.Succeeded)
            {

                return CreatedAtAction("GetUsuario", new { id = Usuario.Id }, Usuario);
            } else
            {

                return BadRequest(resultado.Errors);
            }

            
        }

        // PUT: /Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(string id, UsuarioViewModel usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            var actualizar = await _userManager.FindByIdAsync(id);

            _mapper.Map(usuario, actualizar);

            //Si tenemos nueva contraseña, la hasheamos y la añadimos
            if(!string.IsNullOrEmpty(usuario.PasswordHash))
            {
                var passNueva = usuario.PasswordHash;
                await _userManager.RemovePasswordAsync(actualizar);
                await _userManager.AddPasswordAsync(actualizar, passNueva);

            }            
            
            var resultado = await _userManager.UpdateAsync(actualizar);

            if (resultado.Succeeded)
            {
                return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

            //return NoContent();
            
        }

        // DELETE: /Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var resultado = await _userManager.DeleteAsync(usuario);

            if (resultado.Succeeded)
            {
                //OK
            }
            else
            {
                //Log de error
            }

            return usuario;
        }
    }
}
